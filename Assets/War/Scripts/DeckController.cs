using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DeckController : MonoBehaviour
{
    private Stack<CardController> cards = new Stack<CardController>();
    private bool stackedMessy = false;

    private bool faceUp = false;

    public enum DeckType{
        regular,
        player
    }

    public DeckType deckType = DeckType.regular;

    // add a card to the card stack making sure they match the deck orientation and setting them as a child of this deck in the scene
    public void AddCard(CardController newCard){

        
        newCard.GetComponent<Transform>().SetParent(transform);
        
        if(faceUp){
            newCard.TurnFaceUp();
        } else {
            newCard.TurnFaceDown();
        }

        if(stackedMessy){
            //make rotation random
        }

        Vector3 cardOffset;

        switch (deckType)
        {
            case DeckType.regular:
                cardOffset = new Vector3(.1f, 0.005f, -0.1f);
                newCard.MoveTo(cardOffset.x * cards.Count * Vector3.left + 
                            cardOffset.y * cards.Count * Vector3.up +
                            cardOffset.z * cards.Count * Vector3.forward);
                break;
            // seperates cards by a quarter of their width and makes sure that the deck is centered on the deck position
            case DeckType.player:
                float cardWidth = newCard.GetComponent<RectTransform>().rect.width;
                foreach(CardController card in cards){
                    card.Translate(0.25f * cardWidth * Vector3.left + 0.1f * cards.Count * Vector3.up);
                }
                newCard.Translate(0.25f * cards.Count * cardWidth * Vector3.right);
                break;
            default:
                break;
        }
        
        
    }

    /**
        <summary>
            Add every card in the given stack to the deck
        </summary>
        <param name="newCards">A stack of new cards to be added to the deck</param>
        <returns>void</returns>
    **/
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
