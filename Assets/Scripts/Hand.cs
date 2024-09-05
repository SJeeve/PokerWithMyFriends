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
        private PokerEnums.PokerEnums.HandResults handResult;
        public Hand()
        {
            handResult = PokerEnums.PokerEnums.HandResults.None;
        }
        public void SetHand(List<Card> cards) => hand = cards;
        public void AddCard(Card card) => hand.Add(card);
        public void DiscardCard(Card card) => hand.Remove(card);
        public void DiscardCard(int i) => hand.RemoveAt(i);
        public int GetLength() => hand.Count;
        public Card GetCard(int i) => hand[i];
        public void RateHand()
        {
            if(hand.Count != 5)
                Debug.Log("Hand size does not equal 5");

            //Rating Hand not implemented yet
            List<Card> tmpHand = new List<Card>(hand.Sort((x, y) => x.rank.CompareTo(y.rank)));
            
        }
        //Change all checks to private after done testing
        public bool CheckFlush() => hand.Any(o => o.suit != hand[0].suit);

        public bool CheckStraight()
        {
            return true;
        }
    }
}
