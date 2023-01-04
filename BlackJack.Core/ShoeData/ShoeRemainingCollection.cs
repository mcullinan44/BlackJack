using System;
using System.Linq;
using System.ComponentModel;
using Blackjack.Core.Entities;


namespace Blackjack.Core.ShoeData
{
    public class ShoeRemainingCollection
    {
        public ShoeRemainingCollection(GameController controller)
        {
            ShoeRemainingBindingList = new BindingList<ShoeRemaining>
            {
                new ShoeRemaining(controller.Shoe, Suit.Clubs),
                new ShoeRemaining(controller.Shoe, Suit.Diamonds),
                new ShoeRemaining(controller.Shoe, Suit.Hearts),
                new ShoeRemaining(controller.Shoe, Suit.Spades)
            };
            foreach (var item in ShoeRemainingBindingList)
            {
                item.Calculate();
            }
            controller.OnShuffle += controller_onShuffle;
        }

        private void controller_onShuffle(object sender, EventArgs e)
        {
            foreach (ShoeRemaining item in ShoeRemainingBindingList)
            {
                item.Calculate();
            }
        }

        public BindingList<ShoeRemaining> ShoeRemainingBindingList { get; }

        private void controller_onCardReceived(object sender, OnCardReceivedEventArgs args)
        {
            ShoeRemainingBindingList.First(i => i.Suit == args.Card.CardSuit).Calculate();
        }
    }
}
