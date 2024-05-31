using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class DeckController : MonoBehaviour
{
    private Stack<CardController> cards = new Stack<CardController>();
    private bool stackedMessy = false;

    private bool faceUp = false;

    // add a card to the card stack making sure they match the deck orientation and setting them as a child of this deck in the scene
    public void AddCard(CardController card){

        cards.Push(card);
        card.GetComponent<Transform>().SetParent(transform);
        
        if(faceUp){
            card.TurnFaceUp();
        } else {
            card.TurnFaceDown();
        }

        if(stackedMessy){
            //make rotation random
        }

        card.MoveTo(transform.position + (Vector3.zero + (0.01f * cards.Count * (Vector3.left + Vector3.back)) + (0.005f * cards.Count * Vector3.up)));
    }

    // add every card in the given stack to the deck
    public void AddCards(Stack<CardController> newCards){
        while (newCards.TryPop(out CardController card))
        {
            AddCard(card);
        }
    }

    // change deck orientation to face up and re-add all cards
    public void TurnFaceUp(){
        if(faceUp){
            return;
        }

        faceUp = true;
        Stack<CardController> tempDeck = cards;
        cards = new Stack<CardController>();
        AddCards(tempDeck);
    }
    
    // change deck orientation to face down and re-add all cards
    public void TurnFaceDown(){
        if(!faceUp){
            return;
        }

        faceUp = false;
        Stack<CardController> tempDeck = cards;
        cards = new Stack<CardController>();
        AddCards(tempDeck);
    }

    public Stack<CardController> GetCards(){
        return cards;
    }

    public void OrderCards(){
        Stack<CardController> orderedCards = cards;
        cards = new Stack<CardController>();
        cards.ToList().OrderBy(card => card.GetComponent<CardController>().GetSuit()).ThenBy(card => card.GetComponent<CardController>().GetValue()).ToList().ForEach(card => {
            AddCard(card);
        });
    }

    public void DistributeCards(DeckController[] players){
        int playerIndex = 0;
        while (cards.TryPop(out CardController card))
        {
            if(playerIndex == players.Length){
                playerIndex = 0;
            }
            players[playerIndex].AddCard(card);

            playerIndex++;
        }
    }

    public void DistributeCards(DeckController[] players, DeckController overflow){
        for (int i = 0; cards.TryPop(out CardController card); )
        {
            if(i == players.Length){
                i = 0;
                if(cards.Count < players.Length){
                    cards.Push(card);
                    overflow.AddCards(cards);
                    break;
                }
            }
            players[i].AddCard(card);
        }
    }

}
