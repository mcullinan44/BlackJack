using System;

namespace Blackjack.Core.Entities
{
    public static class GameEvents
    {
        public delegate void OnBankrollChange(object sender, OnBankrollChangedEventArgs e);
        public delegate void OnShowAllCards(object sender, EventArgs e);
        public delegate void OnActivate(object sender, EventArgs e);
        public delegate void OnCardReceived(object sender, OnCardReceivedEventArgs args);

        public delegate void OnTakeCardForSplit(object sender, OnCardRemovedForSplitEventArgs e);
        public delegate void OnDealerCardReceived(object sender, OnCardReceivedEventArgs args);
        public delegate void OnBust(object sender, OnCardReceivedEventArgs args);
        public delegate void OnBlackjack(object sender, OnCardReceivedEventArgs args);
        public delegate void OnWinHand(Hand hand);
        public delegate void OnLoseHand(Hand hand);
        public delegate void OnDealerBust(object sender, OnCardReceivedEventArgs args);
        public delegate void OnDealerBlackjack(object sender, OnCardReceivedEventArgs args);
        public delegate void OnDealerWinHand(Hand hand);
        public delegate void OnDealerLoseHand(Hand hand);
        public delegate void OnPushHand(Hand hand);
        public delegate void OnGameEnd(object sender, EventArgs e);
        public delegate void OnBetChanged(object sender, OnBetChangedEventArgs args);
        public delegate void OnShuffle(object sender, EventArgs e);
    }

    public class OnCardRemovedForSplitEventArgs : EventArgs
    {
        public OnCardRemovedForSplitEventArgs(Card card)
        {
            this.Card = card;
        }

        public Card Card { get; }
    }

    public class OnCardReceivedEventArgs : EventArgs
    {
        public OnCardReceivedEventArgs(Hand hand, Card card)
        {
            this.Hand = hand;
            this.Card = card;
        }

        public Hand Hand { get; }

        public Card Card { get; }
    }

    public class OnBankrollChangedEventArgs: EventArgs
    {
        public OnBankrollChangedEventArgs(Player player)
        {
            this.Player = player;
        }
        public Player Player { get; }
    }

    public class OnBetChangedEventArgs : EventArgs
    {
        public OnBetChangedEventArgs(Bet bet)
        {
            this.Bet = bet;
        }

        public Bet Bet { get; }
    }
}
