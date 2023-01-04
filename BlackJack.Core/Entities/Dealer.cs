namespace Blackjack.Core.Entities
{
    public sealed class Dealer
    {
        public string Name { get; set; }

        public Dealer() {
          
   
        }

        public DealerHand Hand { get; set; }

        public int Position => 0;
    }
}
