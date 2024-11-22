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
        deck.ShuffleDeck();

        //Game Start
        for (int i = 0; i < 4; i++)
        {
            TrainingAgents.Add(playerObjects[i].GetComponent<pokerAgent>());
            characters.Add(TrainingAgents[i].agent);
        }

    }
    public void StartRound()
    {
        //Start everyone with a full hand
        DealToFive();

        for(int i = 0; i < 4; i++)
        {
            characters[i].RateHand();
            TrainingAgents[i].AdvanceStep();
        }
    }

    public void DiscardForAgents()
    {
        for(int i = 0; i < 4; i++)
        {
            TrainingAgents[i].RequestDecision();
        }
    }
    public Character[] GetWinner()
    {
        //Change this to hand score
        //This gets the list of characters with the best hand of that round
        List<Character> winners = characters.OrderByDescending(x => x.GetHand().HandScore).ToList();
        return winners.ToArray();
    }
    public void DealToFive()
    {
        for (int c = 0; c < characters.Count; c++)
            DealToFive(characters[c]);
    }

    public void DealToFive(Character character)
    {
        while (character.GetLength() < 5)
            character.AddCard(deck.DrawCard());
    }


}



