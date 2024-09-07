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
        private PokerEnums.PokerEnums.Rank winningRankSub;
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
            //CheckPair(tempHand);
            tempHand.Sort((x, y) => x.rank.CompareTo(y.rank));
            foreach(Card card in tempHand)
            {
                Debug.Log(card);
            }
            
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
        public bool CheckThree(List<Card> tempHand)
        {
            if (tempHand.GroupBy(x => x.rank).Count(g => g.Count() == 3) == 1)
            {
                winningRank = tempHand.GroupBy(x => x.rank).Where(x => x.Count() == 3).Last().Key;
                Debug.Log(Enum.GetName(typeof(PokerEnums.PokerEnums.Rank), winningRank));
                return true;
            }
            return false;
        }
        public bool CheckFour(List<Card> tempHand)
        {
            if (tempHand.GroupBy(x => x.rank).Count(g => g.Count() == 4) == 1)
            {
                winningRank = tempHand.GroupBy(x => x.rank).Where(x => x.Count() == 4).Last().Key;
                Debug.Log(Enum.GetName(typeof(PokerEnums.PokerEnums.Rank), winningRank));
                return true;
            }
            return false;
        }
        public bool CheckStraightFlush(List<Card> tempHand)
        {
            if(CheckStraight(tempHand) && CheckFlush(tempHand))
            {
                return true;
            }
            return false;
        }
        public bool CheckStraight(List<Card> tempHand)
        {
            if (!(tempHand.Any(card => card.rank == PokerEnums.PokerEnums.Rank.Five) || tempHand.Any(card => card.rank == PokerEnums.PokerEnums.Rank.Ten)))
                return false;
            //Idk how to do a lambda expression for this
            bool endAce = tempHand[0].rank + 12 == tempHand[tempHand.Count - 1].rank;

            for (int i = 0; i < tempHand.Count - 1; i++)
                if ((tempHand[i].rank + 1 != tempHand[i + 1].rank) || (endAce && i == 0)) 
                    return false;
            if (endAce)
                winningRank = PokerEnums.PokerEnums.Rank.EndAce;
            else
                winningRank = tempHand.Last().rank;
            return true;
        }
    }
}
