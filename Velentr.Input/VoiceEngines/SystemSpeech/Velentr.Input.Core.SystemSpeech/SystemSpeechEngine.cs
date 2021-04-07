using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Speech.Recognition;

namespace Velentr.Input.Voice.SystemSpeech
{
    public class SystemSpeechEngine : VoiceEngine
    {

        private ConcurrentBag<string> _currentPhrases;

        private Dictionary<string, bool> _phraseChanges;

        private bool _hasMicrophone = true;

        private SpeechRecognizer _recognizer;

        private Choices _choices;

        public SystemSpeechEngine(InputManager manager) : base(manager)
        {
            _recognizer = new SpeechRecognizer();
            _choices = new Choices();
            _recognizer.SpeechRecognized += PhraseRecognized;

            _phraseChanges = new Dictionary<string, bool>();
            _currentPhrases = new ConcurrentBag<string>();
        }

        public override bool Enabled
        {
            get => _recognizer.Enabled && _hasMicrophone;
            set
            {
                try
                {
                    _recognizer.Enabled = value;
                    _hasMicrophone = value;
                }
                catch
                {
                    _hasMicrophone = false;
                }
            }
        }

        public override void AddCommand(string phrase)
        {
            _choices.Add(phrase);
            _phraseChanges[phrase] = true;

        }

        public override void AddCommands(List<string> phrases)
        {
            for (var i = 0; i < phrases.Count; i++)
            {
                _choices.Add(phrases[i]);
                _phraseChanges[phrases[i]] = true;
            }
        }

        public override void RemovePhrase(string phrase)
        {
            _phraseChanges[phrase] = false;
        }

        public override void RemovePhrases(List<string> phrases)
        {
            for (var i = 0; i < phrases.Count; i++)
            {
                _phraseChanges[phrases[i]] = false;
            }
        }

        public override void Update()
        {
            if (_phraseChanges.Count > 0)
            {
                UpdateRecognizer();
            }
        }

        private void UpdateRecognizer()
        {
            var newChoices = new Choices();
            var newCurrentPhrases = new ConcurrentBag<string>();

            var phrasesToAdd = _phraseChanges.Where(x => x.Value);
            var phrasesToRemove = _phraseChanges.Where(x => !x.Value).ToDictionary(x => x.Key, x => x.Value);

            foreach (var phrase in phrasesToAdd)
            {
                newChoices.Add(phrase.Key);
                newCurrentPhrases.Add(phrase.Key);
            }
            foreach (var phrase in _currentPhrases)
            {
                if (!phrasesToRemove.ContainsKey(phrase))
                {
                    newChoices.Add(phrase);
                    newCurrentPhrases.Add(phrase);
                }
            }

            _choices = newChoices;
            _currentPhrases = newCurrentPhrases;
            _phraseChanges = new Dictionary<string, bool>();

            var gb = new GrammarBuilder(_choices);
            var g = new Grammar(gb);
            _recognizer.UnloadAllGrammars();
            _recognizer.LoadGrammar(g);
        }

        private void PhraseRecognized(object sender, SpeechRecognizedEventArgs args)
        {
            Manager.Voice.PhraseRecognized(args.Result.Text);
        }
    }
}
