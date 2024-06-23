using BlackJack.Core;
using System.Runtime.CompilerServices;
using static BlackJack.Core.GameEvents;

namespace Blackjack.Core.Entities
{
    public sealed class Bet
    {
        public event GameEvents.OnBetChanged OnBetChanged;

        private Guid _guid;

        public Bet(Guid newGuid)
        {
            this._guid = newGuid;
        }




        public int Amount { get; set; } = 0;


        public void IncreaseBase(int amountToIncrease)
        {
            this.Amount += amountToIncrease;
            OnBetChangedEventArgs args = new OnBetChangedEventArgs(this);
            OnBetChanged?.Invoke(this, args);
        }
    }
}
