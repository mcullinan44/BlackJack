namespace Blackjack.Core.Entities
{
    public sealed class Chip
    {
        public Chip(ChipDenomination chipDenomination)
        {
            Value = chipDenomination;
        }

        public ChipDenomination Value { get; }
    }

    public enum ChipDenomination
    {
        One = 1,
        Five = 5,
        Ten = 10,
        TwentyFive = 25,
        Fifty = 50,
        OneHundred = 100
    }
}
