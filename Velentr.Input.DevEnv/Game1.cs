using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Velentr.Input.Conditions;
using Velentr.Input.EventArguments;
using Velentr.Input.GamePad;
using Velentr.Input.Keyboard;
using Velentr.Input.Mouse;
using Velentr.Input.Voice;
#if SYSTEM_SPEECH
using Velentr.Input.Voice.SystemSpeech;
#endif

namespace Velentr.Input.DevEnv
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private InputManager manager;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            manager = new InputManager(this);
#if SYSTEM_SPEECH
            manager.Setup();
            manager.SetVoiceService(new DefaultVoiceService(manager), typeof(SystemSpeechEngine), true);
            var condition = new AnyCondition(
                manager,
                new KeyboardButtonPressedCondition(manager, Key.Escape),
                new GamePadButtonPressedCondition(manager, GamePadButton.Back),
                new MouseButtonPressedCondition(manager, MouseButton.MiddleButton),
                new VoiceCommandCondition(manager, "Exit")
            );
#else
            manager.Setup();
            var condition = new AnyCondition(
                manager,
                new KeyboardButtonPressedCondition(manager, Key.Escape),
                new GamePadButtonPressedCondition(manager, GamePadButton.Back),
                new MouseButtonPressedCondition(manager, MouseButton.MiddleButton)
            );
#endif
            condition.Event += ExitGame;
            manager.AddInputConditionToTracking(condition);
        }

        protected override void Update(GameTime gameTime)
        {
            manager.Update(gameTime);
            base.Update(gameTime);
        }

        public void ExitGame(object sender, ConditionEventArguments args)
        {
            Exit();
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
