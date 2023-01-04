using Blackjack.Core.Entities;


namespace BlackJack.WinForm
{
    public static class ImageHelper
    {
        private static System.Resources.ResourceManager _resourceManager =
            new("BlackJack.WinForm.ImageResource", System.Reflection.Assembly.GetExecutingAssembly());
       
        public const int CARD_WIDTH = 72;
        public const int CARD_HEIGHT = 97;
        public const int CARD_WIDTH_RECT = 73;
        public const int CARD_HEIGHT_RECT = 98;
        public static Bitmap GetFaceImageForCard(Card card)
        {
            Bitmap cardImages = ImageResource.cards;
            //Adjust the clipping of the cards image to reflect the current card
            int x;
            int y;

            //if (card..Visible)
            //{
            //Define the card position in the cards image
            if (card.Index <= 10)
            {
                x = (card.Index - 1) % 2;
                y = (card.Index - 1) / 2;

                switch (card.CardSuit)
                {
                    case Suit.Spades:
                        x += 6;
                        break;
                    case Suit.Hearts:
                        x += 0;
                        break;
                    case Suit.Diamonds:
                        x += 2;
                        break;
                    case Suit.Clubs:
                        x += 4;
                        break;
                }
            }
            else
            {
                int number = (card.Index - 11);
                switch (card.CardSuit)
                {
                    case Suit.Spades:
                        number += 6;
                        break;
                    case Suit.Hearts:
                        number += 9;
                        break;
                    case Suit.Diamonds:
                        number += 3;
                        break;
                    case Suit.Clubs:
                        number += 0;
                        break;
                }

                x = (number % 2) + 8;
                y = number / 2;
            }
            Rectangle rect = new Rectangle(x * CARD_WIDTH_RECT, y * CARD_HEIGHT_RECT, CARD_WIDTH, CARD_HEIGHT);
            Bitmap cropped = cardImages.Clone(rect, ImageResource.cards.PixelFormat);
            return cropped;
        }

        public static Bitmap GetBackImage()
        {
            int x = 7;
            int y = 5;
            Bitmap cardImages = ImageResource.cards;
            Rectangle rect = new Rectangle(x * CARD_WIDTH_RECT, y * CARD_HEIGHT_RECT, CARD_WIDTH, CARD_HEIGHT);
            Bitmap cropped = cardImages.Clone(rect, cardImages.PixelFormat);
            return cropped;
        }
    }
}
