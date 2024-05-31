using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;

public class WarGameManager : CardGameManager
{
    public GameObject deckPrefab, cardPrefab;
    DeckController mainDeck;
    DeckController[] playerDecks;
    public Sprite[] faceSprites;
    public Sprite[] backSprites;
    private readonly char[] suitChars = new char[]{'c', 'd', 'h', 's'};

    public override void SetupGame()
    {
        LoadCardSprites();
        mainDeck = CreateDeck(Vector3.zero, 0);
        playerDecks = new DeckController[] {
                                    CreateDeck(GameObject.Find("Player1").transform.position, 0, true),
                                    CreateDeck(GameObject.Find("Player2").transform.position, 0, true)
                                };

        mainDeck.DistributeCards(playerDecks);
    }

    public DeckController CreateDeck(Vector3 spawnPoint, int colorIndex, bool empty = false){
        DeckController newDeck =  Instantiate(deckPrefab, spawnPoint, quaternion.identity).GetComponent<DeckController>();
        if(empty){
            return newDeck;
        }

        CardController currentCard;
        Stack<CardController> newCards = new();

        for(int i = 3; i >= 0; i--){
            for(int j = 13; j > 0; j--){
                currentCard = Instantiate(cardPrefab, newDeck.transform).GetComponent<CardController>();
                currentCard.SetCard(j, suitChars[i], faceSprites[(13 * i) + j - 1], backSprites[colorIndex]);

                newCards.Push(currentCard);
            }
        }

        newDeck.AddCards(newCards);

        return newDeck;
    }
    private void LoadCardSprites(){
        faceSprites = Resources.LoadAll<Sprite>("CardFaceSprites")
                        .OrderBy(x => x.name[0])
                        .ThenBy(x => int.Parse(string.Concat(x.name.Where(char.IsDigit))))
                        .ToArray();
/*
        faceSprites= from sprite in Resources.LoadAll<Sprite>("CardFaceSprites")
                        orderby int.Parse(string.Concat(sprite.name.Where(char.IsDigit)))
                        orderby sprite.name[0]
                        select sprite;
*/
        backSprites = Resources.LoadAll<Sprite>("CardBackSprites");
    }
}
