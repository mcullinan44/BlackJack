using System;
using System.Collections.Generic;

namespace Blackjack.Core.Entities
{
    public sealed class Shoe
    {
        private readonly List<Deck> _deckList;
        
        public Shoe(int numberOfDecks)
        {
            _deckList = new List<Deck>(numberOfDecks);
            while (_deckList.Count < numberOfDecks)
            {
                var deck = new Deck();
               
                _deckList.Add(deck);
            }
        }

        public IList<Deck> DeckList => _deckList;

        private Card GetCard(CardType type)
        {
            Card topCard = null;
            bool isFound = false;
            foreach (var deck in _deckList)
            {
                if (isFound)
                    break;
                foreach (var card in deck.CardList)
                {
                    if (!card.IsDealt && card.CardType == type)
                    {
                        topCard = card;
                        topCard.IsDealt = true;
                        isFound = true;
                        break;
                    }
                }
            }

            if (topCard == null)
                throw new ArgumentNullException("the card is null", "the card is null");
            return topCard;
        }

        public Card GetALow()
        {
            return GetCard(CardType.Numeric);
        }

        public Card GetAnAce()
        {
            return GetCard(CardType.Ace);
        }

        public Card GetAFace()
        {
            return GetCard(CardType.Jack);
        }

        public Card SplitTester(Card prevCard)
        {
            Card topCard = null;
            bool isFound = false;
            foreach (Deck deck in _deckList)
            {
                if (isFound)
                    break;
                foreach (var card in deck.CardList)
                {
                    if (!card.IsDealt && card.Index == prevCard.Index )
                    {
                        topCard = card;
                        topCard.IsDealt = true;
                        isFound = true;
                        break;
                    }
                }
            }
            if (topCard == null)
                throw new ArgumentNullException("the card is null", "the card is null");
            return topCard;
        }

        public Card NextCard
        {
            get
            {
                Card topCard = null;
                bool isFound = false;
                foreach (Deck deck in _deckList)
                {
                    if (isFound)
                        break;
                    foreach (var card in deck.CardList)
                    {
                        if (!card.IsDealt)
                        {
                            topCard = card;
                            topCard.IsDealt = true;
                            isFound = true;
                            break;
                        }
                    }
                }

                if (topCard == null)
                    throw new ArgumentNullException("the card is null", "the card is null");
                return topCard;
            }
        }

        public List<Card> DealtCards
        {
            get
            {
                List<Card> list = new List<Card>();
                this._deckList.ForEach(i => i.CardList.ForEach(card => { if(card.IsDealt) list.Add(card); }));
                return list;
            }
        }

        public List<Card> UndealtCards
        {
            get
            {
                List<Card> list = new List<Card>();
                this._deckList.ForEach(i => i.CardList.ForEach(card => { if (!card.IsDealt) list.Add(card); }));
                return list;
            }
        }
    }
}
