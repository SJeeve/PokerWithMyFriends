using cardClass;
using characterClass;
using PokerEnums;
using System.Collections;
using System;
using UnityEngine;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework.Constraints;

public class trainingMaster : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Deck deck;
    public List<GameObject> playerObjects = new List<GameObject>();
    public List<pokerAgent> TrainingAgents = new List<pokerAgent>();
    private List<Character> characters = new List<Character>();
    void Start()
    {
        deck = new Deck();
        //Note: Whenever you call shuffledeck it clears the old deck and shuffles a new one
        deck.ShuffleDeck();

        //Game Start
        for (int i = 0; i < playerObjects.Count; i++)
        {
            TrainingAgents.Add(playerObjects[i].GetComponent<pokerAgent>());
        }
        RoundFlow();
    }
    public void RoundFlow()
    {
        Debug.Log("Beginning of start round");
        StartRound();
        Debug.Log("End of start round");
        DiscardForAgents();
        RevealCards();
        UpdateRankings();
    }
    public void StartRound()
    {
        //Start everyone with a full hand
        deck.ShuffleDeck();

        for(int i = 0; i < TrainingAgents.Count; i++)
        {
            DealToFive(TrainingAgents[i]);
            TrainingAgents[i].hand.RateHand();
            TrainingAgents[i].AdvanceStep();
        }
    }

    public void DiscardForAgents()
    {
        Debug.Log("Waiting for option select");
        for(int i = 0; i < TrainingAgents.Count; i++)
        {
            DealToFive(TrainingAgents[i]);
            TrainingAgents[i].AdvanceStep();
        }
    }

    public void RevealCards()
    {
        List<int[]> AllAgentHands = PopulateAllAgentHandsArray();
        for (int i = 0; i < TrainingAgents.Count; i++)
        {
            List<int[]> currentAgentHand = new List<int[]>
            {
                TrainingAgents[i].hand.GetHandIndexes()
            };
            TrainingAgents[i].SetAllAgentHands(AllAgentHands.Except(currentAgentHand, new IntArrayEqualityComparer()).ToList());
        }
    }
    public void UpdateRankings()
    {
        //Change this to hand score
        //This gets the list of characters with the best hand of that round
        List<pokerAgent> rankings = TrainingAgents.OrderByDescending(x => x.hand.HandScore).ToList();
        for(int i = 0; i < rankings.Count; i++)
        {
            rankings[i].place = i + 1;
        }
    }
    public List<int[]> PopulateAllAgentHandsArray()
    {
        List<int[]> AllHands = new List<int[]>();
        for(int r = 0; r < TrainingAgents.Count; r++)
        {
            AllHands.Add(TrainingAgents[r].hand.GetHandIndexes());
        }
        return AllHands;
    }
    public void DealToFive()
    {
        for (int i = 0; i < TrainingAgents.Count; i++)
            DealToFive(TrainingAgents[i]);
    }

    public void DealToFive(pokerAgent TrainingAgent)
    {
        Debug.Log("Deal To Five");
        Debug.Log(deck.DrawCard().ToString());
        while (TrainingAgent.hand.GetLength() < 5)
            TrainingAgent.AddCard(deck.DrawCard());
        Debug.Log($"TrainingAgent hand length = {TrainingAgent.hand.GetLength()}");
    }

    public class IntArrayEqualityComparer : IEqualityComparer<int[]> {
        
        public bool Equals(int[] x, int[] y)
        {
            return x.SequenceEqual(y);
        }

        public int GetHashCode(int[] obj)
        {
            return obj.Sum();
        }
    }

}



