using System.ComponentModel;
using Blackjack.Core;
using Blackjack.Core.Counting;
using Blackjack.Core.Entities;
using Blackjack.Core.ShoeData;

namespace BlackJack.WinForm
{
    public partial class MainForm : Form
    {

        private readonly List<PlayerHandControl> _playerHandControlList = new List<PlayerHandControl>();
        private DealerHandControl _dealerHandControl;
        private readonly GameController _controller;
        private readonly BindingSource _shoeRemainingBindingSource;
        private readonly BindingSource _cardCountingStrategyBindingSource;
        /*
         * 
         * KNOWN BUGS
         * -If Blackjack and player get blackjack, the player loses.
         * -Aces dealt as hit cards are not treated as 11s.
         */
        //Bug occurs when splitting into a blackjack
        public MainForm()
        {
            InitializeComponent();
            _controller = new GameController(6);
            _controller.OnBankrollChange += Instance_OnBankrollChange;
            _controller.OnShowAllCards += Instance_OnShowAllCards;
            _controller.OnGameEnd += controller_onGameEnd;
            _controller.MinimumBet = Defaults.MINIMUM_BET;
            _controller.NumberOfDecks = Defaults.NUMBER_OF_DECKS;
            _controller.AddPlayer("Player 1", Defaults.BANKROLL);
            _controller.ShuffleAll();

            ShoeRemainingCollection collection = new ShoeRemainingCollection(_controller);

            collection.ShoeRemainingBindingList.ListChanged += ShoeRemainingBindingList_ListChanged;
            _tbBet.Text = _controller.MinimumBet.ToString("c0");
            _lblBankroll.Text = _controller.PlayerList[0].PlayerbankRoll.ToString("c0");
            _shoeRemainingBindingSource = new BindingSource();
            _shoeRemainingBindingSource.DataSource = collection.ShoeRemainingBindingList;
            CardCountingMethodCollection countStrategyCollection = new CardCountingMethodCollection(_controller);
            _cardCountingStrategyBindingSource = new BindingSource();
            _cardCountingStrategyBindingSource.DataSource = countStrategyCollection.CardCountingStrategyBindingList;
        }


        private void ShoeRemainingBindingList_ListChanged(object sender, ListChangedEventArgs e)
        {

        }

        #region User Events

        private void btnBet_Click(object sender, EventArgs e)
        {
            _btnBet.Enabled = false;
            _playerLayoutPanel.Controls.Clear();
            if (_controller.Shoe.UndealtCards.Count < 10)
                _controller.ShuffleAll();
            _controller.ResetBoard();
            _dealerLayoutPanel.Controls.Clear();
            _dealerHandControl = new DealerHandControl(_controller, this);
            _controller.Dealer.Hand.OnDealerBlackjack += controller_onGameEnd;
            _controller.Dealer.Hand.OnDealerBlackjack += Instance_OnShowAllCards;
            _controller.Dealer.Hand.OnDealerBust += controller_onGameEnd;
            _controller.Dealer.Hand.OnDealerBust += Instance_OnShowAllCards;
            _dealerLayoutPanel.Controls.Add(_dealerHandControl);
            PlayerHandControl playerHandControl = new PlayerHandControl(_controller.ActivePlayer, _controller, this, State.Playing);
            _playerHandControlList.Add(playerHandControl);
            _playerLayoutPanel.Controls.Add(playerHandControl);
            playerHandControl.btnDoubleDown.Enabled = true;
            playerHandControl.btnHit.Enabled = true;
            playerHandControl.btnStand.Enabled = true;
            _controller.IncreaseBet(_controller.ActivePlayer.ActiveHand, double.Parse(_tbBet.Text));
            //-----------------------
            _controller.DealAsync();     //
            //-----------------------
        }

        public void SplitHand()
        {
            PlayerHandControl newPlayerHandControl = new PlayerHandControl(_controller.ActivePlayer, _controller, this, State.NotYetPlayed);
            _playerHandControlList.Add(newPlayerHandControl);
            newPlayerHandControl.Visible = true;
            _playerLayoutPanel.Controls.Add(newPlayerHandControl);
            _controller.SeedSplitHandWithNewCard(newPlayerHandControl.PlayerHand);
            _controller.RemoveCardForSplitFromActivePlayerHand();
            _controller.IncreaseBet(newPlayerHandControl.PlayerHand, double.Parse(_tbBet.Text));
            newPlayerHandControl.DeactivateButtons();
            //add another card to first hand
            _controller.GivePlayerNextCardInShoe(_controller.ActivePlayer.ActiveHand, true);
            //add another card to last hand
            _controller.GivePlayerNextCardInShoe(newPlayerHandControl.PlayerHand, true);
        }

        #endregion

        #region Game Events

        private void controller_onGameEnd(object sender, EventArgs e)
        {
            _tbBet.Enabled = true;
            _btnBet.Enabled = true;
            _btnBet.Visible = true;

        }

        private void Instance_OnShowAllCards(object sender, EventArgs e)
        {
            _dealerHandControl.ShowAllCards();
        }

        private void Instance_OnBankrollChange(object sender, OnBankrollChangedEventArgs e)
        {
            _lblBankroll.Text = e.Player.PlayerbankRoll.ToString("c0");
        }

        #endregion
    }
}
