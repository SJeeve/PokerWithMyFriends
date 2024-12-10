using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace PokerEnums
{
    public class PokerEnums
    {

        public enum Rank : long
        {
            Two = 2,
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
            Ace,
            BeginningAce = -1,
            Nothing = 0,
        }
        public enum Suit
        {
            Clubs = 0,
            Diamonds,
            Hearts,
            Spades
        }
        //Thanks Mr.Connor
        public enum HandResults 
        {
            None = 0,
            HighCard = 1,
            OnePair = 2,
            TwoPair = 3,
            ThreeOfAKind = 4,
            Straight = 5,
            Flush = 6,
            FullHouse = 7,
            FourOfAKind = 8,
            StraightFlush = 9,
            RoyalFlush = 10,
        }

    }
}
