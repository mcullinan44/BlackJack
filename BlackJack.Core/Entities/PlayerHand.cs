using BlackJack.Core;

namespace Blackjack.Core.Entities
{
    public class PlayerHand : Hand
    {
        public event GameEvents.CardReceived OnCardReceived;
        public event GameEvents.WinHand OnWinHand;
        public event GameEvents.LoseHand OnLoseHand;
        public event GameEvents.PushHand OnPushHand;
        public event GameEvents.Blackjack OnBlackjack;
        public event GameEvents.Bust OnBust;

        public event GameEvents.Activate OnActivate;
        public event GameEvents.TakeCardForSplit OnTakeCardForSplit;

        private State _state = State.NotYetPlayed;

        public PlayerHand(Player player, GameController gameController)
            : base(gameController)
        {            
            Controller = gameController;
            Player = player;
        }

        public State State
        {
            get => this._state;
            set {
                this._state = value;
                if(this._state == State.Playing)
                {
                    OnActivate?.Invoke(this, null);
                }
            }
        }

        public void Win()
        {
            Result = Result.Win;
            OnWinHand?.Invoke(this);
        }

        public void Lost()
        {
            Result = Result.Lost;
            OnLoseHand?.Invoke(this);
        }

        public void Push()
        {
            Result = Result.Push;
            OnPushHand?.Invoke(this);
        }
        
        public void RemoveCard(Card cardToRemove)
        {
            CardRemovedForSplitEventArgs args = new CardRemovedForSplitEventArgs(cardToRemove);
            this.Cards.Remove(cardToRemove);
            OnTakeCardForSplit?.Invoke(this, args);
        }
        
        public void Blackjack()
        {
            Result = Result.Blackjack;
            CardReceivedEventArgs args = new CardReceivedEventArgs(this, null);
            OnBlackjack?.Invoke(this, args);
        }

        public Player Player { get; set; }

        public Bet CurrentBet { get; set; }

        public void IncreaseBet(int amountToIncrease)
        {
            this.CurrentBet.IncreaseBase(amountToIncrease);
        }

        public void AddCard(Card card)
        {
            Cards.Add(card);
            CardReceivedEventArgs args = new CardReceivedEventArgs(this, card);
            OnCardReceived?.Invoke(this, args);
        }

        public override bool IsBust()
        {
            if (CurrentScore <= 21) return false;
            Result = Result.Bust;
            CardReceivedEventArgs args = new CardReceivedEventArgs(this, null);
            OnBust?.Invoke(this, args);
            return true;
        }

        public void Hit()
        {
            Controller.GivePlayerNextCardInShoe(this, false);
            if(IsBust())
            {
                Controller.PlayOutTheGame(this.Player);
            }
        }

        public void DoubleDown()
        {
            State = State.Doubled;
            Controller.IncreaseBet(this, this.CurrentBet.Amount);
            Controller.GivePlayerNextCardInShoe(this, false);
            IsBust();
            Controller.PlayOutTheGame(Player);
        }

        public void Stand()
        {
            State = State.Stand;
            Controller.PlayOutTheGame(this.Player);
        }
    }
}
