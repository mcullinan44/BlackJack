namespace Blackjack.Core.Entities
{
    public class PlayerHand : Hand
    {
        public event GameEvents.OnCardReceived OnCardReceived;
        public event GameEvents.OnWinHand OnWinHand;
        public event GameEvents.OnLoseHand OnLoseHand;
        public event GameEvents.OnPushHand OnPushHand;
        public event GameEvents.OnBlackjack OnBlackjack;
        public event GameEvents.OnBust OnBust;
        public event GameEvents.OnBetChanged OnBetChanged;
        public event GameEvents.OnActivate OnActivate;
        public event GameEvents.OnTakeCardForSplit OnTakeCardForSplit;

        private State _state = State.NotYetPlayed;

        public PlayerHand(Player player, GameController gameController)
            : base(gameController)
        {            
            Controller = gameController;
            Player = player;
            CurrentBet = new Bet(this);
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

        public void Blackjack()
        {
            Result = Result.Blackjack;
            OnCardReceivedEventArgs args = new OnCardReceivedEventArgs(this, null);
            OnBlackjack?.Invoke(this, args);
        }

        public Player Player { get; set; }

        public Bet CurrentBet { get; set; }

        public void IncreaseBet(double amountToIncrease)
        {
            CurrentBet.Amount += amountToIncrease;
            OnBetChangedEventArgs args = new OnBetChangedEventArgs(CurrentBet);
            OnBetChanged?.Invoke(this, args);
        }

        public void AddCard(Card card)
        {
            Cards.Add(card);
            OnCardReceivedEventArgs args = new OnCardReceivedEventArgs(this, card);
            OnCardReceived.Invoke(this, args);
        }

        public override bool CheckIsBust()
        {
            if (CurrentScore <= 21) return false;
            Result = Result.Bust;
            OnCardReceivedEventArgs args = new OnCardReceivedEventArgs(this, null);
            OnBust?.Invoke(this, args);
            return true;
        }

        public void Hit()
        {
            Controller.GivePlayerNextCardInShoe(this, false);
            if(CheckIsBust())
            {
                Controller.FinishHand(this.Player);
            }
        }

        public void DoubleDown()
        {
            State = State.Doubled;
            Controller.IncreaseBet(this, this.CurrentBet.Amount);
            Controller.GivePlayerNextCardInShoe(this, false);
            CheckIsBust();
            Controller.FinishHand(Player);
        }

        public void Stand()
        {
            State = State.Stand;
            Controller.FinishHand(this.Player);
        }
    }
}
