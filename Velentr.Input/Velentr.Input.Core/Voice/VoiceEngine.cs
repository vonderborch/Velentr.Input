using System.Collections.Generic;

namespace Velentr.Input.Voice
{

    /// <summary>
    /// Defines the base methods and properties that are needed for Voice Input support.
    /// </summary>
    /// <seealso cref="Velentr.Input.InputEngine" />
    public abstract class VoiceEngine : InputEngine
    {

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="VoiceEngine"/> is enabled.
        /// </summary>
        /// <value>
        ///   <c>true</c> if enabled; otherwise, <c>false</c>.
        /// </value>
        public abstract bool Enabled { get; set; }

        /// <summary>
        /// Adds the command.
        /// </summary>
        /// <param name="phrase">The phrase.</param>
        public abstract void AddCommand(string phrase);

        /// <summary>
        /// Adds the commands.
        /// </summary>
        /// <param name="phrases">The phrases.</param>
        public abstract void AddCommands(List<string> phrases);

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

    }
}
