using System;
using System.Linq;
using System.ComponentModel;
using Blackjack.Core.Entities;

namespace Blackjack.Core.ShoeData
{
    public class ShoeRemaining : INotifyPropertyChanged
    {
        private readonly Shoe _shoe;
        public Suit Suit { get; set; }
        private int _twoThroughNineCount;
        private int _tenThroughKingCount;
        private int _acesCount;

        public ShoeRemaining(Shoe shoe, Suit suit)
        {
            this._shoe = shoe;
            this.Suit = suit;
        }

        public void Calculate()
        {
            this.TwoThroughNineCount = _shoe.UndealtCards.Count(i => { return i.Index <= 9 && i.Index > 1 && i.CardSuit == this.Suit; });
            this.TenThroughKingCount = _shoe.UndealtCards.Count(i => { return i.Index > 9 && i.Index <= 13 && i.CardSuit == this.Suit; });
            this.AcesCount = _shoe.UndealtCards.Count(i => { return i.Index == 1 && i.CardSuit == this.Suit; });
        }

        public int TwoThroughNineCount
        {
            get => this._twoThroughNineCount;
            set
            {
                if (value != this._twoThroughNineCount)
                {
                    this._twoThroughNineCount = value;
                    NotifyPropertyChanged("TwoThroughNineCount");
                }
            }
        }

        public int TenThroughKingCount
        {

            get { return this._tenThroughKingCount; }
            set
            {
                if (value != this._tenThroughKingCount)
                {
                    this._tenThroughKingCount = value;
                    NotifyPropertyChanged("TenThroughKingCount");
                }
            }
        }

        public int AcesCount
        {
            get => this._acesCount;
            set
            {
                if (value != this._acesCount)
                {
                    this._acesCount = value;
                    NotifyPropertyChanged("AcesCount");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(String info)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(info));
        }
    }
}
