using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Velentr.Input;
using Velentr.Input.Conditions;
using Velentr.Input.EventArguments;
using Velentr.Input.GamePad;
using Velentr.Input.Keyboard;
using Velentr.Input.Mouse;

namespace Velentr.Input.FNA.DevEnv
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

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

            VelentrInput.System.Setup(this);
            var condition = new AnyCondition(
                new KeyboardButtonPressedCondition(Key.Escape),
                new GamePadButtonPressedCondition(GamePadButton.Back),
                new MouseButtonPressedCondition(MouseButton.MiddleButton)
            );
            condition.Event += ExitGame;
            VelentrInput.System.AddInputConditionToTracking(condition);
        }

        protected override void Update(GameTime gameTime)
        {
            VelentrInput.System.Update(gameTime);
            base.Update(gameTime);
        }

        public void ExitGame(object sender, ConditionEventArguments args)
        {
            Exit();
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            base.Draw(gameTime);
        }
    }
}
