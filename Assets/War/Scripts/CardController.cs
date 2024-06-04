using UnityEngine;
using UnityEngine.UI;

/**
    <summary>
        Controls a card object
    </summary>
**/
public class CardController : MonoBehaviour
{
    public CardSuit suit = CardSuit.none;
    public int value = 0;
    public bool faceUp = true;
    public Sprite[] cardPrints = new Sprite[2];
    public Image cardImage;

    /**
        <summary>
            Assigns the component for the card image
        </summary>
    **/
    void Start()
    {
        cardImage = GetComponent<Image>();
    }

    /**
        <summary>
            returns true if card is facing up
        </summary>
        <returns>true if the card is face up, false if it is face down</returns>
    **/
    public bool IsFaceUp(){
        return faceUp;
    }

    /**
        <summary>
            Changes the card image between face up (its value and suit) and card back
        </summary>
    **/
    public void TurnCard(){
        if(cardImage == null){
            cardImage = GetComponent<Image>();
        }
        faceUp = !faceUp;
        if(faceUp){
            cardImage.sprite = cardPrints[0];
        } else {
            cardImage.sprite = cardPrints[1];
        }
    }

    /**
        <summary>
            Changes card image to be face up (its value and suit)
        </summary>
    **/
    public void TurnFaceUp(){
        faceUp = true;
        
        if(cardImage == null){
            cardImage = GetComponent<Image>();
        }
        cardImage.sprite = cardPrints[0];
    }

    /**
        <summary>
            Changes card image to be face down (its card back)
        </summary>
    **/
    public void TurnFaceDown(){

        faceUp = false;

        if(cardImage == null){
            cardImage = GetComponent<Image>();
        }
        cardImage.sprite = cardPrints[1];
    }

    /**
        <summary>
            Returns the value of the card
        </summary>
        <returns>Value of the card</returns>
    **/
    public int GetValue(){
        return value;
    }

    /**
        <summary>
            Sets the value of the card
        </summary>
        <param name="value">Face value of the card</param>
    **/
    public void SetValue(int value){
        this.value = value;
    }

    /**
        <summary>
            Returns the suit of the card
        </summary>
        <returns>Suit of the card</returns>
    **/
    public CardSuit GetSuit(){
        return suit;
    }

    /**
        <summary>
            Sets the suit of the card
        </summary>
        <param name="suit">Suit of the card</param>
    **/
    public void SetSuit(CardSuit suit){
        this.suit = suit;
    }

    /**
        <summary>
            Sets the sprites, value, and suit of the card
        </summary>
        <param name="value">Value of the card</param>
        <param name="suit">Suit of the card</param>
        <param name="facePrint">Face image of the card</param>
        <param name="backPrint">Card back image</param>
    **/
    public void SetCard(int value, CardSuit suit, Sprite facePrint, Sprite backPrint){
        this.value = value;
        this.suit = suit;
        cardPrints = new Sprite[] {facePrint, backPrint};
    }
    
    /**
        <summary>
            moves the position of the card to the given destination
        </summary>
        <param name="destination">The point to which the card moves</param>
    **/
    public void MoveTo(Vector3 destination){
        transform.position = destination;
    }

    /**
        <summary>
            Translates the position of the card by the given translation
        </summary>
        <param name="translation">The Distance and direction to move the card</param>
    **/
    public void Translate(Vector3 translation){
        transform.Translate(translation);
    }
}
