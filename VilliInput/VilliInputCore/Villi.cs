using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using VilliInput.Conditions;
using VilliInput.MouseInput;

namespace VilliInput
{
    public sealed class Villi
    {
        private MouseService mouseService;

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

        public void Setup(Game game, bool enableMouseService = true, bool enableKeyboardService = true, bool enableGamePadService = true, bool enableTouchService = true)
        {
            Game = game;
            SetupInputSources(enableMouseService, enableKeyboardService, enableGamePadService, enableTouchService);
        }

        public void SetupInputSources(bool enableMouseService = true, bool enableKeyboardService = true, bool enableGamePadService = true, bool enableTouchService = true)
        {
            mouseService = enableMouseService ? new MouseService() : null;
        }

        public void Update(GameTime gameTime)
        {
            // update core variables
            CurrentTime = gameTime;
            CurrentFrame++;

            // update input services if they exist and we want to update them
            Mouse?.Update();

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
                    }
                }
            }
        }

        public void AddEventDrivenConditionForScanning(InputCondition condition, bool consumable, bool ignoreConsumed, EventWatchingMethod methodsToWatch)
        {
            if (!eventDrivenConditions.ContainsKey(methodsToWatch))
            {
                eventDrivenConditions.Add(methodsToWatch, new List<Tuple<InputCondition, bool, bool>>());
            }
            eventDrivenConditions[methodsToWatch].Add(new Tuple<InputCondition, bool, bool>(condition, consumable, ignoreConsumed));
        }
    }
}
