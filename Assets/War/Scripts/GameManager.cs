using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Sprite testSprite;
    public Sprite[] faceSprites;
    public Sprite[] backSprites;
    private char[] suitChars = new char[]{'c', 'd', 'h', 's'};
    public GameObject deckPrefab;
    public GameObject cardPrefab;
    private GameObject mainDeck;
    private DeckController mainDeckController;
    // Start is called before the first frame update
    void Start()
    {
        LoadCardSprites();
        
        mainDeck = CreateDeck(Vector3.zero, 0);
        mainDeckController = mainDeck.GetComponent<DeckController>();
        mainDeckController.TurnFaceUp();
        //mainDeckController.OrderCards();

        CreateDeck(new Vector3(-3, 3, 0), 2);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // creates a new deck at the given spawn point
    public GameObject CreateDeck(Vector3 spawnPoint, int colorIndex){
        GameObject newDeck =  Instantiate(deckPrefab);
        newDeck.GetComponent<DeckController>().TurnFaceDown();
        newDeck.transform.position = spawnPoint;
        
        GameObject currentCard;
        Stack<GameObject> newCards = new Stack<GameObject>();

        for(int i = 0; i < 4; i++){
            for(int j = 1; j <= 13; j++){
                currentCard = Instantiate(cardPrefab, newDeck.transform);
                currentCard.GetComponent<CardController>().SetCard(j, suitChars[i], faceSprites[(13 * i) + j - 1], backSprites[colorIndex]);

                newCards.Push(currentCard);
            }
        }

        newDeck.GetComponent<DeckController>().AddAllCards(newCards);

        return newDeck;
    }

    private void LoadCardSprites(){
        faceSprites = Resources.LoadAll<Sprite>("CardFaceSprites");
        backSprites = Resources.LoadAll<Sprite>("CardBackSprites");
    }
}
