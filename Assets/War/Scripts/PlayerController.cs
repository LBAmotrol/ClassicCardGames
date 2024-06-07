using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/**
    <summary>
        Controls a player object and its children
    </summary>
**/
public class PlayerController : MonoBehaviour
{
    private RectTransform rectTransform;
    private DeckController deck;
    private TextMeshProUGUI playerName;
    private Image portrait;

    /**
        <summary>
            Different positions the player can be on the screen
        </summary>
    **/
    public enum PlayerPosition{
        LeftTop, CenterTop, TopRight,
        LeftCenter, CenterCenter, RightCenter,
        LeftBottom, CenterBottom, RightBottom
    }

    /**
        <summary>
            Assigns object components to properties
        </summary>
    **/
    void Awake(){
        rectTransform = transform.GetComponent<RectTransform>();
        deck = transform.GetChild(0).GetComponent<DeckController>();
        playerName = transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        portrait = transform.GetChild(2).GetComponent<Image>();

        playerName.text = "Player";
    }

    /**
        <summary>
            Returns the name of the player
        </summary>
        <returns>The name of the player</returns>
    **/
    public string GetName(){
        return playerName.text;
    }

    /**
        <summary>
            Returns the player's deck
        </summary>
        <returns>The player's deck</returns>
    **/
    public DeckController GetDeck(){
        return deck;
    }

    /**
        <summary>
            Sets the name of the player
        </summary>
        <param name="playerName">The new name of the player</param>
    **/
    public void SetName(string playerName){
        this.playerName.text = playerName;
    }

    /**
        <summary>
            Moves the player to the given destination
        </summary>
        <param name="destination">The point to move the player</param>
    **/
    public void MovePlayer(Vector2 destination){
        rectTransform.position = destination;
    }

    /**
        <summary>
            Sets the player to one of the preset positions
        </summary>
        <param name="playerPosition">The position to place the player</param>
    **/
    public void SetPosition(PlayerPosition playerPosition){
        Vector2[] playerPositionVectors = new Vector2[]{
            new(0,0),     new(0, -185f),    new(0, 0),
            new(0,0),  new(0, 0), new(0, 0),
            new(0,0),     new(0, 185f),    new(0, 0)
        };

        rectTransform.anchoredPosition = playerPositionVectors[(int)playerPosition];
        //transform.GetComponentsInChildren<RectTransform>().ToList().ForEach(child => child.GetComponent<RectTransform>().pivot = playerPositionVectors[(int)playerPosition]);

        // ToDo: rotate deck for players on the sides
        /*switch (playerPosition)
        {
            case PlayerPosition.LC:
                //deck.rotate(-90);
                break;
            case PlayerPosition.RC:
                //deck.rotate(90);
                break;
            default:
                break;
        }*/

        SetAnchor(playerPosition);
        
    }

    /**
        <summary>
            Sets the 2D anchor for the player and its children            
        </summary>
        <param name="playerPosition">The position at which the player is placed</param>
    **/
    private void SetAnchor(PlayerPosition playerPosition){
        Vector2[] anchorVectors = new Vector2[]{
            new(0,1),     new(0.5f, 1),    new(1, 1),
            new(0,0.5f),  new(0.5f, 0.5f), new(1, 0.5f),
            new(0,0),     new(0.5f, 0),    new(1, 0)
        };

        rectTransform.anchorMin = rectTransform.anchorMax = anchorVectors[(int)playerPosition];

        transform.GetComponentsInChildren<RectTransform>().ToList().ForEach(child => child.anchorMin = child.anchorMax = anchorVectors[(int)playerPosition]);
    }
}
