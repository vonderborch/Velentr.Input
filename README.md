# VilliInput
A simple and easy-to-use input library for XNA/Monogame/FNA.

# Installation
There are nuget packages available for Monogame and FNA.
- Monogame: [Velentr.Input.Monogame](https://www.nuget.org/packages/Velentr.Input.Monogame/)
- FNA: [Velentr.Input.FNA](https://www.nuget.org/packages/Velentr.Input.FNA/)

# Usage
Approach 1: Draw Text Directly
```
var fontSize = 48;
// VelentrFont.Initialize(GraphicsDevice); // Optional, if not called you'll need to add the GraphicsDevice to the first Bragi.Core.GetFont call that is made to set the GraphicsDevice.
var font = VelentrFont.GetFont("pathToFontFile", fontSize, GraphicsDevice);
_spriteBatch.DrawString(font, "Hello World!", new Vector2(50, 50), Color.White);
```

Approach 2: Cache Text (is a bit quicker since we don't need to rebuild the text glyph list on subsequent calls)
```
var fontSize = 48;
// VelentrFont.Initialize(GraphicsDevice); // Optional, if not called you'll need to add the GraphicsDevice to the first Bragi.Core.GetFont call that is made to set the GraphicsDevice.
var font = VelentrFont.GetFont("pathToFontFile", fontSize, GraphicsDevice);
var text = font.MakeText("Hello World!");
_spriteBatch.DrawString(text, new Vector2(50, 50), Color.White);

```

# Example
Code:
```
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Velentr.Font.MonogameDevEnv
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private string testString = "Hello\nWorld! 123 () *&^$%#";
        private string testString2 = "I am a test string!";
        private string fontFile1 = "Content\\PlayfairDisplayRegular-ywLOY.ttf";
        private string fontFile2 = "Content\\Trueno-wml2.otf";
        private Text text1;
        private Text text2;


        Font font1;
        Font font2;

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
            VelentrFont.Core.Initialize(GraphicsDevice);

            font1 = VelentrFont.Core.GetFont(fontFile1, 80);
            text1 = font1.MakeText(testString);

            font2 = VelentrFont.Core.GetFont(fontFile2, 34);
            text2 = font2.MakeText(testString2);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);

            _spriteBatch.Begin();

            _spriteBatch.DrawString(font1, testString, new Vector2(0, -15), Color.Blue);
            _spriteBatch.DrawString(font1, testString, new Vector2(150, -15), Color.Pink, 0.1f, Vector2.Zero, new Vector2(.5f, .5f), SpriteEffects.None, 1f);
            _spriteBatch.DrawString(text2, new Vector2(50, 150), Color.Red);
            _spriteBatch.DrawString(text1, new Vector2(150, 300), Color.Black, 0.1f, Vector2.Zero, new Vector2(.5f, .5f), SpriteEffects.None, 1f);
            _spriteBatch.DrawString(text1, new Vector2(150, 300), Color.Black, 0.1f, new Vector2(50, 50), new Vector2(.5f, .5f), SpriteEffects.FlipVertically, 1f);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}

```

Screenshot:
![Screenshot](https://github.com/vonderborch/Velentr.Font/blob/main/Example.PNG?raw=true)


# Future Plans
See list of issues under the Milestones: https://github.com/vonderborch/Velentr.Font/milestones
