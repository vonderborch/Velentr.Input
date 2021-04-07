using System.Collections.Generic;

namespace Velentr.Input.Voice
{
    public abstract class VoiceEngine : InputEngine
    {

        public abstract bool Enabled { get; set; }

        public abstract void AddCommand(string phrase);

        public abstract void AddCommands(List<string> phrases);

        public abstract void RemovePhrase(string phrase);

        public abstract void RemovePhrases(List<string> phrases);

    }
}
