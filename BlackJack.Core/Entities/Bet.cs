using BlackJack.Core;

namespace Blackjack.Core.Entities
{
    public sealed class Bet
    {
        public event GameEvents.BetChanged OnBetChanged;

        private Guid _guid;

        public Bet(Guid newGuid)
        {
            this._guid = newGuid;
        }

        public int Amount { get; set; } = 0;


        public void IncreaseBase(int amountToIncrease)
        {
            this.Amount += amountToIncrease;
            BetChangedEventArgs args = new BetChangedEventArgs(this);
            OnBetChanged?.Invoke(this, args);
        }
    }
}
