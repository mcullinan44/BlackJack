using BlackJack.Core;

namespace Blackjack.Core.Entities
{
    public class DealerHand : Hand
    {
        public event GameEvents.DealerCardReceived OnDealerCardReceived;
        public event GameEvents.DealerWinHand OnDealerWinHand;
        public event GameEvents.DealerLoseHand OnDealerLoseHand;
        public event GameEvents.DealerBlackjack OnDealerBlackjack;
        public event GameEvents.DealerBust OnDealerBust;
        public event GameEvents.PushHand OnPushHand;

        public DealerHand(GameController gameController)
            : base(gameController)
        {
            this.Controller = gameController;
            this.Controller.Dealer.Hand = this;
        }

        public void Win()
        {
            OnDealerWinHand?.Invoke(this);
        }

        public void Lost()
        {
            OnDealerLoseHand?.Invoke(this);
        }

        public void Push()
        {
            Result = Result.Push;
            OnPushHand?.Invoke(this);
        }

        public void AddCard(Card card)
        {
            Cards.Add(card);
            CardReceivedEventArgs args = new CardReceivedEventArgs(this, card);
            OnDealerCardReceived?.Invoke(this, args);
        }

        public void Blackjack()
        {
            Result = Result.Blackjack;
            CardReceivedEventArgs args = new CardReceivedEventArgs(this, null);
            OnDealerBlackjack?.Invoke(this, args);
        }

        public override bool IsBust()
        {
            if (CurrentScore <= 21) return false;
            Result = Result.Bust;
            CardReceivedEventArgs args = new CardReceivedEventArgs(this, null);
            OnDealerBust?.Invoke(this, args);
            return true;

        }
    }
}
