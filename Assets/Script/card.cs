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

    }
}
