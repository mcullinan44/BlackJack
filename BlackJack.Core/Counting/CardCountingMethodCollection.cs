using System.ComponentModel;
using Blackjack.Core.Entities;


namespace Blackjack.Core.Counting
{
    public class CardCountingMethodCollection
    {
        private readonly BindingList<BaseCardCountingStrategy> _cardCountingStrategyBindingList;
        private readonly GameController _controller;

        public CardCountingMethodCollection(GameController controller)
        {
            this._cardCountingStrategyBindingList = new BindingList<BaseCardCountingStrategy>();
            _cardCountingStrategyBindingList.Add(CardCountingMethodFactory.GetStrategy(CardCountingStrategy.HiLo, controller.Shoe));
            _cardCountingStrategyBindingList.Add(CardCountingMethodFactory.GetStrategy(CardCountingStrategy.HiOp1, controller.Shoe));
            _cardCountingStrategyBindingList.Add(CardCountingMethodFactory.GetStrategy(CardCountingStrategy.HiOpt2, controller.Shoe));
            _cardCountingStrategyBindingList.Add(CardCountingMethodFactory.GetStrategy(CardCountingStrategy.Ko, controller.Shoe));
            _cardCountingStrategyBindingList.Add(CardCountingMethodFactory.GetStrategy(CardCountingStrategy.Omega3, controller.Shoe));
            _cardCountingStrategyBindingList.Add(CardCountingMethodFactory.GetStrategy(CardCountingStrategy.ZenCount, controller.Shoe));
            this._controller = controller;
            //this.controller.onCardReceived += controller_onCardReceived;
        }

        private void controller_onCardReceived(object sender, OnCardReceivedEventArgs args)
        {
            foreach (var item in _cardCountingStrategyBindingList)
            {
                item.CurrentCount += item.GetValueForCard(args.Card);
            }
        }

        public BindingList<BaseCardCountingStrategy> CardCountingStrategyBindingList
        {
            get { return this._cardCountingStrategyBindingList; }
        }
    }
}
