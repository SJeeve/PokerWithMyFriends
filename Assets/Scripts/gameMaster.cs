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
        deck = new Deck(cardSprites);
        deck.ShuffleDeck();

        //Game Start
        for (int i = 0; i < 4; i++)
        {
            characters.Add(new Character(playerObjects[i], ("Player" + i)));
        }

        DealToFive();
        characters[0].GetHand().SetHand(new List<Card> { new Card(PokerEnums.PokerEnums.Suit.Spades, PokerEnums.PokerEnums.Rank.Ace, cardSprites[0]),
            new Card(PokerEnums.PokerEnums.Suit.Hearts, PokerEnums.PokerEnums.Rank.Ace, cardSprites[0]),
            new Card(PokerEnums.PokerEnums.Suit.Spades, PokerEnums.PokerEnums.Rank.Three, cardSprites[0]),
            new Card(PokerEnums.PokerEnums.Suit.Hearts, PokerEnums.PokerEnums.Rank.Three, cardSprites[0]),
            new Card(PokerEnums.PokerEnums.Suit.Spades, PokerEnums.PokerEnums.Rank.Ten, cardSprites[0]) });
        characters[0].GetHand().RateHand();
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
        while(character.GetLength() < 5)
        {
            character.AddCard(deck.DrawCard());
        }
    }


}



