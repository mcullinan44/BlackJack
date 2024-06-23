using Blackjack.Core.Entities;
using Blazor.Extensions;
using Blazor.Extensions.Canvas.Canvas2D;
using Microsoft.AspNetCore.Components;
using System.Drawing;

namespace BlackjackWeb
{
    public class CardGraphicsHelper
    {
        const int CARD_WIDTH = 72;
        const int CARD_HEIGHT = 97;
        const int CARD_WIDTH_RECT = 73;
        const int CARD_HEIGHT_RECT = 98;

        private readonly ElementReference _spritesheet;
        private readonly Canvas2DContext _context;
        private readonly BECanvasComponent _canvas;

        public CardGraphicsHelper(ElementReference spritesheet, Canvas2DContext context, BECanvasComponent canvas)
        {
            this._spritesheet = spritesheet;
            this._context = context;
            this._canvas = canvas;
        }


        public async ValueTask DealCardFaceUp(Card card, Hand hand)
        {
            int cardCountWidthOffset = hand.CardCount == 0 ? 0 : hand.CardCount * 20;
            int cardCountHeightOffset = hand.CardCount == 0 ? 0 : hand.CardCount * 20;
            AddCardFaceUp(card, hand, cardCountWidthOffset, cardCountHeightOffset);
        }


        public async ValueTask AddCardFaceUp(Card card, Hand hand, int cardCountWidthOffset, int cardCountHeightOffset)
        {
            if (_context == null)
            {
                return;
            }
            //Adjust the clipping of the cards image to reflect the current card
            int x;
            int y;

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

            await _context.DrawImageAsync(_spritesheet
                    , x * CARD_WIDTH_RECT
                    , y * CARD_HEIGHT_RECT
                    , CARD_WIDTH
                    , CARD_HEIGHT
                    , cardCountWidthOffset
                    , cardCountHeightOffset
                    , CARD_WIDTH
                    , CARD_HEIGHT);

        }




        public async Task DealHoleCard(Hand hand)
        {
            int x = 7;
            int y = 5;
            await _context.DrawImageAsync(_spritesheet
                , x * CARD_WIDTH_RECT
                , y * CARD_HEIGHT_RECT
                , CARD_WIDTH
                , CARD_HEIGHT
                , 20
                , 20
                , CARD_WIDTH
                , CARD_HEIGHT);
        }


        public async Task RevealAll(Hand hand)
        {

            await ClearCanvas();
            foreach(Card card in hand.Cards)
            {
                await AddCardFaceUp(card, hand,  hand.Cards.IndexOf(card) * 20, hand.Cards.IndexOf(card)* 20);

            }
        }


        public async ValueTask ClearCanvas()
        {
            await _context.ClearRectAsync(0, 0, _canvas.Width, _canvas.Height);
        }
    }
}
