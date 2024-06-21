namespace Blackjack.Core.Entities
{
    public sealed class Bet
    {
        public Bet(PlayerHand hand)
        {
            this.Hand = hand;
        }

        public PlayerHand Hand { get; set; }

        public int Amount { get; set; } = 0;
    }
}
