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
InputManager manager = new InputManager(this);

// This part goes in Setup()
manager.Setup();
condition = new AnyCondition(
    manager,
    new KeyboardButtonPressedCondition(manager, Key.Escape),
    new GamePadButtonPressedCondition(manager, GamePadButton.Back),
    new MouseButtonPressedCondition(manager, MouseButton.MiddleButton)
);

// This part goes in Update()
manager.Update(gameTime);
if (condition.ConditionMet()) {
    // Take action!
}
```

Approach 2: Event-driven
```
// This goes in the class
InputManager manager = new InputManager(this);

// This part goes in Setup()
manager.Setup();
var condition = new AnyCondition(
    manager,
    new KeyboardButtonPressedCondition(manager, Key.Escape),
    new GamePadButtonPressedCondition(manager, GamePadButton.Back),
    new MouseButtonPressedCondition(manager, MouseButton.MiddleButton)
);
condition.Event += ExitGame;
manager.AddInputConditionToTracking(condition);

// This part goes in Update()
manager.Update(gameTime);

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
            manager.Setup();
            var condition = new AnyCondition(
                manager,
                new KeyboardButtonPressedCondition(manager, Key.Escape),
                new GamePadButtonPressedCondition(manager, GamePadButton.Back),
                new MouseButtonPressedCondition(manager, MouseButton.MiddleButton)
            );
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

```


# Future Plans
See list of issues under the Milestones: https://github.com/vonderborch/Velentr.Input/milestones
