# Velentr.Input
A simple and easy-to-use input library for XNA/Monogame/FNA.

# Installation
There are nuget packages available for Monogame and FNA.
- Monogame: [Velentr.Input.Monogame](https://www.nuget.org/packages/Velentr.Input.Monogame/)
- FNA: [Velentr.Input.FNA](https://www.nuget.org/packages/Velentr.Input.FNA/)

# Basic Usage
Approach 1: Create an input condition and poll for if the condition is met directly.
```
// This goes in the class
InputCondition condition;

// This part goes in Setup()
VelentrInput.System.Setup(this);
condition = new AnyCondition(
    new KeyboardButtonPressedCondition(Key.Escape),
    new GamePadButtonPressedCondition(GamePadButton.Back),
    new MouseButtonPressedCondition(MouseButton.MiddleButton)
);

// This part goes in Update()
VelentrInput.System.Update(gameTime);
if (condition.ConditionMet()) {
    // Take action!
}
```

Approach 2: Event-driven
```
// This goes in the class
InputCondition condition;

// This part goes in Setup()
VelentrInput.System.Setup(this);
var condition = new AnyCondition(
    new KeyboardButtonPressedCondition(Key.Escape),
    new GamePadButtonPressedCondition(GamePadButton.Back),
    new MouseButtonPressedCondition(MouseButton.MiddleButton)
);
condition.Event += methodToCallWhenConditionIsMet;
VelentrInput.System.AddInputConditionToTracking(condition);

// This part goes in Update()
VelentrInput.System.Update(gameTime);

```

# Example
Code:
```
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Velentr.Input.Conditions;
using Velentr.Input.EventArguments;
using Velentr.Input.GamePad;
using Velentr.Input.Keyboard;
using Velentr.Input.Mouse;

namespace Velentr.Input.Monogame.DevEnv
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

            // TODO: Add your update logic here

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

```


# Future Plans
See list of issues under the Milestones: https://github.com/vonderborch/Velentr.Input/milestones
