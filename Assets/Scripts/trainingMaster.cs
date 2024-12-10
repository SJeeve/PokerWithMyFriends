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
using UnityEngine.XR;
public class trainingMaster : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private int num = 0;
    public Deck deck = new Deck();
    public List<GameObject> playerObjects = new List<GameObject>();
    public List<pokerAgent> TrainingAgents = new List<pokerAgent>();
    private List<Character> characters = new List<Character>();
    private cardClass.Hand testHand = new cardClass.Hand();
    void Start()
    {
        testHand.SetHand(new List<Card>() { new Card(1), new Card(51), new Card(50), new Card(4), new Card(5)});
        Debug.Log($"TestHand check\n{testHand.HandResult}");
        //Note: Whenever you call shuffledeck it clears the old deck and shuffles a new one
        for (int i = 0; i < playerObjects.Count; i++)
        {
            TrainingAgents.Add(playerObjects[i].GetComponent<pokerAgent>());
        }

    }
    //What this should do is that every fixed update it'll check if all the agents are done, and if they are it'll start a new round
    private void FixedUpdate()
    {

        if ((!TrainingAgents.Any(pa => pa.GetReadyForNewRound() == false) && num < 2))
        {
            for(int i = 0; i < TrainingAgents.Count; i++)
            {
                TrainingAgents[i].hand.DiscardAll();
            }
            ResetField();
            RoundFlow();
            num++;
        }
    }
    //This function is pretty empty as of now, but I'll need it for when I implement betting and multiple players
    private void ResetField()
    {
        deck.ShuffleDeck();
    }    
    public void RoundFlow()
    {
        StartRound();
        DiscardForAgents();
        UpdateRankings();
        EndAgentEpisodes();
    }
    public void StartRound()
    {
        Debug.Log("Beginning of start round");
        //Start everyone with a full hand
        deck.ShuffleDeck();
        for(int i = 0; i < TrainingAgents.Count; i++)
        {
            TrainingAgents[i].setReadyForNewRound(false);
            DealToFive(TrainingAgents[i]);
            Debug.Log("StartRound advanceStep");
            TrainingAgents[i].AdvanceStep();
        }
        Debug.Log("End of start round");
    }

    public void DiscardForAgents()
    {
        for(int i = 0; i < TrainingAgents.Count; i++)
        {
            DealToFive(TrainingAgents[i]);
            Debug.Log("Discard advanceStep");
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
        List<pokerAgent> rankings = TrainingAgents.OrderByDescending(x => x.hand.HandScore).ToList();
        for(int i = 0; i < rankings.Count; i++)
        {
            //rankings[i].place = i + 1;
        }
    }
    public void EndAgentEpisodes()
    {
        for(int i = 0; i < TrainingAgents.Count; i++)
        {
            TrainingAgents[i].RewardAndEndEpisode();
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



