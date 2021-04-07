# Velentr.Input
A simple and easy-to-use input library for XNA/Monogame/FNA.

# Installation
There are nuget packages available for Monogame and FNA.
- **Monogame**: [Velentr.Input.Monogame](https://www.nuget.org/packages/Velentr.Input.Monogame/)
- **FNA**: [Velentr.Input.FNA](https://www.nuget.org/packages/Velentr.Input.FNA/)

# Plugins
Additional plugin libraries are available:
- **[System.Speech](https://www.nuget.org/packages/System.Speech/) Voice Recognition Support**:
  - **Monogame**: [Velentr.Input.Monogame.SystemSpeech](https://www.nuget.org/packages/Velentr.Input.Monogame.SystemSpeech/)
  - **FNA**: [Velentr.Input.FNA.SystemSpeech](https://www.nuget.org/packages/Velentr.Input.FNA.SystemSpeech/)

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

# Plugging into the System
Velentr.Input is flexible and allows you to modify specific behaviors, add support for new platforms, or new input methods entirely easily!

### Need a new type of input not already covered by what is built in?
Create a new input system (your new class should inherit from `InputService`), then call `manager.AddInputService(serviceName, newServiceClassInstance)` to enable it.

### Want to change the behavior of how an existing system works internally?
##### Want to change how things are handled from a service standpoint (how we mark consumed inputs, etc.)?
Built-in Input System | Abstract Implementation | Default Implementation | Manager Method to call to assign
--------------------- | ----------------------- | ---------------------- | --------------------------------
GamePad | `GamePadService` | `DefaultGamePadService` | `manager.SetGamePadService(newGamePadServiceObject)`
Keyboard | `KeyboardService` | `KeyboardDefaultService` | `manager.SetKeyboardService(newKeyboardServiceObject)`
Mouse | `MouseService` | `MouseDefaultService` | `manager.SetMouseService(newMouseServiceObject)`
Touch | `TouchService` | `DefaultTouchService` | `manager.SetTouchService(newTouchServiceObject)`
Voice | `VoiceService` | `DefaultVoiceService` | `manager.SetService(newVoiceServiceObject)`

##### Want to change how we read in inputs?
Create a new class that inherits from the `InputEngine` of the input type that you want to update.

Built-in Input System | Abstract Implementation | Default Implementation
--------------------- | ----------------------- | ---------------------- 
GamePad | `GamePadEngine` | `XnaGamePadEngine`
Keyboard | `KeyboardEngine` | `XnaKeyboardEngine`
Mouse | `MouseEngine` | `XnaMouseEngine`
Touch | `TouchEngine` | `XnaTouchEngine`
Voice | `VoiceEngine` | N/A (see **Plugins** for the default implementation)

##### Want to add a new condition?
Create a new condition by inheriting from the `InputCondition` class (or an existing class that implements `InputCondition`), then use your new condition as you would any other.

# Voice Recognition Notes:
Full support for voice commands is not built-in, although the basic systems to use it are built out. To use voice commands, you must download a Voice Input library compatible with your platform (see **Plugins** for currently supported speech recognition systems).

We went with this approach because voice support is more of a specialized requirement than the other default supported input methods.

Basic example of setting up/using voice commands (after the plugin has been installed):
```
// This goes in the class
InputManager manager = new InputManager(this);

// This part goes in Setup()
manager.Setup();
manager.SetVoiceService(new DefaultVoiceService(manager), typeof(SystemSpeechEngine), true);
var condition = new AnyCondition(
    manager,
    new KeyboardButtonPressedCondition(manager, Key.Escape),
    new GamePadButtonPressedCondition(manager, GamePadButton.Back),
    new MouseButtonPressedCondition(manager, MouseButton.MiddleButton),
    new VoiceCommandCondition(manager, "My Phrase or Word Here")
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
