using System;
using PokerEnums;
using UnityEngine;
namespace cardClass
{
    public class Card
    {
            public PokerEnums.PokerEnums.Suit suit;
            public PokerEnums.PokerEnums.Rank rank;
            public Sprite cardSprite;
            [SerializeField] public SpriteRenderer spriteRenderer;
            public Sprite backSprite;

            public Card(PokerEnums.PokerEnums.Suit suit, PokerEnums.PokerEnums.Rank rank, Sprite cardSprite)
            {
                this.suit = suit;
                this.rank = rank;
                this.cardSprite = cardSprite;
                
            }

            public Card()
            {
                this.suit = (PokerEnums.PokerEnums.Suit)0;
                this.rank = (PokerEnums.PokerEnums.Rank)0;
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
                output += Enum.GetName(typeof(PokerEnums.PokerEnums.Rank), rank);
                output += " of ";
                output += Enum.GetName(typeof(PokerEnums.PokerEnums.Suit), suit);
                return output;
            }


    }
}
