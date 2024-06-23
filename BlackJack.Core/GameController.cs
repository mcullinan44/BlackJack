using BlackJack.Core;
using Blackjack.Core.Entities;

namespace Blackjack.Core
{
    public sealed class GameController
    {
        public event GameEvents.BankrollChange OnBankrollChange;
        public event GameEvents.ShowAllCards OnShowAllCards;
        public event GameEvents.GameEnd OnGameEnd;
        public event GameEvents.Shuffle OnShuffle;
        public event GameEvents.NewDeal OnNewDeal;
        public event GameEvents.SitDownToPlay OnSitDownToPlay;

        public GameController(int numberOfDecks)
        {
            this.PlayerList = new List<Player>();
            this.Shoe = new Shoe(numberOfDecks);
            this.Dealer = new Dealer();  
        }

        #region Properties

        public List<Player> PlayerList { get; }

        public Player ActivePlayer
        {
            get { return PlayerList.FirstOrDefault(i => i.IsActive);  }
        }

        public Dealer Dealer { get; }

        public int NumberOfDecks { get; set; }

        public int MinimumBet { get;  set;  }

        public Shoe Shoe
        {
            get;
            set;
        }

        #endregion

        #region Methods

        public void AddPlayer(Player player)
        {
            PlayerList.Add(player);
        }

        public void ShuffleAll()
        {
            foreach (var deck in Shoe.DeckList)
            {
                deck.Shuffle();
            }
            OnShuffle?.Invoke(this, null);
        }

        public void SetPlayerActive(Player player)
        {
            PlayerList.Find(i => i.Name == player.Name).IsActive = true;
        }

        public void SitDownToPlay()
        {
            foreach(Player player in PlayerList)
            {
                player.CurrentHands.Clear();
            }
            Dealer.Hand?.Cards.Clear();

            this.OnSitDownToPlay?.Invoke(this, null);
        }

        public DealerHand AddDealerHandToDealer()
        {
            this.Dealer.Hand = new DealerHand(this);
            return this.Dealer.Hand;
        }

        public PlayerHand AddHandToPlayer(Player player, State state, Bet bet)
        {
            PlayerHand result = new(player, this)
            {
                State = state
            };
            player.CurrentHands.Add(result);
            result.CurrentBet = bet;
            return result;
        }

        private void DealARoundOfCards()
        {
            foreach (Player player in PlayerList)
            {
                GivePlayerNextCardInShoe(player.ActiveHand, false);
            }
        }

        public void AdjustPlayerBankroll(Player player, double amount)
        {
            player.PlayerbankRoll = player.PlayerbankRoll + amount;
            BankrollChangedEventArgs args = new BankrollChangedEventArgs(player);
            OnBankrollChange?.Invoke(this, args);
        }

        public async Task DealAsync()
        {
            OnNewDeal?.Invoke(this, EventArgs.Empty);
            bool dealerBlackJack = false;

            await Task.Delay(500);
            DealARoundOfCards();

            await Task.Delay(500);
            GiveDealerACard();

            await Task.Delay(250);
            DealARoundOfCards();

            await Task.Delay(250);
            GiveDealerACard();

            //check if the dealer has blackjack.
            if (Dealer.Hand.CurrentScore == 21)
            {
                dealerBlackJack = true;
            }

            //check if any players have blackjack
            foreach (Player player in PlayerList)
            {
                if(player.ActiveHand.CurrentScore == 21)
                {
                    
                    //if both the dealer and the player have blackjack, then PUSH.
                    if(dealerBlackJack)
                    {
                        AdjustPlayerBankRoll(player, player.ActiveHand.CurrentBet.Amount);
                        player.ActiveHand.Push();
                    }
                    //Otherwise, the player wins 1.5x his original bet
                    else
                    {
                        player.ActiveHand.Blackjack();
                        

                        //TODO: Need an onPotchange event to denote the house giving the extra half

                        AdjustPlayerBankRoll(player, player.ActiveHand.CurrentBet.Amount * 2.5);
                    }
                }
                else //player does not have blackjack, but the dealer does. The player loses.
                {
                    if(dealerBlackJack)
                    {
                        //give dealer blackjack
                        Dealer.Hand.Blackjack();
                        //player loses
                        player.ActiveHand.Lost();
                    }
                }
            }

            bool moreToPlay = PlayerList.Any(i => i.CurrentHands.Any(x => x.Result == Result.Undetermined));
            if(!moreToPlay)
            {
                foreach (Player player in PlayerList)
                {
                    player.CurrentHands.Clear();
                }
                OnGameEnd?.Invoke(this, null);
            }
        }

        private void AdjustPlayerBankRoll(Player player, double amount)
        {
            player.PlayerbankRoll += amount;
            BankrollChangedEventArgs args = new BankrollChangedEventArgs(player);
            OnBankrollChange?.Invoke(this, args);
        }

