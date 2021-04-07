using System;
using Velentr.Input.EventArguments;
using Velentr.Input.Helpers;

namespace Velentr.Input.Conditions
{

    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Velentr.Input.Conditions.InputCondition" />
    public class VoiceCommandCondition : InputCondition, IDisposable
    {
        private bool disposedValue;

        /// <summary>
        /// Initializes a new instance of the <see cref="VoiceCommandCondition"/> class.
        /// </summary>
        /// <param name="manager">The manager.</param>
        /// <param name="phrase">The phrase to recognize.</param>
        /// <param name="maxPhraseRecognizedLifespanMilliseconds">The maximum time a recognized phrase will be remembered, in milliseconds.</param>
        /// <param name="windowMustBeActive">if set to <c>true</c> [window must be active].</param>
        /// <param name="consumable">if set to <c>true</c> [consumable].</param>
        /// <param name="allowedIfConsumed">if set to <c>true</c> [allowed if consumed].</param>
        /// <param name="milliSecondsForTimeOut">The milli seconds for time out.</param>
        public VoiceCommandCondition(InputManager manager, string phrase, uint maxPhraseRecognizedLifespanMilliseconds = 2000, bool windowMustBeActive = true, bool consumable = true, bool allowedIfConsumed = false, uint milliSecondsForTimeOut = 0) : base(manager, Enums.InputSource.Voice, windowMustBeActive, consumable, allowedIfConsumed, 0, milliSecondsForTimeOut)
        {
            Phrase = phrase;

            if (manager.Voice == null || !manager.Voice.Enabled)
            {
                throw new Exception("No voice engine is configured!");
            }
            manager.Voice?.AddPhrase(phrase);
        }

        /// <summary>
        /// Gets the phrase.
        /// </summary>
        /// <value>
        /// The phrase.
        /// </value>
        public string Phrase { get; }

        /// <summary>
        /// Internal method to determine if the conditions are met or not.
        /// </summary>
        /// <param name="consumable">if set to <c>true</c> [consumable].</param>
        /// <param name="allowedIfConsumed">if set to <c>true</c> [allowed if consumed].</param>
        /// <returns></returns>
        public override bool InternalConditionMet(bool consumable, bool allowedIfConsumed)
        {
            var valid = Manager.Voice.IsPhraseSaid(Phrase);
            if (valid && !ConditionMetState)
            {
                UpdateState(true);
            }
            else if (!valid && ConditionMetState)
            {
                UpdateState(false);
            }

            if (ActionValid(allowedIfConsumed, MilliSecondsForConditionMet))
            {
                ConditionMetCleanup(consumable, GetArguments());
                return true;
            }

            return false;
        }

        /// <summary>
        /// Internals the get value.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        protected override Value InternalGetValue()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Consumes the input.
        /// </summary>
        public override void Consume()
        {
            Manager.Voice?.Consume(Phrase);
        }

        /// <summary>
        /// Determines whether the input is consumed.
        /// </summary>
        /// <returns>
        ///   <c>true</c> if the input is consumed; otherwise, <c>false</c>.
        /// </returns>
        public override bool IsConsumed()
        {
            return Manager.Voice?.IsConsumed(Phrase) ?? true;
        }

        /// <summary>
        /// Gets the arguments to provide to events that are fired.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public override ConditionEventArguments GetArguments()
        {
            return new VoiceEventArguments
            {
                Phrase = Phrase,
                Condition = this,
                InputSource = InputSource,
                MilliSecondsForConditionMet = MilliSecondsForConditionMet,
                ConditionStateStartTime = CurrentStateStart,
                ConditionStateTimeMilliSeconds = Helper.ElapsedMilliSeconds(CurrentStateStart, Manager.CurrentTime),
                WindowMustBeActive = WindowMustBeActive
            };
        }

        /// <summary>
        /// Checks to see if the input is valid.
        /// </summary>
        /// <param name="allowedIfConsumed">if set to <c>true</c> [allowed if consumed].</param>
        /// <param name="milliSecondsForConditionMet">The milli seconds for condition met.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        protected override bool ActionValid(bool allowedIfConsumed, uint milliSecondsForConditionMet)
        {
            return (
                ((WindowMustBeActive && Manager.IsWindowActive) || !WindowMustBeActive)
                && (allowedIfConsumed || !IsConsumed())
                && (milliSecondsForConditionMet == 0 || Helper.ElapsedMilliSeconds(CurrentStateStart, Manager.CurrentTime) >= milliSecondsForConditionMet)
                && (MilliSecondsForTimeOut == 0 || Helper.ElapsedMilliSeconds(LastFireTime, Manager.CurrentTime) >= MilliSecondsForTimeOut)
            );
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    Manager.Voice.RemovePhrase(Phrase);
                }

                disposedValue = true;
            }
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

    }
}
