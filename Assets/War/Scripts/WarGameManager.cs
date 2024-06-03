using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;

public class WarGameManager : CardGameManager
{
    private Transform canvas;
    public GameObject playerPrefab, deckPrefab, cardPrefab;
    DeckController mainDeck;
    PlayerController[] players;
    public Sprite[] faceSprites;
    public Sprite[] backSprites;
    private readonly char[] suitChars = new char[]{'c', 'd', 'h', 's'};

    void Start(){
        canvas = GameObject.Find("Canvas").transform;
        SetupGame();
    }

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

    private void PlacePlayers()
    {
        GameObject[] playersObj = new GameObject[]{
            Instantiate(playerPrefab, canvas),
            Instantiate(playerPrefab, canvas)
        };
        Debug.Log($"p1: {playersObj[0]}\np2: {playersObj[1]}");
        playersObj[0].GetComponent<Transform>();
        players[0] = playersObj[0].GetComponent<PlayerController>();
        players[1] = playersObj[1].GetComponent<PlayerController>();

//        players[0] = Instantiate(playerPrefab, canvas).GetComponent<PlayerController>();
//        players[1] = Instantiate(playerPrefab, canvas).GetComponent<PlayerController>();
        
        players[0].SetPosition(PlayerController.PlayerPosition.CB);
        players[1].SetPosition(PlayerController.PlayerPosition.CT);
    }

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
