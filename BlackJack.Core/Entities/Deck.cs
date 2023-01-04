using System;
using System.Collections.Generic;
using System.Drawing;

namespace Blackjack.Core.Entities
{
    public class Deck
    {
        private readonly List<Card> _cardList;
        
        public Deck() {
            _cardList = new List<Card>();
            List<Suit> suitList = new List<Suit>
            {
                Suit.Clubs,
                Suit.Diamonds,
                Suit.Hearts,
                Suit.Spades
            };
            foreach (Suit suit in suitList)
            {
                for (int i = 2; i <= 14; i++)
                {
                    int value;
                    CardType type;
                    if (i <= 10)
                    {
                        value = i;
                        type = CardType.Numeric;
                    }
                    else
                    {
                        value = i <= 13 ? 10 : 11;
                        type = (i == 11 ? CardType.Jack : i == 12 ? CardType.Queen : i == 13 ? CardType.King : CardType.Ace);
                    }
                    _cardList.Add(new Card(suit, value, type));
                }
            }
        }

        public void Shuffle()
        {
            Random rand = new Random(DateTime.Now.Millisecond);
            for(int i = 0; i < _cardList.Count; i++)
            {
                int pos = rand.Next(_cardList.Count - 1);
                Card thisCard = _cardList[i];
                thisCard.IsDealt = false;
                Card replaceCard = _cardList[pos];
                replaceCard.IsDealt = false;
                _cardList[i] = replaceCard;
                _cardList[pos] = thisCard;
            }
        }

        public List<Card> CardList => _cardList;
    }
}
