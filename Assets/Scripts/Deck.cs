using PokerEnums;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.GPUSort;
namespace cardClass
{
    public class Deck
    {
        Card[] cards = new Card[52];
        Stack<Card> shuffledDeck = new Stack<Card>();
        System.Random Random = new System.Random();
        public Deck(Sprite[] cardSprites)
        {
            if (cardSprites.Length != 52)
                Debug.Log("cardSprites array length not 52");
            for (int i = 0; i < 52; i++)
                cards[i] = new Card((PokerEnums.PokerEnums.Suit)(i / 13), (PokerEnums.PokerEnums.Rank)(i % 13), cardSprites[i]);
        }

        public void ShuffleDeck()
        {
            shuffledDeck.Clear();
            shuffledDeck = RandomExtensions.Shuffle<Card>(Random, cards);
        }
        public void PrintShuffled()
        {
            foreach (Card card in shuffledDeck)
            {
                Debug.Log(card.ToString());
            }
        }
        public void PrintUnshuffled()
        {
            foreach (Card card in cards)
            {
                Debug.Log(card.ToString());
            }
        }
        public Card DrawCard()
        {
            return shuffledDeck.Pop();
        }

    }

    static class RandomExtensions
    {
        //Knuth shuffle
        public static Stack<Card> Shuffle<Card>(this System.Random rng, Card[] array)
        {
            Card[] tempArray = array;
            int n = array.Length;
            for(int i = 0; i < array.Length; i++)
            {
                tempArray[i] = array[i];
            }
            while (n > 1)
            {
                int k = rng.Next(n--);
                Card temp = tempArray[n];
                tempArray[n] = tempArray[k];
                tempArray[k] = temp;
            }
            Stack<Card> stack = new Stack<Card>();
            for(int j = 0; j < tempArray.Length; j++)
            {
                stack.Push(tempArray[j]);
            }
            return stack;
        }
    }

}
