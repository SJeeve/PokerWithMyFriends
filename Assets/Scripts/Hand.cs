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
        private PokerEnums.PokerEnums.HandResults handResult = HandResults.None;
        public HandResults HandResults {
            get { return handResult; }
            private set { handResult = value; }
        }
        private PokerEnums.PokerEnums.Rank _winningRank;
        private PokerEnums.PokerEnums.Rank _winningRankSub;
        private List<Func<Boolean>> handCheckers;
        public List<Card> _tempHand;
        public float handScore = 0;
        private int[] lastHandIndexes = { -1, -1, -1, -1, -1};
        public float HandScore
        {
            get { return handScore; }
            private set { handScore = value; }

        }
        public PokerEnums.PokerEnums.Rank WinningRank
        {
            get => _winningRank;
            private set => _winningRank = value;
        }
        public PokerEnums.PokerEnums.Rank WinningRankSub
        {
            get => _winningRankSub;
            private set => _winningRankSub = value;
        }
        public PokerEnums.PokerEnums.HandResults HandResult
        {
            get => handResult;
            private set => handResult = value;
        }
        public Hand()
        {
            HandResult = PokerEnums.PokerEnums.HandResults.None;
            handCheckers = new List<Func<Boolean>>();
            PopulateActions();
        }
        public void PopulateActions()
        {
            handCheckers.Add(() => CheckRoyalFlush(_tempHand));
            handCheckers.Add(() => CheckStraightFlush(_tempHand));
            handCheckers.Add(() => CheckFour(_tempHand));
            handCheckers.Add(() => CheckFullHouse(_tempHand));
            handCheckers.Add(() => CheckFlush(_tempHand));
            handCheckers.Add(() => CheckStraight(_tempHand));
            handCheckers.Add(() => CheckThree(_tempHand));
            handCheckers.Add(() => CheckTwoPair(_tempHand));
            handCheckers.Add(() => CheckPair(_tempHand));
            handCheckers.Add(() => CheckHighCard(_tempHand));
        }
        public void SetHand(List<Card> cards)
        {
            hand.Clear();
            hand = cards;

        }
        public void AddCard(Card card) => hand.Add(card);
        public void DiscardCard(Card card) => hand.Remove(card);
        public void DiscardCard(int i) => hand.RemoveAt(i);
        public void DiscardAll() => hand.Clear();
        public int GetLength() => hand.Count;
        public Card GetCard(int i) => hand[i];
        public bool HasAce()
        {
            return hand.Any(card => card.rank == Rank.Ace);
        }
        public void RateHand()
        {   
            if(hand.Count != 5)
                Debug.LogWarning("Hand size does not equal 5");
            hand = hand.OrderBy(x => x.rank).ToList();

            int[] currentHandIndexes = new int[hand.Count];

            //We check real quick to see if the new hand is the exact same as the old hand
            //If so we don't need to check anything new so we just return
            if (currentHandIndexes.SequenceEqual(lastHandIndexes))
            {
                return;
            }
            
            //If we do have a different hand then we update the last hand

            //Now that I think about it I could just have a boolean to check if we discarded
            for (int i = 0; i < lastHandIndexes.Length; i++)
            {
                lastHandIndexes[0] = currentHandIndexes[i];
            }


            _tempHand = new(hand);
            _tempHand = _tempHand.OrderBy(x => x.rank).ToList();

            foreach(Func<Boolean> func in handCheckers)
            {
                if (func.Invoke())
                {
                    break;
                }
            }
            handScore = ((float)handResult) + ((float)WinningRank) / 10f + ((float)WinningRankSub) / 100;
        }
        //Change all checks to private after done testing
        private bool CheckRoyalFlush(List<Card> tempHand)
        {
           if(CheckStraightFlush(tempHand) && _winningRank == Rank.Ace)
           {
                handResult = HandResults.RoyalFlush;
                return true;
           }
           return false;
        }
        private bool CheckFlush(List<Card> tempHand)
        {
            if(handResult == HandResults.Flush)
                return true;
            return !(tempHand.Any(card => card.suit != hand[index: 0].suit));
        }
        private bool CheckPair(List<Card> tempHand)
        {
            return handResult == HandResults.OnePair;
        }
        private bool CheckTwoPair(List<Card> tempHand)
        {
            return handResult == HandResults.TwoPair;
        }
        //CheckFullHouse handles itself, three of a kind, two pair, and pairs.
        private bool CheckFullHouse(List<Card> tempHand)
        {
            IEnumerable<IGrouping<PokerEnums.PokerEnums.Rank, Card>> groups = tempHand.GroupBy(x => x.rank).Where(g => g.Count() >= 2);
            if(groups.Count(g => g.Count() == 3) == 1)
            {
                //If we have a three of a kind we know the card in the middle is the _winningRank
                _winningRank = tempHand[2].rank;
                if(groups.Count(g => g.Count() == 2) == 1)
                {
                    handResult = HandResults.FullHouse;
                    _winningRankSub = groups.Where(g => g.Count() == 2).Last().Key;
                    return true;
                }
                handResult = HandResults.ThreeOfAKind;
            }
            else if (groups.Count(g => g.Count() == 2) >= 1)
            {
                _winningRank = groups.Last().Key;
                
                if(groups.Count() > 1)
                {
                    _winningRankSub = groups.First().Key;
                    handResult = HandResults.TwoPair;
                    return false;
                }
                handResult = HandResults.OnePair;
            }
            return false;
        }
        private bool CheckThree(List<Card> tempHand)
        {
            return handResult == HandResults.ThreeOfAKind;
        }
        private bool CheckFour(List<Card> tempHand)
        {
            
            if (tempHand.GroupBy(x => x.rank).Count(g => g.Count() == 4) == 1)
            {
                handResult = HandResults.FourOfAKind;
                _winningRank = tempHand[3].rank;
                return true;
            }
            return false;
        }
        private bool CheckStraightFlush(List<Card> tempHand)
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
        private bool CheckStraight(List<Card> tempHand)
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
                _winningRank = tempHand[tempHand.Count - 2].rank;
            else
                _winningRank = tempHand.Last().rank;
            handResult = HandResults.Straight;
            tempHand.RemoveAt(4);
            return true;
        }
        private bool CheckHighCard(List<Card> tempHand)
        {
            handResult = HandResults.HighCard;
            _winningRank = tempHand.Last().rank;
            return true;
        }
    }
}
