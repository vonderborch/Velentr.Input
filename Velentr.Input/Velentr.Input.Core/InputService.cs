using Velentr.Input.Enums;

namespace Velentr.Input
{

    public abstract class InputService
    {

        protected InputManager Manager;

        public InputService(InputManager inputManager)
        {
            Manager = inputManager;
        }

        public InputSource Source { get; protected set; }

        public abstract void Setup(InputEngine engine);

        public abstract void Update();

    }

}
