using System.Collections.Generic;

namespace Blackjack.Core.Entities
{
    public abstract class Hand
    {
        protected GameController Controller;

        protected Hand(GameController controller)
        {
            Cards = new List<Card>();
            Result = new Result();
            Controller = controller;
        }

        public List<Card> Cards { get; }

        public int CardCount => Cards.Count;
        
       public int CurrentScore
       {
           get
           {
               int runningTotal = 0;
               Cards.ForEach((i) =>
               {
                   runningTotal += i.Value;
               });

                //treat Aces as 1 if current score is over 21
               if(runningTotal > 21)
               {
                   foreach (Card card in Cards)
                   {
                       if (card.CardType == CardType.Ace)
                       {
                           runningTotal -= 10;
                       }

                       if (runningTotal < 21)
                           break;
                   }
               }

               return runningTotal;
           }
          
       }

        public abstract bool CheckIsBust();
        
        public Result Result { get; set; }
    }
}
