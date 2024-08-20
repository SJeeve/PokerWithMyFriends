using UnityEngine;
namespace cardClass
{
    public class Card
    {
            public int suit;
            public int rank;
            public Sprite cardSprite;
            [SerializeField] public SpriteRenderer spriteRenderer;

            public Card(int suit, int rank, Sprite cardSprite)
            {
                this.suit = suit;
                this.rank = rank;
                this.cardSprite = cardSprite;
            }

            public void updateFace()
            {
                if (spriteRenderer.sprite != null)
                {
                    spriteRenderer.sprite = cardSprite;
                }
            }

    }
}
