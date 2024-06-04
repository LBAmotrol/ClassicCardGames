using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/**
    <summary>
        Controls a deck object
    </summary>
**/
public class DeckController : MonoBehaviour
{
    private Stack<CardController> cards = new Stack<CardController>();
    private bool stackedMessy = false;

    private bool faceUp = false;

    /**
        <summary>
            Enum for possible deck types
        </summary>
    **/
    public enum DeckType{
        regular,
        player
    }

    public DeckType deckType = DeckType.regular;

    // 
    /**
        <summary>
            Add a card as a child and to the card stack, matching orientation and position
        </summary>
        <param name="newCard">The card to be added</param>
    **/
    public void AddCard(CardController newCard){

        
        newCard.GetComponent<Transform>().SetParent(transform);
        
        if(faceUp){
            newCard.TurnFaceUp();
        } else {
            newCard.TurnFaceDown();
        }

        if(stackedMessy){
            //ToDo: make rotation random
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
                float spacingRatio = 0.25f;
                foreach(CardController card in cards){
                    card.Translate(spacingRatio * cardWidth * Vector3.left + 0.1f * cards.Count * Vector3.up);
                }
                newCard.Translate(spacingRatio * cards.Count * cardWidth * Vector3.right);
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
    **/
    public void AddCards(Stack<CardController> newCards){
        while (newCards.TryPop(out CardController card))
        {
            AddCard(card);
        }
    }

    /**
        <summary>
            change deck orientation to face up and re-add all cards, correcting their image
        </summary>
    **/
    public void TurnFaceUp(){
        if(faceUp){
            return;
        }

        faceUp = true;
        Stack<CardController> tempDeck = cards;
        cards = new Stack<CardController>();
        AddCards(tempDeck);
    }
    
    /**
        <summary>
            change deck orientation to face down and re-add all cards, correcting their image
        </summary>
    **/
    public void TurnFaceDown(){
        if(!faceUp){
            return;
        }

        faceUp = false;
        Stack<CardController> tempDeck = cards;
        cards = new Stack<CardController>();
        AddCards(tempDeck);
    }

    /**
        <summary>
            Returns the stack of cards
        </summary>
        <returns>The stack of all cards in the deck</returns>
    **/
    public Stack<CardController> GetCards(){
        return cards;
    }

    /**
        <summary>
            Orders the cards by suit then value
        </summary>
    **/
    public void OrderCards(){
        Stack<CardController> orderedCards = cards;
        cards = new Stack<CardController>();
        cards.ToList().OrderBy(card => card.GetComponent<CardController>().GetSuit()).ThenBy(card => card.GetComponent<CardController>().GetValue()).ToList().ForEach(card => {
            AddCard(card);
        });
    }

    /**
        <summary>
            Distributes the cards evenly among given decks (extra cards will make it uneven)
        </summary>
        <param name="decks">all decks to distribute the cards between</param>
    **/
    public void DistributeCards(DeckController[] decks){
        int playerIndex = 0;
        while (cards.TryPop(out CardController card))
        {
            if(playerIndex == decks.Length){
                playerIndex = 0;
            }
            decks[playerIndex].AddCard(card);

            playerIndex++;
        }
    }

    /**
        <summary>
            Distributes the cards evenly among given decks, extra cards being put in the given overflow deck
        </summary>
        <param name="decks">all decks to distribute the cards between</param>
        <param name="overflow">destination for remaining cards</param>
    **/
    public void DistributeCards(DeckController[] decks, DeckController overflow){
        for (int i = 0; cards.TryPop(out CardController card); )
        {
            if(i == decks.Length){
                i = 0;
                if(cards.Count < decks.Length){
                    cards.Push(card);
                    overflow.AddCards(cards);
                    break;
                }
            }
            decks[i].AddCard(card);
        }
    }

}
