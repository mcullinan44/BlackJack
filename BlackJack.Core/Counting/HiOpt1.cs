using Blackjack.Core.Entities;

namespace Blackjack.Core.Counting
{
    public class HighOpt1 : BaseCardCountingStrategy
    {
        public HighOpt1(Shoe shoe) : base(shoe) { }

        public override string StrategyName => "High-Opt1";

        public override int GetValueForCard(Card card)
        {
            int value = 0;
            switch (card.Index)
            {
                case 2:
                    value = 0;
                    break;
                case 3:
                case 4:
                case 5:
                case 6:
                    value = 1;
                    break;
                case 7:
                case 8:
                case 9:
                    value = 0;
                    break;
                case 10:
                case 11:
                case 12:
                case 13:
                    value = -1;
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
