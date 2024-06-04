using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;

/**
    <summary>
        A card game manager for War
    </summary>
**/
public class WarGameManager : CardGameManager
{
    public GameObject playerPrefab, deckPrefab, cardPrefab;
    DeckController mainDeck;
    PlayerController[] players;
    public Sprite[] faceSprites;
    public Sprite[] backSprites;
    private readonly char[] suitChars = new char[]{'c', 'd', 'h', 's'};

    /**
        <summary>
            Sets up the card game
        </summary>
    **/
    public override void SetupGame()
    {
        LoadCardSprites();

        Debug.Log("placing players");
        PlacePlayers();
        Debug.Log("players placed");


        mainDeck = CreateDeck(Vector3.zero, 0);
        Debug.Log("main deck created");

        mainDeck.DistributeCards(players.ToList()
                                        .Select(player => player.GetDeck())
                                        .ToArray()
        );
    }

    /**
        <summary>
            Places the players on the screen
        </summary>
    **/
    private void PlacePlayers()
    {
        //creates players
        GameObject[] playersObj = new GameObject[]{
            Instantiate(playerPrefab, canvas),
            Instantiate(playerPrefab, canvas)
        };
        Debug.Log($"p1: {playersObj[0]}\np2: {playersObj[1]}");

        // ensures that the objects are correctly assigned, and components can be selected
        playersObj[0].GetComponent<Transform>();
        playersObj[1].GetComponent<Transform>();

        // breaks here
        players[0] = playersObj[0].GetComponent<PlayerController>();
        players[1] = playersObj[1].GetComponent<PlayerController>();

// bugged       players[0] = Instantiate(playerPrefab, canvas).GetComponent<PlayerController>();
// bugged       players[1] = Instantiate(playerPrefab, canvas).GetComponent<PlayerController>();
        
        players[0].SetPosition(PlayerController.PlayerPosition.CenterBottom);
        players[1].SetPosition(PlayerController.PlayerPosition.CenterTop);

        players[0].SetName(MainManager.Instance.name);
        players[1].SetName("The Enemy");
    }

    /**
        <summary>
            Creates a deck object
        </summary>
        <param name="spawnPoint">The starting position of the deck</param>
        <param name="colorIndex">The selected back color of the deck</param>
        <param name="empty">If the deck is to be filled with cards or not</param>
        <param name="parent">The selected parent of the deck (game canvas if null)</param>
    **/
    public DeckController CreateDeck(Vector3 spawnPoint, int colorIndex, bool empty = false, Transform parent = null){
        if(parent == null){
            parent = canvas;
        }

        DeckController newDeck =  Instantiate(deckPrefab, spawnPoint, quaternion.identity, parent).GetComponent<DeckController>();
        if(empty){
            return newDeck;
        }

        CardController currentCard;
        Stack<CardController> newCards = new();

        CardSuit[] suits = new CardSuit[]{
            CardSuit.club,
            CardSuit.diamond,
            CardSuit.heart,
            CardSuit.spade
        };
        for(int i = 3; i >= 0; i--){
            for(int j = 13; j > 0; j--){
                currentCard = Instantiate(cardPrefab, newDeck.transform).GetComponent<CardController>();
                currentCard.SetCard(j, suits[i], faceSprites[(13 * i) + j - 1], backSprites[colorIndex]);

                newCards.Push(currentCard);
            }
        }

        newDeck.AddCards(newCards);

        return newDeck;
    }

    /**
        <summary>
            Loads card sprite from the resources directory in assets
        </summary>
    **/
    private void LoadCardSprites(){
        faceSprites = Resources.LoadAll<Sprite>("CardFaceSprites")
                        .OrderBy(x => x.name[0])
                        .ThenBy(x => int.Parse(string.Concat(x.name.Where(char.IsDigit))))
                        .ToArray();
        backSprites = Resources.LoadAll<Sprite>("CardBackSprites");
    }
}
