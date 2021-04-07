using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Velentr.Collections.Collections;
using Velentr.Input.Conditions;
using Velentr.Input.GamePad;
using Velentr.Input.Keyboard;
using Velentr.Input.Mouse;
using Velentr.Input.Touch;
using Velentr.Input.Voice;

namespace Velentr.Input
{
    public class InputManager
    {
        /// <summary>
        /// The window
        /// </summary>
        private static Rectangle window;

        /// <summary>
        /// The center coordinates
        /// </summary>
        private static Point centerCoordinates;

        /// <summary>
        /// The tracked conditions
        /// </summary>
        public Bank<string, InputCondition> TrackedConditions = new Bank<string, InputCondition>();

        /// <summary>
        /// Initializes a new instance of the <see cref="InputManager"/> class.
        /// </summary>
        public InputManager(Game game)
        {
            _inputServices = new Dictionary<string, InputService>();
            Game = game;
        }

        /// <summary>
        /// Gets the game.
        /// </summary>
        /// <value>
        /// The game.
        /// </value>
        public Game Game { get; private set; }

        /// <summary>
        /// Gets the current time.
        /// </summary>
        /// <value>
        /// The current time.
        /// </value>
        public GameTime CurrentTime { get; private set; }

        /// <summary>
        /// Gets the window.
        /// </summary>
        /// <value>
        /// The window.
        /// </value>
        public GameWindow Window => Game.Window;

        /// <summary>
        /// Gets a value indicating whether this instance is window active.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is window active; otherwise, <c>false</c>.
        /// </value>
        public bool IsWindowActive => Game.IsActive;

        /// <summary>
        /// Gets the width of the window.
        /// </summary>
        /// <value>
        /// The width of the window.
        /// </value>
        public int WindowWidth => Window.ClientBounds.Width;

        /// <summary>
        /// Gets the height of the window.
        /// </summary>
        /// <value>
        /// The height of the window.
        /// </value>
        public int WindowHeight => Window.ClientBounds.Height;

        /// <summary>
        /// Gets the index of the condition.
        /// </summary>
        /// <value>
        /// The index of the condition.
        /// </value>
        public ulong ConditionIndex { get; private set; }

        /// <summary>
        /// Gets the mouse service.
        /// </summary>
        /// <value>
        /// The mouse.
        /// </value>
        public MouseService Mouse => (MouseService)_inputServices[Constants.MouseService];

        /// <summary>
        /// Gets the keyboard service.
        /// </summary>
        /// <value>
        /// The keyboard.
        /// </value>
        public KeyboardService Keyboard => (KeyboardService)_inputServices[Constants.KeyboardService];

        /// <summary>
        /// Gets the game pad service.
        /// </summary>
        /// <value>
        /// The game pad.
        /// </value>
        public GamePadService GamePad => (GamePadService)_inputServices[Constants.GamePadService];

        /// <summary>
        /// Gets the touch service.
        /// </summary>
        /// <value>
        /// The touch.
        /// </value>
        public DefaultTouchService Touch => (DefaultTouchService)_inputServices[Constants.TouchService];

        /// <summary>
        /// Gets the voice service.
        /// </summary>
        /// <value>
        /// The voice.
        /// </value>
        public VoiceService Voice => (VoiceService)_inputServices[Constants.VoiceService];

        /// <summary>
        /// The input services
        /// </summary>
        private Dictionary<string, InputService> _inputServices;

        /// <summary>
        /// Gets the center coordinates.
        /// </summary>
        /// <value>
        /// The center coordinates.
        /// </value>
        public Point CenterCoordinates
        {
            get
            {
                if (Window.ClientBounds != window)
                {
                    window = Window.ClientBounds;
                    centerCoordinates = new Point(WindowWidth / 2, WindowHeight / 2);
                }

                return centerCoordinates;
            }
        }

        /// <summary>
        /// Gets the current frame.
        /// </summary>
        /// <value>
        /// The current frame.
        /// </value>
        public ulong CurrentFrame { get; private set; }

        /// <summary>
        /// Gets the settings.
        /// </summary>
        /// <value>
        /// The settings.
        /// </value>
        public Constants Settings => Constants.Settings;

        /// <summary>
        /// Setups the Input system with the default services and engines, as requested
        /// </summary>
        /// <param name="enableMouseService">if set to <c>true</c> [enable mouse service].</param>
        /// <param name="enableKeyboardService">if set to <c>true</c> [enable keyboard service].</param>
        /// <param name="enableGamePadService">if set to <c>true</c> [enable game pad service].</param>
        /// <param name="enableTouchService">if set to <c>true</c> [enable touch service].</param>
        public void Setup(bool enableMouseService = true, bool enableKeyboardService = true, bool enableGamePadService = true, bool enableTouchService = true)
        {
            _inputServices = new Dictionary<string, InputService>();
            ResetInputSourcesWithDefaults(enableMouseService, enableKeyboardService, enableGamePadService, enableTouchService);
        }

