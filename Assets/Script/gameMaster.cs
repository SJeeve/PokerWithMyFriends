using cardClass;
using System.Collections;
using System;
using UnityEngine;

public class gameMaster : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] Sprite[] cardSprites = new Sprite[52];
    GameObject[] cards = new GameObject[52];
    public GameObject cardPrefab;
    public BaseDeck startDeck;
    public 
    void Start()
    {
        startDeck = new BaseDeck(cardSprites);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}


