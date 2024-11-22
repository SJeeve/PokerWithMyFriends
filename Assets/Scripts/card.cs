using System;
using PokerEnums;
using UnityEngine;
namespace cardClass
{
    public class Card
    {
        public PokerEnums.PokerEnums.Suit suit;
        public PokerEnums.PokerEnums.Rank rank;
        public int cardIndex;
        public Card(int cardIndex)
        {
            this.cardIndex = cardIndex;
            suit = (PokerEnums.PokerEnums.Suit)(cardIndex / 13);
            rank = (PokerEnums.PokerEnums.Rank)(cardIndex % 13 + 2);
        }

        public Card()
        {
            this.suit = (PokerEnums.PokerEnums.Suit)0;
            this.rank = (PokerEnums.PokerEnums.Rank)0;
        }

        public int getIndex()
        {
            return cardIndex;
        }

        public override string ToString()
        {
            string output = "";
            output += Enum.GetName(typeof(PokerEnums.PokerEnums.Rank), rank);
            output += " of ";
            output += Enum.GetName(typeof(PokerEnums.PokerEnums.Suit), suit);
            return output;
        }


    }
}
