using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardController : MonoBehaviour
{
    public char suit = 'x';
    public int value = 0;
    public bool faceUp = true;
    public Sprite[] cardPrints = new Sprite[2];
    public SpriteRenderer SR;

    // Start is called before the first frame update
    void Start()
    {
        SR = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TurnCard(){
        if(SR == null){
            SR = GetComponent<SpriteRenderer>();
        }
        faceUp = !faceUp;
        if(faceUp){
            SR.sprite = cardPrints[0];
        } else {
            SR.sprite = cardPrints[1];
        }
    }

    public bool IsFaceUp(){
        return faceUp;
    }

    public void TurnFaceUp(){
        faceUp = true;
        
        if(SR == null){
            SR = GetComponent<SpriteRenderer>();
        }
        SR.sprite = cardPrints[0];
    }

    public void TurnFaceDown(){

        faceUp = false;

        if(SR == null){
            SR = GetComponent<SpriteRenderer>();
        }
        SR.sprite = cardPrints[1];
    }

    public void SetValue(int value){
        this.value = value;
    }

    public void SetSuit(char suit){
        this.suit = suit;
    }

    public int GetValue(){
        return value;
    }

    public char GetSuit(){
        return suit;
    }

    public void SetCard(int value, char suit, Sprite facePrint, Sprite backPrint){
        this.value = value;
        this.suit = suit;
        cardPrints = new Sprite[] {facePrint, backPrint};
    }
    
    public void MoveTo(Vector3 destination){
        transform.position = destination;
    }
}