        public void GivePlayerNextCardInShoe(PlayerHand playerHand, bool checkBlackJackImmediately, DealType dealType = DealType.Standard)
        {
            Card card;
            switch (dealType)
            {
                case DealType.Standard:
                    card = this.Shoe.NextCard;
                    break;
                case DealType.BlackJack when playerHand.Cards.Exists(i => i.CardType == CardType.Ace):
                    card = Shoe.GetNextTen;
                    break;
                case DealType.BlackJack:
                    card = Shoe.GetNextAce;
                    break;
                default:
                    card = this.Shoe.NextCard;
                    break;
            }

            playerHand.AddCard(card);

            if (!checkBlackJackImmediately) return;

            if (playerHand.State != State.Playing || playerHand.CurrentScore != 21) return;
            //blackjack
            playerHand.Blackjack();

            PlayOutTheGame(playerHand.Player);
        }

        public void SeedSplitHandWithNewCard(PlayerHand playerHand)
        {
            Card result = ActivePlayer.ActiveHand.Cards[1];
            playerHand.AddCard(result);

        }

        public void RemoveCardForSplitFromActivePlayerHand()
        {
            ActivePlayer.ActiveHand.RemoveCard(ActivePlayer.ActiveHand.Cards[1]);
        }

        private void GiveDealerACard(DealType dealType = DealType.Standard)
        {
            Card card;
            switch (dealType)
            {
                case DealType.Standard:
                    card = this.Shoe.NextCard;
                    break;
                case DealType.BlackJack when Dealer.Hand.Cards.Exists(i => i.CardType == CardType.Ace):
                    card = Shoe.GetNextTen;
                    break;
                case DealType.BlackJack:
                    card = Shoe.GetNextAce;
                    break;
                default:
                    card = this.Shoe.NextCard;
                    break;
            }
            Dealer.Hand.AddCard(card);
        }


        public enum DealType
        {
            Standard = 1,
            BlackJack = 2
        }
        public void IncreaseBet(PlayerHand hand, int amountToIncrease)
        {
            hand.IncreaseBet(amountToIncrease);
            hand.Player.PlayerbankRoll -= amountToIncrease;
            BankrollChangedEventArgs args = new BankrollChangedEventArgs(hand.Player);
            OnBankrollChange?.Invoke(this, args);
        }

        public async void PlayOutTheGame(Player player)
        {
            PlayerHand nextHand = player.CurrentHands.FirstOrDefault(i => i.State == State.NotYetPlayed);
            //move to the next player if there are no more unresolved hands.
            if (nextHand == null)
            {
                player.IsActive = false;

                bool everythingIsbusted = !PlayerList.Any(i => i.CurrentHands.Any(x => x.Result != Result.Bust));
                //If all hands have busted, end the game and don't play for the dealer
                if (everythingIsbusted)
                {
                    //do nothing
                    await Task.Delay(300);
                    OnGameEnd?.Invoke(this, null);
                    player.CurrentHands.Clear();
                    
                }
                else
                {
                    await Task.Delay(200);
                    OnShowAllCards?.Invoke(this, null);
                    while (!Dealer.Hand.IsBust() && Dealer.Hand.CurrentScore <= 16)
                    {
                        await Task.Delay(500);
                        GiveDealerACard();
                    }
                    CalculateScore();
                    await Task.Delay(200);
                    OnGameEnd?.Invoke(this, null);
                    player.CurrentHands.Clear();
                }
            }
            else 
            {
                nextHand.State = State.Playing;
            }
        }

        #endregion

        #region PrivateMethods

        private void CalculateScore()
        {
            //check if dealer has blackJack
            int dealerScore = Dealer.Hand.CurrentScore;
            foreach(var player in PlayerList)
            {
                foreach (PlayerHand i in player.CurrentHands)
                {
                    if (i.State == State.Stand || i.State == State.Doubled)
                    {
                        bool isDealerBust = Dealer.Hand.IsBust();
                        if (i.CurrentScore > dealerScore || isDealerBust)
                        {
                            AdjustPlayerBankRoll(player, i.CurrentBet.Amount * 2);
                            i.Win();
                            //TODO: fix, this raises a second event.
                            if(isDealerBust)
                            {
                                return;
                            }
                            else
                            {
                                Dealer.Hand.Lost();
                            }
                        }
                        else if (i.CurrentScore == dealerScore)
                        {
                            AdjustPlayerBankRoll(player, i.CurrentBet.Amount);
                            i.Push();
                            Dealer.Hand.Push(); 
                        }
                        else
                        {
                            i.Lost();
                            Dealer.Hand.Win();
                        }
                    }
                }
            }
        }
        #endregion

    }
}
