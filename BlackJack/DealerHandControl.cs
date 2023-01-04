using Blackjack.Core;
using Blackjack.Core.Entities;

namespace BlackJack.WinForm
{
    public partial class DealerHandControl : HandControl
    {
        private readonly DealerHand _hand;
        
        public DealerHandControl(GameController controller,MainForm form): base(controller,form)
        {
            _hand = new DealerHand(controller);
            controller.OnDealerCardReceived += dealer_onCardReceived;
            lblOutcome.Text = string.Empty;
            lblOutcome.Visible = false;
            btnDoubleDown.Visible = false;
            btnSplit.Visible = false;
            btnStand.Visible = false;
            btnHit.Visible = false;
            _hand.OnDealerBust += dealer_onBust;
            _hand.OnDealerBlackjack += dealer_onBlackjack;
            _hand.OnDealerWinHand += hand_onWinHand;
            _hand.OnDealerLoseHand += hand_onLoseHand;
            _hand.OnPushHand += hand_onPushHand;
        }

        private void hand_onPushHand(Hand hand)
        {
            lblOutcome.Text = "Push";
        }

        private void hand_onLoseHand(Hand hand)
        {
            lblOutcome.Text = hand.CurrentScore.ToString();
            lblOutcome.Visible = true;
        }

        private void hand_onWinHand(Hand hand)
        {
            lblOutcome.Text = hand.CurrentScore.ToString();
            lblOutcome.Visible = true;
        }

        private void dealer_onBust(object sender, OnCardReceivedEventArgs args)
        {
            lblOutcome.Text = "Bust with " + args.Hand.CurrentScore;
            lblOutcome.Visible = true;
        }

        private void dealer_onBlackjack(object sender, OnCardReceivedEventArgs args)
        {
            lblOutcome.Text = "Blackjack";
            lblOutcome.Visible = true;
        }

        private void dealer_onCardReceived(object sender, OnCardReceivedEventArgs args)
        {
            try
            {
                if (args.Hand.CardCount == 2)
                {
                    AddCardAsync(args.Card, false);
                }
                else
                {
                    AddCard(args.Card);
                }
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
            }
        }

        public void ShowAllCards()
        {
            this.PictureBoxList.Clear();
            pnlHand.Controls.Clear();
            foreach (var card in _hand.Cards)
            {
                AddCardAsync(card, true);
            }
        }
    }
}