        public void AddInputService(string name, InputService service, Type inputEngine = null, bool createEngine = true, Type serviceBaseType = null, Type engineBaseType = null)
        {
            if (serviceBaseType != null)
            {
                if (!service.GetType().IsSubclassOf(serviceBaseType))
                {
                    throw new Exception($"Invalid service! Services with the name [{name}] must inherit from [{serviceBaseType.ToString()}]!");
                }
            }
            if (engineBaseType != null)
            {
                if (!inputEngine.IsSubclassOf(engineBaseType))
                {
                    throw new Exception($"Invalid engine! Engines for the service with the name [{name}] must inherit from [{engineBaseType}]!");
                }
            }

            if (createEngine && inputEngine == null)
            {
                throw new Exception("Requested to create an engine and none is supplied!");
            }

            var engine = (InputEngine)Activator.CreateInstance(inputEngine);
            service.Setup(engine);

            _inputServices[name] = service;
        }

        public void SetGamePadService(TouchService service, Type inputEngine = null, bool createEngine = true)
        {
            AddInputService(Constants.GamePadService, service, inputEngine, createEngine, typeof(TouchService), typeof(TouchEngine));
        }

        public void SetKeyboardService(TouchService service, Type inputEngine = null, bool createEngine = true)
        {
            AddInputService(Constants.KeyboardService, service, inputEngine, createEngine, typeof(TouchService), typeof(TouchEngine));
        }

        public void SetMouseService(TouchService service, Type inputEngine = null, bool createEngine = true)
        {
            AddInputService(Constants.MouseService, service, inputEngine, createEngine, typeof(TouchService), typeof(TouchEngine));
        }

        public void SetTouchService(TouchService service, Type inputEngine = null, bool createEngine = true)
        {
            AddInputService(Constants.TouchService, service, inputEngine ?? typeof(XnaEngine), createEngine, typeof(TouchService), typeof(TouchEngine));
        }

        public void SetVoiceService(TouchService service, Type inputEngine = null, bool createEngine = true)
        {
            AddInputService(Constants.TouchService, service, inputEngine, createEngine, typeof(TouchService), typeof(TouchEngine));
        }

        /// <summary>
        /// Setups the Input system with the default services and engines, as requested
        /// </summary>
        /// <param name="defaultMouseService">if set to <c>true</c> [enable mouse service].</param>
        /// <param name="defaultKeyboardService">if set to <c>true</c> [enable keyboard service].</param>
        /// <param name="defaultGamePadService">if set to <c>true</c> [enable game pad service].</param>
        /// <param name="defaultTouchService">if set to <c>true</c> [enable touch service].</param>
        public void ResetInputSourcesWithDefaults(bool defaultMouseService = true, bool defaultKeyboardService = true, bool defaultGamePadService = true, bool defaultTouchService = true)
        {
            if (defaultMouseService)
            {
                _inputServices.Add(Constants.MouseService, new MouseService(this));
                Mouse.Setup();
            }

            if (defaultKeyboardService)
            {
                _inputServices.Add(Constants.KeyboardService, new KeyboardService(this));
                Keyboard.Setup();
            }

            if (defaultGamePadService)
            {
                _inputServices.Add(Constants.GamePadService, new GamePadService(this));
                GamePad.Setup();
            }

            if (defaultTouchService)
            {
                SetTouchService(new DefaultTouchService(this), typeof(XnaEngine), true);
            }
        }

        /// <summary>
        /// Updates the specified game time.
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        public void Update(GameTime gameTime)
        {
            // update core variables
            CurrentTime = gameTime;
            CurrentFrame++;

            // update input services if they exist and we want to update them
            foreach (var service in _inputServices)
            {
                service.Value.Update();
            }

            // update all tracked input conditions
            foreach (var item in TrackedConditions)
            {
                item.Value.IsConditionMet();
            }
        }

        /// <summary>
        /// Adds the input condition to tracking for event-driven behavior.
        /// </summary>
        /// <param name="condition">The condition.</param>
        /// <param name="name">The name.</param>
        /// <param name="layerDepth">The layer depth.</param>
        /// <param name="forceAdd">if set to <c>true</c> [force add].</param>
        /// <returns>Whether the input condition was added to tracking or not.</returns>
        public (bool, int, string) AddInputConditionToTracking(InputCondition condition, string name = null, int layerDepth = int.MaxValue, bool forceAdd = false)
        {
            if (name == null)
            {
                name = $"Condition_{ConditionIndex++}";
            }

            return TrackedConditions.AddItemAndGetMetadata(name, condition, layerDepth, forceAdd);
        }

        /// <summary>
        /// The total number of input conditions tracked.
        /// </summary>
        /// <returns>The total number of input conditions tracked.</returns>
        public long TotalInputConditionsTracked()
        {
            return TrackedConditions.Count;
        }

        /// <summary>
        /// Removes an input condition from tracking.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public (string, InputCondition, int) RemoveInputConditionFromTracking(string name)
        {
            return TrackedConditions.PopItem(name);
        }

        /// <summary>
        /// Removes an input condition from tracking.
        /// </summary>
        /// <param name="layerDepth">The layer depth.</param>
        /// <returns></returns>
        public (string, InputCondition, int) RemoveInputConditionFromTracking(int layerDepth)
        {
            return TrackedConditions.PopItem(layerDepth);
        }
    }
}
