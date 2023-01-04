using System;
using System.ComponentModel;
using Blackjack.Core.Entities;


namespace Blackjack.Core.Counting
{
    public abstract class BaseCardCountingStrategy : INotifyPropertyChanged
    {
        private readonly Shoe _shoe;
        private int _currentCount;
        public abstract string StrategyName { get; }

        protected BaseCardCountingStrategy(Shoe shoe)
        {
            _shoe = shoe;
        }

        public abstract int GetValueForCard(Card card);

        public int CurrentCount
        {
            get => _currentCount;
            set
            {
                if (value != this._currentCount)
                {
                    this._currentCount = value;
                    NotifyPropertyChanged("CurrentCount");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }
    }
}
