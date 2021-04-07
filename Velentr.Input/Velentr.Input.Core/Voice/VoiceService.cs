using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace Velentr.Input.Voice
{

    public class VoiceService : InputService
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
        /// The engine
        /// </summary>
        private VoiceEngine _engine;

        /// <summary>
        /// Initializes a new instance of the <see cref="VoiceService"/> class.
        /// </summary>
        /// <param name="inputManager">The input manager.</param>
        public VoiceService(InputManager inputManager) : base(inputManager)
        {

        }

        public bool Enabled => _engine != null && _engine.Enabled;

        /// <summary>
        /// Setups this instance.
        /// </summary>
        public override void Setup()
        {
            _phrasesToRemove = new ConcurrentBag<string>();
            _availablePhrases = new Dictionary<string, RecognizedPhrase>();
            _phraseLifespans = new Dictionary<string, uint>();
            _engine = Constants.Settings.VoiceEngine;
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

            _engine?.Update();
        }

        /// <summary>
        /// Consumes the specified phrase.
        /// </summary>
        /// <param name="phrase">The phrase.</param>
        public void Consume(string phrase)
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
        public bool IsConsumed(string phrase)
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
        public bool IsPhraseSaid(string phrase)
        {
            return _availablePhrases.TryGetValue(phrase, out var _);
        }

        /// <summary>
        /// Adds the phrase.
        /// </summary>
        /// <param name="phrase">The phrase.</param>
        /// <param name="maxPhraseRecognizedLifespanMilliseconds">The maximum phrase recognized lifespan milliseconds.</param>
        public void AddPhrase(string phrase, uint maxPhraseRecognizedLifespanMilliseconds = 2000)
        {
            _engine?.AddCommand(phrase);
            _phraseLifespans.Add(phrase, maxPhraseRecognizedLifespanMilliseconds);
        }

        /// <summary>
        /// Adds the phrases.
        /// </summary>
        /// <param name="phrases">The phrases.</param>
        public void AddPhrases(List<(string, uint)> phrases)
        {
            _engine?.AddCommands(phrases.Select(x => x.Item1).ToList());

            for (var i = 0; i < phrases.Count; i++)
            {
                _phraseLifespans.Add(phrases[i].Item1, phrases[i].Item2);
            }
        }

        /// <summary>
        /// Removes the phrase.
        /// </summary>
        /// <param name="phrase">The phrase.</param>
        public void RemovePhrase(string phrase)
        {
            if (_availablePhrases.ContainsKey(phrase))
            {
                _availablePhrases[phrase].Consumed = true;
                _phrasesToRemove.Add(phrase);
                _phraseLifespans.Remove(phrase);
            }

            _engine?.RemovePhrase(phrase);
        }

        /// <summary>
        /// Removes the phrases.
        /// </summary>
        /// <param name="phrases">The phrases.</param>
        public void RemovePhrases(List<string> phrases)
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

            _engine?.RemovePhrases(phrases);
        }

        /// <summary>
        /// Phrases the recognized.
        /// </summary>
        /// <param name="phrase">The phrase.</param>
        public void PhraseRecognized(string phrase)
        {
            _availablePhrases[phrase] = new RecognizedPhrase(phrase, false, Manager.CurrentTime);
        }
    }

}
