using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.GPUSort;
namespace cardClass
{
    public class BaseDeck
    {
        Card[] cards = new Card[52];
        Queue<Card> shuffledDeck = new Queue<Card>();
        public BaseDeck(Sprite[] cardSprites)
        {
            if (cardSprites.Length != 52)
                Console.WriteLine("cardSprites array length not 52");
            for (int i = 0; i < 52; i++)
                cards[i] = new Card(i / 13, i % 13, cardSprites[i]);
        }

        public Card[] getShuffledDeck()
        {
            return cards;
        }



    }

    static class RandomExtensions
    {
        public static Card[] Shuffle<Card>(this System.Random rng, Card[] array)
        {
            Card[] tempArray = array;
            int n = array.Length;

            while (n > 1)
            {
                int k = rng.Next(n--);
                Card temp = array[n];
                tempArray[n] = array[k];
                tempArray[k] = temp;
            }
            return tempArray;
        }
    }

}
