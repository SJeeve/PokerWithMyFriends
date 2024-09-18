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
        public void SetHand(List<Card> cards)
        {
            hand.Clear();
            hand = cards;
            Debug.Log("Start of SetHand");
            Debug.Log(hand.Count());
            foreach(Card card in hand)
            {
                Debug.Log(card);
            }
        }
        public void AddCard(Card card) => hand.Add(card);
        public void DiscardCard(Card card) => hand.Remove(card);
        public void DiscardCard(int i) => hand.RemoveAt(i);
        public int GetLength() => hand.Count;
        public Card GetCard(int i) => hand[i];
        public void RateHand()
        {   
            if(hand.Count != 5)
                Debug.Log("Hand size does not equal 5");

            List<Card> tempHand = new List<Card>(hand);
            tempHand.Sort((x, y) => x.rank.CompareTo(y.rank));
            Debug.Log("Start of ratehand check");
            foreach(Card card in tempHand)
            {
                Debug.Log(card);
            }

            
        }
        //Change all checks to private after done testing
        public bool CheckRoyalFlush(List<Card> tempHand) => CheckStraightFlush(tempHand) && winningRank == Rank.Ace;
        public bool CheckFlush(List<Card> tempHand) => !(tempHand.Any(card => card.suit != hand[index: 0].suit));
        public bool CheckPair(List<Card> tempHand)
        {
            return handResult == HandResults.OnePair;
        }
        public bool CheckTwoPair(List<Card> tempHand)
        {
            return handResult == HandResults.TwoPair;
        }
        //CheckFullHouse handles itself, three of a kind, two pair, and pairs.
        public bool CheckFullHouse(List<Card> tempHand)
        {
            IEnumerable<IGrouping<PokerEnums.PokerEnums.Rank, Card>> groups = tempHand.GroupBy(x => x.rank).Where(g => g.Count() >= 2);
            if(groups.Count(g => g.Count() == 3) == 1)
            {
                //If we have a three of a kind we know the card in the middle is the winningRank
                winningRank = tempHand[2].rank;
                if(groups.Count(g => g.Count() == 2) == 1)
                {
                    handResult = HandResults.FullHouse;
                    winningRankSub = groups.Where(g => g.Count() == 2).Last().Key;
                    return true;
                }
                handResult = HandResults.ThreeOfAKind;
            } else {

                winningRank = groups.Last().Key;
                handResult = HandResults.OnePair;
                if(groups.Count() > 1)
                {
                    winningRankSub = groups.First().Key;
                    handResult = HandResults.TwoPair;
                }
            }
                
            return false;
        }
        public bool CheckThree(List<Card> tempHand)
        {
            return handResult == HandResults.ThreeOfAKind;
        }
        public bool CheckFour(List<Card> tempHand)
        {
            
            if (tempHand.GroupBy(x => x.rank).Count(g => g.Count() == 4) == 1)
            {
                winningRank = tempHand[3].rank;
                return true;
            }
            return false;
        }
        public bool CheckStraightFlush(List<Card> tempHand)
        {
            if (CheckStraight(tempHand) && CheckFlush(tempHand))
            {
                return true;
            }
            return false;
        }
        public bool CheckStraight(List<Card> tempHand)
        {
            //I don't know how to do a lambda expression for this one
            if (!(tempHand.Any(card => card.rank == PokerEnums.PokerEnums.Rank.Five) || tempHand.Any(card => card.rank == PokerEnums.PokerEnums.Rank.Ten)))
                return false;
            bool beginningAce = tempHand.Last().rank == Rank.Ace && tempHand.First().rank == Rank.Two;

            for (int i = 0; i < tempHand.Count - 1; i++)
                if ((tempHand[i].rank + 1 != tempHand[i + 1].rank) && !(beginningAce && i == tempHand.Count - 1))
                    return false;
            if (beginningAce)
                winningRank = tempHand[tempHand.Count - 2].rank;
            else
                winningRank = tempHand.Last().rank;
            return true;
        }
    }
}
