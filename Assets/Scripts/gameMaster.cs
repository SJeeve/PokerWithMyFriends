using cardClass;
using characterClass;
using System.Collections;
using System;
using UnityEngine;
using NUnit.Framework;
using System.Collections.Generic;

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

        InitialDeal();

    }

    public void InitialDeal()
    {
        for(int c = 0; c < characters.Count; c++)
        {
            for(int d = 0; d < 5; d++)
            {

                characters[c].AddCard(deck.DrawCard());
            }
            Debug.Log(characters[c].ToString());
        }
    }

}


