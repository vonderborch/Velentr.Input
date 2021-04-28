using System;
using Microsoft.Xna.Framework;

namespace Velentr.Input.Voice
{
    public class RecognizedPhrase
    {

        public RecognizedPhrase(string phrase, bool consumed, TimeSpan timeStamp)
        {
            Phrase = phrase;
            Consumed = consumed;
            PhraseSaid = timeStamp;
        }

        public bool Consumed { get; set; }

        public string Phrase { get; }

        public TimeSpan PhraseSaid { get; }
    }
}
