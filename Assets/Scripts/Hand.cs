using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using PokerEnums;
using static PokerEnums.PokerEnums;
using System;
using Unity.VisualScripting;
using System.Security.Cryptography;

namespace cardClass
{
    public class Hand
    {
        private List<Card> hand = new List<Card>();
        private PokerEnums.PokerEnums.HandResults handResult;
        private PokerEnums.PokerEnums.Rank winningRank;
        private PokerEnums.PokerEnums.Rank winningRankSub;
        private List<Action> handCheckers;
        public List<Card> _tempHand;
        public PokerEnums.PokerEnums.HandResults HandResult
        {
            get => handResult;
            private set => handResult = value;
        }
        public Hand()
        {
            HandResult = PokerEnums.PokerEnums.HandResults.None;
            handCheckers = new List<Action>();
            //Action _checkRoyalFlush = () => CheckRoyalFlush
        }
        public void SetHand(List<Card> cards)
        {
            hand.Clear();
            hand = cards;

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
            _tempHand = new(hand);
            _tempHand = _tempHand.OrderBy(x => x.rank).ToList();

            Debug.Log("Start of ratehand check");
            foreach(Card card in _tempHand)
            {
                Debug.Log(card);
            }
            Debug.Log(CheckFullHouse(_tempHand));

            Debug.Log(winningRank.ToString());
            Debug.Log(handResult.ToString());
        }
        //Change all checks to private after done testing
        public bool CheckRoyalFlush(List<Card> tempHand)
        {
           if(CheckStraightFlush(tempHand) && winningRank == Rank.Ace)
           {
                handResult = HandResults.RoyalFlush;
                return true;
           }
           return false;
        }
        public bool CheckFlush(List<Card> tempHand)
        {
            handResult = HandResults.Flush;
            return !(tempHand.Any(card => card.suit != hand[index: 0].suit));
        }
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
            } else if(groups.Count(g => g.Count() == 2) > 0){

                winningRank = groups.Last().Key;
                
                if(groups.Count() > 1)
                {
                    winningRankSub = groups.First().Key;
                    handResult = HandResults.TwoPair;
                    return false;
                }
                handResult = HandResults.OnePair;
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
                handResult = HandResults.FourOfAKind;
                winningRank = tempHand[3].rank;
                return true;
            }
            return false;
        }
        public bool CheckStraightFlush(List<Card> tempHand)
        {
            if (handResult == HandResults.StraightFlush)
                return true;
            bool Straight = CheckStraight(tempHand);
            bool Flush = CheckFlush(tempHand);
            if (Straight)
                handResult = HandResults.Straight;
            if (Flush)
                handResult = HandResults.Flush;
            if (Straight && Flush)
            {
                handResult = HandResults.StraightFlush;
                return true;
            }
            return false;
        }
        public bool CheckStraight(List<Card> tempHand)
        {
            if(handResult == HandResults.Straight)
                return true;
            if (!(tempHand.Any(card => card.rank == PokerEnums.PokerEnums.Rank.Five) || tempHand.Any(card => card.rank == PokerEnums.PokerEnums.Rank.Ten)))
                return false;
            bool beginningAce = tempHand[4].rank == Rank.Ace && tempHand[0].rank == Rank.Two;
            Debug.Log("Beginning Ace: " + beginningAce);
            for (int i = 0; i < tempHand.Count - 1; i++)
            {
                if ((tempHand[i].rank + 1 != tempHand[i + 1].rank) && !(beginningAce && i == tempHand.Count - 2))
                    return false;
            }
            Debug.Log("End of for");
            if (beginningAce)
                winningRank = tempHand[tempHand.Count - 2].rank;
            else
                winningRank = tempHand.Last().rank;
            handResult = HandResults.Straight;
            tempHand.RemoveAt(4);
            return true;
        }

        public bool CheckHighCard(List<Card> tempHand)
        {
            handResult = HandResults.HighCard;
            winningRank = tempHand.Last().rank;
            return true;
        }
    }
}
