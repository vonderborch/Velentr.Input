using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using VilliInput.Conditions;
using VilliInput.GamePad;
using VilliInput.Helpers;
using VilliInput.Keyboard;
using VilliInput.Mouse;
using VilliInput.Touch;
using VilliInput.Voice;

namespace VilliInput
{
    public sealed class Villi
    {
        private static Rectangle window;

        private static Point centerCoordinates;

        static Villi() { }
        private Villi() { }

        public static Villi System { get; } = new Villi();

        public static Game Game { get; private set; }

        public static GameTime CurrentTime { get; private set; }

        public static GameWindow Window => Game.Window;

        public static bool IsWindowActive => Game.IsActive;

        public static int WindowWidth => Window.ClientBounds.Width;

        public static int WindowHeight => Window.ClientBounds.Height;

        public Cache<string, InputCondition> TrackedConditions = new Cache<string, InputCondition>();

        public ulong ConditionIndex { get; private set; } = 0;

        public MouseService Mouse { get; private set; }

        public KeyboardService Keyboard { get; private set; }

        public GamePadService GamePad { get; private set; }

        public TouchService Touch { get; private set; }

        public VoiceService Voice { get; private set; }

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
                item.Item2.ConditionMet();
            }
        }

        public bool AddInputConditionToTracking(InputCondition condition, string name = null, int layerDepth = int.MaxValue, bool forceAdd = false)
        {
            if (name == null)
            {
                name = $"Condition_{(ConditionIndex++)}";
            }

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
