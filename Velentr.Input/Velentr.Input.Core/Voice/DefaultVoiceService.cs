using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace Velentr.Input.Voice
{

    public class DefaultVoiceService : VoiceService
    {

        /// <summary>
        /// The phrases to remove
        /// </summary>
        private ConcurrentBag<string> _phrasesToRemove;

        /// <summary>
        /// The available phrases
        /// </summary>
        private Dictionary<string, RecognizedPhrase> _availablePhrases;

        /// <summary>
        /// The phrase lifespans
        /// </summary>
        private Dictionary<string, uint> _phraseLifespans;

        /// <summary>
        /// Initializes a new instance of the <see cref="VoiceService"/> class.
        /// </summary>
        /// <param name="inputManager">The input manager.</param>
        public DefaultVoiceService(InputManager inputManager) : base(inputManager)
        {

        }

        /// <summary>
        /// Sets up the input service.
        /// </summary>
        /// <param name="engine">The engine to setup the input service with.</param>
        protected override void SetupInternal(InputEngine engine)
        {
            _phrasesToRemove = new ConcurrentBag<string>();
            _availablePhrases = new Dictionary<string, RecognizedPhrase>();
            _phraseLifespans = new Dictionary<string, uint>();
            Engine = (VoiceEngine)engine;
        }

        /// <summary>
        /// Updates this instance.
        /// </summary>
        public override void Update()
        {
            // clean up our available phrases
            if (_availablePhrases.Count > 0)
            {
                // scan through all recognized phrases and remove ones that have reached their max lifespan
                if (_availablePhrases.Count > 0)
                {
                    foreach (var phrase in _availablePhrases)
                    {
                        if (Helpers.Helper.ElapsedMilliSeconds(phrase.Value.PhraseSaid, Manager.CurrentTime) >= _phraseLifespans[phrase.Key])
                        {
                            _phrasesToRemove.Add(phrase.Key);
                        }
                    }
                }

                // remove any phrases that we have used!
                if (_phrasesToRemove.Count > 0)
                {
                    foreach (var phrase in _phrasesToRemove)
                    {
                        _availablePhrases.Remove(phrase);
                    }
                    _phrasesToRemove = new ConcurrentBag<string>();
                }
            }

            // if this still has items left in it, let's clean it up too
            if (_phrasesToRemove.Count > 0)
            {
                _phrasesToRemove = new ConcurrentBag<string>();
            }

            Engine?.Update();
        }

        /// <summary>
        /// Consumes the specified phrase.
        /// </summary>
        /// <param name="phrase">The phrase.</param>
        public override void Consume(string phrase)
        {
            if (_availablePhrases.ContainsKey(phrase))
            {
                _availablePhrases[phrase].Consumed = true;
                _phrasesToRemove.Add(phrase);
            }
        }

        /// <summary>
        /// Determines whether the specified phrase is consumed.
        /// </summary>
        /// <param name="phrase">The phrase.</param>
        /// <returns>
        ///   <c>true</c> if the specified phrase is consumed; otherwise, <c>false</c>.
        /// </returns>
        public override bool IsConsumed(string phrase)
        {
            // if we didn't see the phrase, we'll claim that it was already consumed
            return !_availablePhrases.TryGetValue(phrase, out var result) || result.Consumed;
        }

        /// <summary>
        /// Determines whether [is phrase said] [the specified phrase].
        /// </summary>
        /// <param name="phrase">The phrase.</param>
        /// <returns>
        ///   <c>true</c> if [is phrase said] [the specified phrase]; otherwise, <c>false</c>.
        /// </returns>
        public override bool IsPhraseSaid(string phrase)
        {
            return _availablePhrases.TryGetValue(phrase, out var _);
        }

        /// <summary>
        /// Adds the phrase.
        /// </summary>
        /// <param name="phrase">The phrase.</param>
        /// <param name="maxPhraseRecognizedLifespanMilliseconds">The maximum phrase recognized lifespan milliseconds.</param>
        public override void AddPhrase(string phrase, uint maxPhraseRecognizedLifespanMilliseconds = 2000)
        {
            Engine?.AddCommand(phrase);
            _phraseLifespans.Add(phrase, maxPhraseRecognizedLifespanMilliseconds);
        }

        /// <summary>
        /// Adds the phrases.
        /// </summary>
        /// <param name="phrases">The phrases.</param>
        public override void AddPhrases(List<(string, uint)> phrases)
        {
            Engine?.AddCommands(phrases.Select(x => x.Item1).ToList());

            for (var i = 0; i < phrases.Count; i++)
            {
                _phraseLifespans.Add(phrases[i].Item1, phrases[i].Item2);
            }
        }

        /// <summary>
        /// Removes the phrase.
        /// </summary>
        /// <param name="phrase">The phrase.</param>
        public override void RemovePhrase(string phrase)
        {
            if (_availablePhrases.ContainsKey(phrase))
            {
                _availablePhrases[phrase].Consumed = true;
                _phrasesToRemove.Add(phrase);
                _phraseLifespans.Remove(phrase);
            }

            Engine?.RemovePhrase(phrase);
        }

        /// <summary>
        /// Removes the phrases.
        /// </summary>
        /// <param name="phrases">The phrases.</param>
        public override void RemovePhrases(List<string> phrases)
        {
            for (var i = 0; i < phrases.Count; i++)
            {
                if (_availablePhrases.ContainsKey(phrases[i]))
                {
                    _availablePhrases[phrases[i]].Consumed = true;
                    _phrasesToRemove.Add(phrases[i]);
                    _phraseLifespans.Remove(phrases[i]);
                }
            }

            Engine?.RemovePhrases(phrases);
        }

        /// <summary>
        /// Phrases the recognized.
        /// </summary>
        /// <param name="phrase">The phrase.</param>
        public override void PhraseRecognized(string phrase)
        {
            _availablePhrases[phrase] = new RecognizedPhrase(phrase, false, Manager.CurrentTime);
        }
    }

}
