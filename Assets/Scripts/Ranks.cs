using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace PokerEnums
{
    public class PokerEnums
    {

        public enum Rank
        {
            Ace = 0,
            Two,
            Three,
            Four,
            Five,
            Six,
            Seven,
            Eight,
            Nine,
            Ten,
            Jack,
            Queen,
            King,
            //EndAce is not assigned to any cards on start, but when deciding the winning rank on a straight will apply if the hand is a 10-A straight
            EndAce,
        }
        public enum Suit
        {
            Clubs = 0,
            Diamonds,
            Hearts,
            Spades
        }
        public enum HandResults 
        {
            None = -1,
            HighCard = 1,
            OnePair = 2,
            TwoPair = 3,
            ThreeOfAKind = 4,
            FourOfAKind = 8,
            Straight = 5,
            Flush = 6,
            FullHouse = 7,
            StraightFlush = 9,
            RoyalFlush = 10,
        }

    }
}