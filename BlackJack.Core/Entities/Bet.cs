namespace Blackjack.Core.Entities
{
    public sealed class Bet
    {
        public Bet(PlayerHand hand)
        {
            this.Hand = hand;
        }

        public PlayerHand Hand { get; set; }

        public double Amount { get; set; } = 0;
    }
}
