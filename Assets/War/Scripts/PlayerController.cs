using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private RectTransform rectTransform;
    private DeckController deck;
    private TextMeshProUGUI name;
    private Image portrait;

    public enum PlayerPosition{
        LT, CT, TR,
        LC, CC, RC,
        LB, CB, RB
    }

    void Start(){
        rectTransform = transform.GetComponent<RectTransform>();
        deck = transform.GetChild(0).GetComponent<DeckController>();
        name = transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        portrait = transform.GetChild(2).GetComponent<Image>();

        name.text = MainManager.Instance.name;
    }
    public string GetName(){
        return name.text;
    }

    public DeckController GetDeck(){
        return deck;
    }

    public void SetName(string name){
        this.name.text = name;
    }

    public void MovePlayer(Vector2 destination){
        rectTransform.position = destination;
    }

    public void SetPosition(PlayerPosition playerPosition){
        Vector2[] playerPositionVectors = new Vector2[]{
            new(0,0),     new(185f, 0),    new(0, 0),
            new(0,0),  new(0, 0), new(0, 0),
            new(0,0),     new(-185f, 0),    new(0, 0)
        };

        transform.GetComponentsInChildren<RectTransform>().ToList().ForEach(child => child.position = playerPositionVectors[(int)playerPosition]);

        // rotate deck for players on the sides
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
