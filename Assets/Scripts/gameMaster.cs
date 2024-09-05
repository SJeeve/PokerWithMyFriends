using cardClass;
using characterClass;
using PokerEnums;
using System.Collections;
using System;
using UnityEngine;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

public class gameMaster : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] Sprite[] cardSprites = new Sprite[52];
    public Deck deck;
    public List<GameObject> players = new List<GameObject>();
    private List<Character> characters = new List<Character>();
    void Start()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 30;
        deck = new Deck(cardSprites);
        deck.ShuffleDeck();

        //Game Start
        for (int i = 0; i < 4; i++)
        {
            characters.Add(new Character(players[i], ("Player" + i)));
        }

        DealToFive();

    }
    public Character GetWinner()
    {
        //Won't check for ties currently
        return characters.OrderByDescending(h => h.HandResult()).First();
    }
    public void DealToFive()
    {
        for(int c = 0; c < characters.Count; c++)
        {
            DealToFive(characters[c]);
            Debug.Log(characters[c].ToString());
        }
    }

    public void DealToFive(Character character)
    {
        while(character.GetLength() <= 5)
        {
            character.AddCard(deck.DrawCard());
        }
    }

}


