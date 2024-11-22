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

public class gameMaster : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    //cardSprites is currently not working because values of rank enums were changed
    //Need to reorder it 
    [SerializeField] Sprite[] cardSprites = new Sprite[52];
    public Deck deck;
    public List<GameObject> playerObjects = new List<GameObject>();
    private List<Character> characters = new List<Character>();

    void Start()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 30;
        deck = new Deck();
        deck.PrintUnshuffled();
        deck.ShuffleDeck();

        //Game Start
        for (int i = 0; i < 4; i++)
        {

            characters.Add(new Character(playerObjects[i], "Player" + i));
        }   

        DealToFive();
        foreach (Character character in characters)
        {
            character.RateHand();
        }
        Character winner = GetWinner();
        
        Debug.Log(winner.name + " won with a " + winner.HandResult().ToString() + winner);

    }
    public void StartRound()
    {
        for(int i = 0; i < characters.Count; i++)
        {

        }
    }
    public Character GetWinner()
    {
        //This gets the list of characters with the best hand of that round
        List<Character> winners = characters.Where(x => x.GetHand().HandResult == characters.Max(g => g.GetHand().HandResult)).ToList();
        if (winners.Count() == 1)
            return winners[0];

        Debug.Log("Characters with same handResult");
        foreach (Character character in winners)
            Debug.Log(character);

        winners = winners.Where(x => x.GetHand().WinningRank == winners.Max(g => g.GetHand().WinningRank)).ToList();
        if (winners.Count() == 1)
            return winners[0];

        winners = winners.Where(x => x.GetHand().WinningRankSub == winners.Max(g => g.GetHand().WinningRankSub)).ToList();
        if(winners.Count() == 1)
            return winners[0];

        return null;

    }
    public void DealToFive()
    {
        for(int c = 0; c < characters.Count; c++)
            DealToFive(characters[c]);
    }

    public void DealToFive(Character character)
    {
        while(character.GetLength() < 5)
            character.AddCard(deck.DrawCard());
    }


}



