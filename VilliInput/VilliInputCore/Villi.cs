using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using VilliInput.Conditions;
using VilliInput.Helpers;
using VilliInput.Mouse;

namespace VilliInput
{
    public sealed class Villi
    {
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

        public Cache<string, InputCondition> TrackedConditions = new Cache<string, InputCondition>();

        public MouseService Mouse { get; private set; }

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

        public Constants Settings => Constants.Settings;

        public void Setup(Game game, bool enableMouseService = true, bool enableKeyboardService = true, bool enableGamePadService = true, bool enableTouchService = true)
        {
            Game = game;
            SetupInputSources(enableMouseService, enableKeyboardService, enableGamePadService, enableTouchService);
        }

        public void SetupInputSources(bool enableMouseService = true, bool enableKeyboardService = true, bool enableGamePadService = true, bool enableTouchService = true)
        {
            if (enableMouseService)
            {
                Mouse = new MouseService();
                Mouse.Setup();
            }

            if (enableKeyboardService)
            {

            }

            if (enableGamePadService)
            {

            }

            if (enableTouchService)
            {

            }
        }

        public void Update(GameTime gameTime)
        {
            // update core variables
            CurrentTime = gameTime;
            CurrentFrame++;

            // update input services if they exist and we want to update them
            Mouse?.Update();

            // update all tracked input conditions
            foreach (var item in TrackedConditions)
            {
                item.Item2.ConditionMet();
            }
        }

        public bool AddInputConditionToTracking(string name, InputCondition condition, int layerDepth = int.MaxValue, bool forceAdd = false)
        {
            return TrackedConditions.AddItem(name, condition, layerDepth, forceAdd) != null;
        }

        public int TotalInputConditionsTracked()
        {
            return TrackedConditions.Count;
        }

        public (string, InputCondition, int) RemoveInputConditionFromTracking(string name)
        {
            return TrackedConditions.RemoveItem(name);
        }

        public (string, InputCondition, int) RemoveInputConditionFromTracking(int layerDepth)
        {
            return TrackedConditions.RemoveItem(layerDepth);
        }
    }
}
