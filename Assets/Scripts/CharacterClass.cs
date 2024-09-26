using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using cardClass;
namespace characterClass
{
    public class Character
    {
        GameObject character;
        private Hand hand;
        public string name;
        public Character(GameObject character, string name) {
            this.character = character;
            this.name = name;
            hand = new Hand();
        }
        
        public void AddCard(Card card)
        {
            Debug.Log($"Added {card} to {name}'s hand");
            hand.AddCard(card);
        }
        public void DiscardCard(Card card)
        {
            Debug.Log($"Removed {card} from {name}'s hand");
            hand.DiscardCard(card);
        }
        public void DiscardCard(int i) 
        {
            Debug.Log($"Removed card at index {i} from {name}'s hand");
            hand.DiscardCard(i);
        }
        public int GetLength()
        {
            return hand.GetLength();
        }
        public PokerEnums.PokerEnums.HandResults HandResult()
        {
            return hand.HandResult;
        }
        public void RateHand()
        {
            hand.RateHand();
        }
        public Hand GetHand()
        {
            return hand;
        }
        public void DiscardAll()
        {
            hand.DiscardAll();
        }
        public override string ToString()
        {
            string output = $"{name} has a \n";
            for(int i = 0; i < hand.GetLength(); i++)
            {
                output += hand.GetCard(i);
                output += "\n";
            }
            output += "Result of hand: " + HandResult().ToString();
            return output;
        }
    }
}
