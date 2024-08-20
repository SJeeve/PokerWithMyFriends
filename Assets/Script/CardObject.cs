using System.Collections;
using System.Collections.Generic;
using cardClass;
using UnityEngine;

public class CardObject : MonoBehaviour
{
    // Start is called before the first frame update
    public Card data;
    void Start()
    {

    }

    public void newCard(int suit, int rank, Sprite cardSprite)
    {
        data = new Card(suit, rank, cardSprite);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
