using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using PokerEnums;
using static PokerEnums.PokerEnums;
using System;

namespace cardClass
{
    public class Hand
    {
        private List<Card> hand = new List<Card>();
        private PokerEnums.PokerEnums.HandResults handResult;
        private PokerEnums.PokerEnums.Rank winningRank;
        public PokerEnums.PokerEnums.HandResults HandResult
        {
            get => handResult;
            private set => handResult = value;
        }
        public Hand()
        {
            HandResult = PokerEnums.PokerEnums.HandResults.None;
        }
        public void SetHand(List<Card> cards) => hand = cards;
        public void AddCard(Card card) => hand.Add(card);
        public void DiscardCard(Card card) => hand.Remove(card);
        public void DiscardCard(int i) => hand.RemoveAt(i);
        public int GetLength() => hand.Count;
        public Card GetCard(int i) => hand[i];
        public void RateHand()
        {
            Debug.Log(hand.Count);
            if(hand.Count != 5)
                Debug.Log("Hand size does not equal 5");

            //Rating Hand not implemented yet
            List<Card> tempHand = new List<Card>(hand);
            CheckPair(tempHand);
            tempHand.Sort((x, y) => x.rank.CompareTo(y.rank));
            
        }
        //Change all checks to private after done testing
        public bool CheckFlush(List<Card> tempHand) => !(tempHand.Any(card => card.suit != hand[index: 0].suit));
        public bool CheckPair(List<Card> tempHand)
        {
            
            if(tempHand.GroupBy(x => x.rank).Count(g => g.Count() == 2) == 1)
            {
                winningRank = tempHand.GroupBy(x => x.rank).Where(x => x.Count() == 2).Last().Key;
                Debug.Log(Enum.GetName(typeof(PokerEnums.PokerEnums.Rank), winningRank));
                return true;
            }
            return false;
        }
        public bool CheckTwoPair(List<Card> tempHand)
        {
           if(tempHand.GroupBy(x => x.rank).Count(g => g.Count() == 2) == 2)
            {

                return true;
            }
           return false;
        }
        public bool CheckStraight(List<Card> tempHand)
        {
            if (!(tempHand.Any(card => card.rank == PokerEnums.PokerEnums.Rank.Five) || tempHand.Any(card => card.rank == PokerEnums.PokerEnums.Rank.Ten)))
                return false;

            return true;
        }
    }
}
