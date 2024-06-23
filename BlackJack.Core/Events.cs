using Blackjack.Core.Entities;

namespace BlackJack.Core
{
    public static class GameEvents
    {
        public delegate void BankrollChange(object sender, BankrollChangedEventArgs e);
        public delegate void ShowAllCards(object sender, EventArgs e);
        public delegate void Activate(object sender, EventArgs e);
        public delegate void CardReceived(object sender, CardReceivedEventArgs args);
        public delegate void TakeCardForSplit(object sender, CardRemovedForSplitEventArgs e);
        public delegate void DealerCardReceived(object sender, CardReceivedEventArgs args);
        public delegate void Bust(object sender, CardReceivedEventArgs args);
        public delegate void Blackjack(object sender, CardReceivedEventArgs args);
        public delegate void WinHand(Hand hand);
        public delegate void LoseHand(Hand hand);
        public delegate void DealerBust(object sender, CardReceivedEventArgs args);
        public delegate void DealerBlackjack(object sender, CardReceivedEventArgs args);
        public delegate void DealerWinHand(Hand hand);
        public delegate void DealerLoseHand(Hand hand);
        public delegate void PushHand(Hand hand);
        public delegate void GameEnd(object sender, EventArgs e);
        public delegate void BetChanged(object sender, BetChangedEventArgs args);
        public delegate void Shuffle(object sender, EventArgs e);
        public delegate void NewDeal(object sender, EventArgs e);
        public delegate void SitDownToPlay(object sender, EventArgs e);
    }

    public class CardRemovedForSplitEventArgs : EventArgs
    {
        public CardRemovedForSplitEventArgs(Card card)
        {
            this.Card = card;
        }

        public Card Card { get; }
    }

    public class CardReceivedEventArgs : EventArgs
    {
        public CardReceivedEventArgs(Hand hand, Card card)
        {
            this.Hand = hand;
            this.Card = card;
        }

        public Hand Hand { get; }

        public Card Card { get; }
    }

    public class BankrollChangedEventArgs: EventArgs
    {
        public BankrollChangedEventArgs(Player player)
        {
            this.Player = player;
        }
        public Player Player { get; }
    }

    public class BetChangedEventArgs : EventArgs
    {
        public BetChangedEventArgs(Bet bet)
        {
            this.Bet = bet;
        }

        public Bet Bet { get; }
    }
}
