namespace VilliInput.EventArguments
{

    public class KeyboardKeysPressedCountEventArguments : VilliEventArguments
    {

        public int NumberOfKeysPressed { get; internal set; }

        public int NumberOfKeysPressedDelta { get; internal set; }

        public override VilliEventArguments Clone()
        {
            return (KeyboardKeysPressedCountEventArguments) MemberwiseClone();
        }

    }

}
