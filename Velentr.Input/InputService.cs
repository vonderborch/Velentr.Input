using Velentr.Input.Enums;

namespace Velentr.Input
{

    /// <summary>
    /// Defines the base methods available for an Input Service
    /// </summary>
    public abstract class InputService
    {

        /// <summary>
        /// The manager
        /// </summary>
        protected InputManager Manager;

        /// <summary>
        /// Initializes a new instance of the <see cref="InputService"/> class.
        /// </summary>
        /// <param name="inputManager">The input manager.</param>
        protected InputService(InputManager inputManager)
        {
            Manager = inputManager;
        }

        /// <summary>
        /// Gets or sets the source.
        /// </summary>
        /// <value>
        /// The source.
        /// </value>
        public InputSource Source { get; protected set; }

        /// <summary>
        /// Sets up the input service.
        /// </summary>
        /// <param name="engine">The engine to setup the input service with.</param>
        public void Setup(InputEngine engine)
        {
            engine.Setup(Manager);
            SetupInternal(engine);
        }

        /// <summary>
        /// Sets up the input service.
        /// </summary>
        /// <param name="engine">The engine to setup the input service with.</param>
        protected abstract void SetupInternal(InputEngine engine);

        /// <summary>
        /// Updates the input service.
        /// </summary>
        public abstract void Update();

    }

}
