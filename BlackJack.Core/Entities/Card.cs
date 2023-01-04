namespace Blackjack.Core.Entities
{
    public sealed class Card
    {
        public Card(Suit suit, int pointValue, CardType type) 
        {
            this.CardSuit = suit;
            this.Value = pointValue;
            this.CardType = type;
            this.IsDealt = false;
            this.Index = type == CardType.Numeric ? pointValue : type == CardType.Jack ? 11 : type == CardType.Queen ? 12 : type == CardType.King ? 13 : 1;
        }

        public Suit CardSuit { get; }

        public int Value { get; set; }

        public CardType CardType { get; set; }

        public bool IsDealt
        {
            get;
            set; 
        }

        public int Index
        {
            get;
            set;
        }

        public override string ToString()
        {
            return  (CardType == CardType.Ace ? "A" : CardType == CardType.Jack ? "J" : CardType == CardType.Queen ? "Q" : CardType == CardType.King ? "K" : Value.ToString()) + " " +
                (CardSuit == Suit.Clubs ? "\u2663" : CardSuit == Suit.Spades ? "\u2660" : CardSuit == Suit.Diamonds ? "\u2666" : "\u2665");
        }
    }
}
