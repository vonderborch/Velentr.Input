using System.Collections.Generic;

namespace Velentr.Input.Voice
{

    public abstract class VoiceService : InputService
    {

        /// <summary>
        /// Gets or sets the engine.
        /// </summary>
        /// <value>
        /// The engine.
        /// </value>
        public VoiceEngine Engine { get; protected set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="VoiceService"/> class.
        /// </summary>
        /// <param name="inputManager">The input manager.</param>
        protected VoiceService(InputManager inputManager) : base(inputManager)
        {
        }

        public bool Enabled => Engine != null && Engine.Enabled;

        /// <summary>
        /// Consumes the specified phrase.
        /// </summary>
        /// <param name="phrase">The phrase.</param>
        public abstract void Consume(string phrase);

        /// <summary>
        /// Determines whether the specified phrase is consumed.
        /// </summary>
        /// <param name="phrase">The phrase.</param>
        /// <returns>
        ///   <c>true</c> if the specified phrase is consumed; otherwise, <c>false</c>.
        /// </returns>
        public abstract bool IsConsumed(string phrase);

        /// <summary>
        /// Determines whether [is phrase said] [the specified phrase].
        /// </summary>
        /// <param name="phrase">The phrase.</param>
        /// <returns>
        ///   <c>true</c> if [is phrase said] [the specified phrase]; otherwise, <c>false</c>.
        /// </returns>
        public abstract bool IsPhraseSaid(string phrase);

        /// <summary>
        /// Adds the phrase.
        /// </summary>
        /// <param name="phrase">The phrase.</param>
        /// <param name="maxPhraseRecognizedLifespanMilliseconds">The maximum phrase recognized lifespan milliseconds.</param>
        public abstract void AddPhrase(string phrase, uint maxPhraseRecognizedLifespanMilliseconds = 2000);

        /// <summary>
        /// Adds the phrases.
        /// </summary>
        /// <param name="phrases">The phrases.</param>
        public abstract void AddPhrases(List<(string, uint)> phrases);

        /// <summary>
        /// Removes the phrase.
        /// </summary>
        /// <param name="phrase">The phrase.</param>
        public abstract void RemovePhrase(string phrase);

        /// <summary>
        /// Removes the phrases.
        /// </summary>
        /// <param name="phrases">The phrases.</param>
        public abstract void RemovePhrases(List<string> phrases);

        /// <summary>
        /// Phrases the recognized.
        /// </summary>
        /// <param name="phrase">The phrase.</param>
        public abstract void PhraseRecognized(string phrase);

    }

}
