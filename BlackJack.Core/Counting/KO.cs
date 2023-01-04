using Blackjack.Core.Entities;

namespace Blackjack.Core.Counting
{
    public class Ko : BaseCardCountingStrategy
    {
        public Ko(Shoe shoe) : base(shoe) { }

        public override string StrategyName => "KO";

        public override int GetValueForCard(Card card)
        {
            int value = 0;
            switch (card.Index)
            {
                case 2:
                case 3:
                case 4:
                case 5:
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
                    value = -1;
                    break;
                case 1:
                    value = -1;
                    break;
                default:
                    value = 0;
                    break;
            }
            return value;
        }
    }

}
