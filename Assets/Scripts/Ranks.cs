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
            King
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
            HighCard,
            OnePair,
            TwoPair,
            ThreeOfAKind,
            Straight,
            Flush,
            FullHouse,
            FourOfAKind,
            StraightFlush,
            RoyalFlush
        }

    }
}