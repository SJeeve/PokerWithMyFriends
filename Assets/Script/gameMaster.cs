using cardClass;
using UnityEngine;

public class gameMaster : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] Sprite[] cardSprites = new Sprite[52];
    GameObject[] cards = new GameObject[52];
    public GameObject cardPrefab;
    void Start()
    {
        GameObject currentCardOb = null;
        Card currentCard = null;
        for(int i = 0; i < cards.Length; i++)
        {
            currentCard = new Card(i/13, i%13, cardSprites[i]);

            //currentCard = Instantiate(cardPrefab, new Vector2(0, 0), Quaternion.identity);

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
