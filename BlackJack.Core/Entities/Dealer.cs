namespace Blackjack.Core.Entities
{
    public sealed class Dealer
    {
        public string Name { get; set; }

        public Dealer() {
          
            Hand = new DealerHand();
        }

        public DealerHand Hand { get; set; }

        public int Position => 0;
    }
}
