using Microsoft.Xna.Framework;
using Velentr.Input.Conditions;
using Velentr.Input.GamePad;
using Velentr.Input.Helpers;
using Velentr.Input.Keyboard;
using Velentr.Input.Mouse;
using Velentr.Input.Touch;
using Velentr.Input.Voice;

namespace Velentr.Input
{
    /// <summary>
    /// The core of the input system
    /// </summary>
    public sealed class VelentrInput
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
        public Cache<string, InputCondition> TrackedConditions = new Cache<string, InputCondition>();

        static VelentrInput() { }

        private VelentrInput() { }

        /// <summary>
        /// Gets the system.
        /// </summary>
        /// <value>
        /// The system.
        /// </value>
        public static VelentrInput System { get; } = new VelentrInput();

        /// <summary>
        /// Gets the game.
        /// </summary>
        /// <value>
        /// The game.
        /// </value>
        public static Game Game { get; private set; }

        /// <summary>
        /// Gets the current time.
        /// </summary>
        /// <value>
        /// The current time.
        /// </value>
        public static GameTime CurrentTime { get; private set; }

        /// <summary>
        /// Gets the window.
        /// </summary>
        /// <value>
        /// The window.
        /// </value>
        public static GameWindow Window => Game.Window;

        /// <summary>
        /// Gets a value indicating whether this instance is window active.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is window active; otherwise, <c>false</c>.
        /// </value>
        public static bool IsWindowActive => Game.IsActive;

        /// <summary>
        /// Gets the width of the window.
        /// </summary>
        /// <value>
        /// The width of the window.
        /// </value>
        public static int WindowWidth => Window.ClientBounds.Width;

        /// <summary>
        /// Gets the height of the window.
        /// </summary>
        /// <value>
        /// The height of the window.
        /// </value>
        public static int WindowHeight => Window.ClientBounds.Height;

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
        public MouseService Mouse { get; private set; }

        /// <summary>
        /// Gets the keyboard service.
        /// </summary>
        /// <value>
        /// The keyboard.
        /// </value>
        public KeyboardService Keyboard { get; private set; }

        /// <summary>
        /// Gets the game pad service.
        /// </summary>
        /// <value>
        /// The game pad.
        /// </value>
        public GamePadService GamePad { get; private set; }

        /// <summary>
        /// Gets the touch service.
        /// </summary>
        /// <value>
        /// The touch.
        /// </value>
        public TouchService Touch { get; private set; }

        /// <summary>
        /// Gets the voice service.
        /// </summary>
        /// <value>
        /// The voice.
        /// </value>
        public VoiceService Voice { get; private set; }

        /// <summary>
        /// Gets the center coordinates.
        /// </summary>
        /// <value>
        /// The center coordinates.
        /// </value>
        public static Point CenterCoordinates
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
        public static ulong CurrentFrame { get; private set; }

        /// <summary>
        /// Gets the settings.
        /// </summary>
        /// <value>
        /// The settings.
        /// </value>
        public Constants Settings => Constants.Settings;

        /// <summary>
        /// Setups the Input system with the requested input services.
        /// </summary>
        /// <param name="game">The game.</param>
        /// <param name="enableMouseService">if set to <c>true</c> [enable mouse service].</param>
        /// <param name="enableKeyboardService">if set to <c>true</c> [enable keyboard service].</param>
        /// <param name="enableGamePadService">if set to <c>true</c> [enable game pad service].</param>
        /// <param name="enableTouchService">if set to <c>true</c> [enable touch service].</param>
        public void Setup(Game game, bool enableMouseService = true, bool enableKeyboardService = true, bool enableGamePadService = true, bool enableTouchService = true, bool enableVoiceService = true)
        {
            Game = game;
            SetupInputSources(enableMouseService, enableKeyboardService, enableGamePadService, enableTouchService, enableVoiceService);
        }

        /// <summary>
        /// Setups the input services.
        /// </summary>
        /// <param name="enableMouseService">if set to <c>true</c> [enable mouse service].</param>
        /// <param name="enableKeyboardService">if set to <c>true</c> [enable keyboard service].</param>
        /// <param name="enableGamePadService">if set to <c>true</c> [enable game pad service].</param>
        /// <param name="enableTouchService">if set to <c>true</c> [enable touch service].</param>
        /// <param name="enableVoiceService">if set to <c>true</c> [enable voice service].</param>
        public void SetupInputSources(bool enableMouseService = true, bool enableKeyboardService = true, bool enableGamePadService = true, bool enableTouchService = true, bool enableVoiceService = true)
        {
            if (enableMouseService)
            {
                Mouse = new MouseService();
                Mouse.Setup();
            }

            if (enableKeyboardService)
            {
                Keyboard = new KeyboardService();
                Keyboard.Setup();
            }

            if (enableGamePadService)
            {
                GamePad = new GamePadService();
                GamePad.Setup();
            }

            if (enableTouchService)
            {
                Touch = new TouchService();
                Touch.Setup();
            }

            if (enableVoiceService)
            {
                //Voice = new VoiceService();
                //Voice.Setup();
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
            Mouse?.Update();
            Keyboard?.Update();
            GamePad?.Update();
            Touch?.Update();
            //Voice?.Update();

            // update all tracked input conditions
            foreach (var item in TrackedConditions)
            {
                item.Item2.IsConditionMet();
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

            return TrackedConditions.AddItem(name, condition, layerDepth, forceAdd);
        }

        /// <summary>
        /// The total number of input conditions tracked.
        /// </summary>
        /// <returns>The total number of input conditions tracked.</returns>
        public int TotalInputConditionsTracked()
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
            return TrackedConditions.RemoveItem(name);
        }

        /// <summary>
        /// Removes an input condition from tracking.
        /// </summary>
        /// <param name="layerDepth">The layer depth.</param>
        /// <returns></returns>
        public (string, InputCondition, int) RemoveInputConditionFromTracking(int layerDepth)
        {
            return TrackedConditions.RemoveItem(layerDepth);
        }

    }

}
