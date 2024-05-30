using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class DeckController : MonoBehaviour
{
    private Stack<GameObject> cards = new Stack<GameObject>();
    private bool stackedMessy = false;

    private bool faceUp = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // add a card to the card stack making sure they match the deck orientation and setting them as a child of this deck in the scene
    public void AddCard(GameObject card){
        cards.Push(card);
        card.GetComponent<Transform>().SetParent(transform);

        CardController cardControllerScript = card.GetComponent<CardController>();
        if(faceUp){
            cardControllerScript.TurnFaceUp();
        } else {
            cardControllerScript.TurnFaceDown();
        }

        if(stackedMessy){
            //make rotation random
        }

        card.GetComponent<CardController>().MoveTo(transform.position + (Vector3.zero + (0.01f * this.cards.Count * (Vector3.left + Vector3.back)) + (0.005f * this.cards.Count * Vector3.up)));
    }

    // add every card in the given stack to the deck
    public void AddCards(Stack<GameObject> cards){
        cards.Reverse().ToList().ForEach(card => AddCard(card));
    }

    public void AddAllCards(Stack<GameObject> cards){
        cards.ToList().OrderBy(card => card.GetComponent<CardController>().GetSuit()).ThenBy(card => card.GetComponent<CardController>().GetValue()).ToList().ForEach(card => {
            AddCard(card);
        });
    }

    // change deck orientation to face up and re-add all cards
    public void TurnFaceUp(){
        faceUp = true;
        Stack<GameObject> tempDeck = cards;
        cards = new Stack<GameObject>();
        AddCards(tempDeck);
    }
    
    // change deck orientation to face down and re-add all cards
    public void TurnFaceDown(){
        faceUp = false;
        Stack<GameObject> tempDeck = cards;
        cards = new Stack<GameObject>();
        AddCards(tempDeck);
    }

    public Stack<GameObject> GetCards(){
        return cards;
    }

    public void OrderCards(){
        Stack<GameObject> orderedCards = cards;
        cards = new Stack<GameObject>();
        cards.ToList().OrderBy(card => card.GetComponent<CardController>().GetSuit()).ThenBy(card => card.GetComponent<CardController>().GetValue()).ToList().ForEach(card => {
            AddCard(card);
        });
    }
}
