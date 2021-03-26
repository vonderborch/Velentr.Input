using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using VilliInput.OLD.Conditions;
using VilliInput.OLD.GamePadInput;
using VilliInput.OLD.KeyboardInput;
using VilliInput.OLD.MouseInput;

namespace VilliInput.OLD
{
    public sealed class Villi
    {
        private MouseService mouseService;

        private KeyboardService keyboardService;

        private GamePadService gamePadService;

        private static Rectangle window;

        private static Point centerCoordinates;

        static Villi() { }
        private Villi() { }

        public static Villi System { get; } = new Villi();

        internal static Game Game { get; private set; }

        internal static GameTime CurrentTime { get; private set; }

        public static GameWindow Window => Game.Window;

        public static bool IsWindowActive => Game.IsActive;

        public static int WindowWidth => Window.ClientBounds.Width;

        public static int WindowHeight => Window.ClientBounds.Height;

        private Dictionary<EventWatchingMethod, List<Tuple<InputCondition, bool, bool>>> eventDrivenConditions = new Dictionary<EventWatchingMethod, List<Tuple<InputCondition, bool, bool>>>();

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

        public static ulong CurrentFrame { get; private set; }

        public MouseService Mouse => mouseService;

        public KeyboardService Keyboard => keyboardService;

        public GamePadService GamePad => gamePadService;

        public Constants Settings => Constants.Instance;

        public void Setup(Game game, bool enableMouseService = true, bool enableKeyboardService = true, bool enableGamePadService = true, bool enableTouchService = true)
        {
            Game = game;
            SetupInputSources(enableMouseService, enableKeyboardService, enableGamePadService, enableTouchService);
        }

        public void SetupInputSources(bool enableMouseService = true, bool enableKeyboardService = true, bool enableGamePadService = true, bool enableTouchService = true)
        {
            mouseService = enableMouseService ? new MouseService() : null;
            mouseService?.Setup();

            keyboardService = enableKeyboardService ? new KeyboardService() : null;
            keyboardService?.Setup();

            gamePadService = enableGamePadService ? new GamePadService() : null;
            gamePadService?.Setup();
        }

        public void Update(GameTime gameTime)
        {
            // update core variables
            CurrentTime = gameTime;
            CurrentFrame++;

            // update input services if they exist and we want to update them
            Mouse?.Update();
            Keyboard?.Update();
            GamePad?.Update();

            // Update any InputConditions we've been told to monitor to allow for Event-Driven Behavior
            if (eventDrivenConditions.Count > 0)
            {
                foreach (var eventWatching in eventDrivenConditions)
                {
                    foreach (var condition in eventWatching.Value)
                    {
                        if (eventWatching.Key.HasFlag(EventWatchingMethod.Pressed))
                        {
                            condition.Item1.Pressed(condition.Item2, condition.Item3);
                        }

                        if (eventWatching.Key.HasFlag(EventWatchingMethod.PressStarted))
                        {
                            condition.Item1.PressStarted(condition.Item2, condition.Item3);
                        }

                        if (eventWatching.Key.HasFlag(EventWatchingMethod.Released))
                        {
                            condition.Item1.Released(condition.Item2, condition.Item3);
                        }

                        if (eventWatching.Key.HasFlag(EventWatchingMethod.ReleaseStarted))
                        {
                            condition.Item1.ReleaseStarted(condition.Item2, condition.Item3);
                        }

                        if (eventWatching.Key.HasFlag(EventWatchingMethod.ValueValid))
                        {
                            condition.Item1.ValueValid();
                        }
                    }
                }
            }
        }

        public void AddEventDrivenConditionForScanning(InputCondition condition, EventWatchingMethod methodsToWatch, bool consumable = true, bool ignoreConsumed = false)
        {
            if (!eventDrivenConditions.ContainsKey(methodsToWatch))
            {
                eventDrivenConditions.Add(methodsToWatch, new List<Tuple<InputCondition, bool, bool>>());
            }
            eventDrivenConditions[methodsToWatch].Add(new Tuple<InputCondition, bool, bool>(condition, consumable, ignoreConsumed));
        }
    }
}
