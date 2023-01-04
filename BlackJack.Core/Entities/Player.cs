using System.Collections.Generic;

namespace Blackjack.Core.Entities
{
    public sealed class Player
    {
        public Player(int position)
        {
            CurrentHands = new List<PlayerHand>();
            Position = position;
        }

        public bool IsActive { get; set; }

        public string Name { get; set; }

        public List<PlayerHand> CurrentHands { get; set; }

        public double PlayerbankRoll { get; set; } = 0;

        public int Position { get; }

        public PlayerHand ActiveHand
        {
            get { return this.CurrentHands.Find(i => i.State == State.Playing); }
        }
    }
}
