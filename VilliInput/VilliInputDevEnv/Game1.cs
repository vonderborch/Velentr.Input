using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using VilliInput;
using VilliInput.Conditions;
using VilliInput.EventArguments;
using VilliInput.GamePad;
using VilliInput.Keyboard;
using VilliInput.Mouse;
using ButtonState = Microsoft.Xna.Framework.Input.ButtonState;

namespace VilliInputDevEnv
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

            Villi.System.Setup(this);
            var condition = new AnyCondition(
                new KeyboardButtonPressedCondition(Key.Escape),
                new GamePadButtonPressedCondition(GamePadButton.Back),
                new MouseButtonPressedCondition(MouseButton.MiddleButton)
            );
            //condition.Event.Event += ExitGame;
            condition.Event += ExitGame;
            Villi.System.AddInputConditionToTracking(condition);
        }

        protected override void Update(GameTime gameTime)
        {
            Villi.System.Update(gameTime);

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        public void ExitGame(object sender, VilliEventArguments args)
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
