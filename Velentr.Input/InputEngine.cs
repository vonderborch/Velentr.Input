namespace Velentr.Input
{

    /// <summary>
    /// Defines an abstract input engine, which drive handling inputs internally for a particular service
    /// </summary>
    public abstract class InputEngine
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="InputEngine"/> class.
        /// </summary>
        protected InputEngine()
        {
        }

        /// <summary>
        /// Gets or sets the input manager this engine is associated with.
        /// </summary>
        /// <value>
        /// The manager.
        /// </value>
        protected InputManager Manager { get; set; }

        /// <summary>
        /// Sets up the InputEngine
        /// </summary>
        /// <param name="manager">The manager.</param>
        public void Setup(InputManager manager)
        {
            Manager = manager;

            SetupInternal();
        }

        /// <summary>
        /// Sets up the InputEngine
        /// </summary>
        protected abstract void SetupInternal();

        /// <summary>
        /// Updates the InputEngine.
        /// </summary>
        public abstract void Update();

    }
}
