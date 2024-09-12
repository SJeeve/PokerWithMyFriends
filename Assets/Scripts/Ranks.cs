using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace PokerEnums
{
    public class PokerEnums
    {

        public enum Rank
        {
            Ace = 12,
            Two = 0,
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
            BeginningAce = -1,
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