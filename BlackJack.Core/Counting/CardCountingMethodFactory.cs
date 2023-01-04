using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blackjack.Core.Entities;

namespace Blackjack.Core.Counting
{
    public class CardCountingMethodFactory
    {
        public static BaseCardCountingStrategy GetStrategy(CardCountingStrategy strategyLookup, Shoe shoe)
        {
            BaseCardCountingStrategy result = null;
            switch (strategyLookup)
            {
                case CardCountingStrategy.HiLo:
                    result = new HighLo(shoe);
                    break;
                case CardCountingStrategy.HiOp1:
                    result = new HighOpt1(shoe);
                    break;
                case CardCountingStrategy.HiOpt2:
                    result = new HighOpt2(shoe);
                    break;
                case CardCountingStrategy.Ko:
                    result = new Ko(shoe);
                    break;
                case CardCountingStrategy.Omega3:
                    result = new OmegaIii(shoe);
                    break;
                case CardCountingStrategy.ZenCount:
                    result = new ZenCount(shoe);
                    break;
                case CardCountingStrategy.Red7:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(strategyLookup), strategyLookup, null);
            }
            return result;
        }
    }
}
