using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace cardClass
{
    public class Hand
    {
        private List<Card> hand = new List<Card>();
        public Hand(){}

        public void AddCard(Card card)
        {
            hand.Add(card);
        }
        public void DiscardCard(Card card)
        {
            hand.Remove(card);
        }
        public void DiscardCard(int i)
        {
            hand.RemoveAt(i);
        }
        public int GetLength()
        {
            return hand.Count;
        }

        public Card GetCard(int i)
        {
            return hand[i];
        }
        public string RateHand()
        {
            if(hand.Count != 5)
            {
                Debug.Log("Hand size does not equal 5");
            }
            //Rating Hand not implemented yet
            return "";
        }
    }
}
