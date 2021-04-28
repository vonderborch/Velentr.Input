namespace Velentr.Input.EventArguments
{

    public class VoiceEventArguments : ConditionEventArguments
    {

        public string Phrase { get; internal set; }

        public override ConditionEventArguments Clone()
        {
            return (VoiceEventArguments) MemberwiseClone();
        }

    }

}
