namespace VilliInput.OLD
{
    public struct InputValueLogic
    {
        public InputValue ConditionalValue { get; private set; }

        public Comparison Comparator { get; private set; }

        public InputValueLogic(InputValue conditionalValue, Comparison comparator)
        {
            ConditionalValue = conditionalValue;
            Comparator = comparator;
        }
    }
}
