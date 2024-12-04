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
        Card[] unshuffledDeck = new Card[52];
        Stack<Card> shuffledDeck = new Stack<Card>();
        System.Random Random = new System.Random();
        public Deck()
        {
            for (int i = 0; i < 52; i++)
                unshuffledDeck[i] = new Card(i);
        }

        public void ShuffleDeck()
        {
            shuffledDeck.Clear();
            shuffledDeck = RandomExtensions.Shuffle<Card>(Random, unshuffledDeck);
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
            foreach (Card card in unshuffledDeck)
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
        //To be completely honest why this is so effective in randomizing a list is a little above my understanding
        //So I'll have to research that later
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
                //This actually makes the stack the reverse of shuffled deck, but it doesn't matter so Im not changing it
                stack.Push(tempArray[j]);
            }
            return stack;
        }
    }

}
