namespace Blackjack.Core.Entities
{
    public class Stats
    {
        public Stats(PlayerHand playerHand)
        {
            this.PlayerHand = playerHand;
            this.Score = playerHand.CurrentScore;
            this.BetAmount = playerHand.CurrentBet.Amount;
            this.Result = Entities.Result.Undetermined;
        }
        public PlayerHand PlayerHand { get; set; }
        public int Score { get; set; }
        public double BetAmount { get; set;  }
        public Result? Result { get; set; }
        public double Bankroll { get; set;  }
    }

    public enum Result
    {
        Undetermined,
        Blackjack,
        Win,
        Push,
        Lost,
        Bust
    }

    public enum State
    {
        NotYetPlayed,
        Playing,
        Stand,
        Doubled
    }

    public class ShoeStat
    {
        public string CardString { get; set; }

        public bool IsDealt { get; set; }

        public int DeckNumber { get; set; }
    }
}
