namespace Velentr.Input.EventArguments
{

    public class KeyboardKeysPressedCountEventArguments : ConditionEventArguments
    {

        public int NumberOfKeysPressed { get; internal set; }

        public int NumberOfKeysPressedDelta { get; internal set; }

        public override ConditionEventArguments Clone()
        {
            return (KeyboardKeysPressedCountEventArguments) MemberwiseClone();
        }

    }

}
