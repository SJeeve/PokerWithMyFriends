using System;
using UnityEngine;
namespace cardClass
{
    public class Card
    {
            public int suit;
            public int rank;
            public Sprite cardSprite;
            [SerializeField] public SpriteRenderer spriteRenderer;
            public Sprite backSprite;

            public Card(int suit, int rank, Sprite cardSprite)
            {
                this.suit = suit;
                this.rank = rank;
                this.cardSprite = cardSprite;
                
            }

            public Card()
            {
                this.suit = 0;
                this.rank = 0;
            }

            public void updateFace()
            {
                if (spriteRenderer.sprite != null)
                {
                    spriteRenderer.sprite = cardSprite;
                }
            }

            public void updateBack()
            {
                if (spriteRenderer.sprite != null)
                {
                    spriteRenderer.sprite = backSprite;
                }
            }

            public override string ToString()
        {
            string output = "";
            //Prolly not good code but I wanted to 
            switch (rank)
            {
                case 0:
                    output += "Ace";
                    break;
                case 1:
                    output += "Two";
                    break;
                case 2:
                    output += "Three";
                    break;
                case 3:
                    output += "Four";
                    break;
                case 4:
                    output += "Five";
                    break;
                case 5:
                    output += "Six";
                    break;
                case 6:
                    output += "Seven";
                    break;
                case 7:
                    output += "Eight";
                    break;
                case 8:
                    output += "Nine";
                    break;
                case 9:
                    output += "Ten";
                    break;
                case 10:
                    output += "Jack";
                    break;
                case 11:
                    output += "Queen";
                    break;
                case 12:
                    output += "King";
                    break;
                default:
                    output += "Rank not known";
                    break;
            }
            switch (suit)
            {
                case 0:
                    output += " of Clubs";
                    break;
                case 1:
                    output += " of Diamonds";
                    break;
                case 2:
                    output += " of Hearts";
                    break;
                case 3:
                    output += " of Spades";
                    break;
                default:
                    output += "Suit not known";
                    break;
            }
            return output;
        }


    }
}
