using System;
using PokerEnums;
using UnityEngine;
namespace cardClass
{
    public class Card
    {
            public Enum suit;
            public Enum rank;
            public Sprite cardSprite;
            [SerializeField] public SpriteRenderer spriteRenderer;
            public Sprite backSprite;

            public Card(Enum suit, Enum rank, Sprite cardSprite)
            {
                this.suit = suit;
                this.rank = rank;
                this.cardSprite = cardSprite;
                
            }

            public Card()
            {
                this.suit = (PokerEnums.PokerEnums.Rank)0;
                this.rank = (PokerEnums.PokerEnums.Suit)0;
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
            output += Enum.GetName(typeof(PokerEnums.PokerEnums.Rank), rank);
            output += " of ";
            output += Enum.GetName(typeof(PokerEnums.PokerEnums.Suit), suit);
            return output;
        }


    }
}
