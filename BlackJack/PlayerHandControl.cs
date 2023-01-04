using Blackjack.Core;
using Blackjack.Core.Entities;

namespace BlackJack.WinForm
{
    public partial class PlayerHandControl : HandControl
    {
        public readonly PlayerHand PlayerHand;
        
        public PlayerHandControl(Player player, GameController controller, MainForm blackjackForm, State state)
            : base(controller,blackjackForm)
        {
            InitializeComponent();
            //create a new hand for the active player
            PlayerHand = controller.AddHandToPlayer(player, State.NotYetPlayed);
            PlayerHand.State = state;
            
            lblBet.Visible = true;
            lblBet.Text = PlayerHand.CurrentBet.Amount.ToString("c0");
            PlayerHand.OnBetChanged += PlayerHand_onBetChanged;
            PlayerHand.OnCardReceived += hand_onCardReceived;
            PlayerHand.OnBust += hand_onBust;
            PlayerHand.OnBlackjack += hand_onBlackjack;
            PlayerHand.OnWinHand += hand_onWinHand;
            PlayerHand.OnLoseHand += hand_onLoseHand;
            PlayerHand.OnPushHand += hand_onPushHand;
            PlayerHand.OnActivate += PlayerHand_onActivate;
            PlayerHand.OnTakeCardForSplit += PlayerHand_OnTakeCardForSplit;
            controller.OnGameEnd += Controller_onGameEnd;
            IsPlaying = PlayerHand.State == State.Playing;
            btnSplit.Click += btnSplit_Click;
            btnDoubleDown.Click += btnDoubleDown_Click;
            btnHit.Click += btnHit_Click;
            btnStand.Click += btnStand_Click;
        }

        private void PlayerHand_OnTakeCardForSplit(object sender, OnCardRemovedForSplitEventArgs e)
        {
            Card result = e.Card;
            PlayerHand.Cards.Remove(result);
            PictureBoxList.Remove(PictureBoxList.Find(i => i.Tag == result));
            pnlHand.Controls.RemoveAt(0);
        }

        private void PlayerHand_onBetChanged(object sender, OnBetChangedEventArgs args)
        {
            lblBet.Text = (args.Bet.Amount * 1).ToString("c0");
        }

        private void PlayerHand_onActivate(object sender, EventArgs e)
        {
            ActivateButtons();
        }

        private void ActivateButtons()
        {
            btnHit.Enabled = true;
            btnStand.Enabled = true;
            //can't split more than two times.
            if(Controller.ActivePlayer.CurrentHands.Count == 3)
            {
                btnSplit.Enabled = false;
            }
            if (PlayerHand.Cards.Count == 2)
            {
                if (PlayerHand.Cards[0].CardType == PlayerHand.Cards[1].CardType
                    && PlayerHand.Cards[0].Value == PlayerHand.Cards[1].Value)
                {
                    btnSplit.Enabled = true;
                }
                btnDoubleDown.Enabled = true;
            }
            else
            {
                btnDoubleDown.Enabled = this.btnSplit.Enabled = false;
            }
        }

        public void DeactivateButtons()
        {
            btnHit.Enabled = btnSplit.Enabled = btnDoubleDown.Enabled = btnStand.Enabled = false;
        }

        private void Controller_onGameEnd(object sender, EventArgs e)
        {
            btnDoubleDown.Enabled = false;
            btnHit.Enabled = false;
            btnStand.Enabled = false;
            btnSplit.Enabled = false;
        }

        private void hand_onBlackjack(object sender, OnCardReceivedEventArgs args)
        {
            lblWinning.Text = "+ " + (this.PlayerHand.CurrentBet.Amount * 1.5).ToString("c0");
            lblWinning.Visible = lblOutcome.Visible = true;
            lblOutcome.Text = "Blackjack!";
            IsPlaying = false;
        }

        private void hand_onBust(object sender, OnCardReceivedEventArgs args)
        {
            lblWinning.Text = "-" + ((PlayerHand)args.Hand).CurrentBet.Amount.ToString("c0");
            lblWinning.Visible = lblOutcome.Visible = true;
            lblOutcome.Text = "Bust with " + args.Hand.CurrentScore;
            IsPlaying = false;
        }

        private void hand_onPushHand(Hand hand)
        {
            lblWinning.Text = "$0";
            lblWinning.Visible = lblOutcome.Visible = true;
            lblOutcome.Text = "Push";
            IsPlaying = false;
        }

        private void hand_onLoseHand(Hand hand)
        {
            lblWinning.Text = "- " + ((PlayerHand)hand).CurrentBet.Amount.ToString("c0");
            lblWinning.Visible = lblOutcome.Visible = true;
            lblOutcome.Text = "Lose with " + hand.CurrentScore;
            IsPlaying = false;
        }

        private void hand_onWinHand(Hand hand)
        {
            lblWinning.Text = "+ " + ((PlayerHand)hand).CurrentBet.Amount.ToString("c0");
            lblWinning.Visible = lblOutcome.Visible = true;
            lblOutcome.Text = "Win with " + hand.CurrentScore;
            IsPlaying = false;
        }

        private void hand_onCardReceived(object sender, OnCardReceivedEventArgs args)
        {
            AddCard(args.Card);
            if(PlayerHand.State == State.Playing)
            {
                ActivateButtons();
            }
        }

        private bool IsPlaying
        {
            set
            {
                if(!value)
                {
                    DeactivateButtons();

                }
                lblActive.Visible = value;
            }
        }

        private void btnHit_Click(object sender, System.EventArgs e)
        {
            PlayerHand.Hit();
            
        }

        private void btnStand_Click(object sender, System.EventArgs e)
        {
            
            PlayerHand.Stand();
            DisableButtons();
        }

        private void btnSplit_Click(object sender, System.EventArgs e)
        {
            ((MainForm)this.ParentForm).SplitHand();
        }

        private void btnDoubleDown_Click(object sender, System.EventArgs e)
        {
            //Controller.DoubleDown((PlayerHand)hand);

            PlayerHand.DoubleDown();

            DisableButtons();

            //if the double down results in the bust, bust
            //if the double down does not bust, finishHand

            //if the hit results in a bust, finishedHand
            //if the hit does not result in the bust, doNotFinishHand

        }

        private void DisableButtons()
        {
            btnDoubleDown.Enabled = false;
            btnSplit.Enabled = false;
            btnHit.Enabled = false;
            btnStand.Enabled = false;
        }
    }
}
