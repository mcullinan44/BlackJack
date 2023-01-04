using Blackjack.Core.Entities;

namespace Blackjack.Core.Counting
{
    public class HighOpt2 : BaseCardCountingStrategy
    {
        public HighOpt2(Shoe shoe) : base(shoe) { }

        public override string StrategyName => "High-Opt II";

        public override int GetValueForCard(Card card)
        {
            int value = 0;
            switch (card.Index)
            {
                case 2:
                case 3:
                    value = 1;
                    break;
                case 4:
                case 5:
                    value = 2;
                    break;
                case 6:
                case 7:
                    value = 1;
                    break;
                case 8:
                case 9:
                    value = 0;
                    break;
                case 10:
                case 11:
                case 12:
                case 13:
                    value = -2;
                    break;
                case 1:
                    value = 0;
                    break;
                default:
                    value = 0;
                    break;
            }
            return value;
        }
    }

}
