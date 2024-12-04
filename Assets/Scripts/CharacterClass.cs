using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using cardClass;
namespace characterClass
{
    interface Character
    {


        public void AddCard(Card card);
        public void DiscardCard(Card card);
        public void DiscardCard(int i);
        public PokerEnums.PokerEnums.HandResults HandResult();
        public string ToString();
    }
}
