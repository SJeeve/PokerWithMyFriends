using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using PokerEnums;
namespace cardClass
{
    public class Hand
    {
        private List<Card> hand = new List<Card>();
        public PokerEnums.PokerEnums.HandResults handResults;
        public Hand()
        {
            handResults = PokerEnums.PokerEnums.HandResults.None;
        }
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
        public void RateHand()
        {
            if(hand.Count != 5)
                Debug.Log("Hand size does not equal 5");

            //Rating Hand not implemented yet
            hand.Sort((x, y) => x.rank.CompareTo(y.rank));
            
        }
    }
}
