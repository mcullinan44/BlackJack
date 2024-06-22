using BlackJack.Core;
using static BlackJack.Core.GameEvents;

namespace Blackjack.Core.Entities
{
    public class DealerHand : Hand
    {
        public event GameEvents.OnDealerCardReceived OnDealerCardReceived;
        public event GameEvents.OnDealerWinHand OnDealerWinHand;
        public event GameEvents.OnDealerLoseHand OnDealerLoseHand;
        public event GameEvents.OnDealerBlackjack OnDealerBlackjack;
        public event GameEvents.OnDealerBust OnDealerBust;
        public event GameEvents.OnPushHand OnPushHand;

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
            OnCardReceivedEventArgs args = new OnCardReceivedEventArgs(this, card);
            OnDealerCardReceived?.Invoke(this, args);
        }

        public void Blackjack()
        {
            Result = Result.Blackjack;
            OnCardReceivedEventArgs args = new OnCardReceivedEventArgs(this, null);
            OnDealerBlackjack?.Invoke(this, args);
        }

        public override bool Bust()
        {
            if (CurrentScore <= 21) return false;
            Result = Result.Bust;
            OnCardReceivedEventArgs args = new OnCardReceivedEventArgs(this, null);
            OnDealerBust?.Invoke(this, args);
            return true;

        }
    }
}
