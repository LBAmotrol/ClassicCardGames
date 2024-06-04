using UnityEngine;

/**
    <summary>
        Abstract class which is a template for card game managers
    </summary>
**/
public abstract class CardGameManager : MonoBehaviour
{
    protected Transform canvas;

    /**
        <summary>
            Sets up the card game then starts the game loop
        </summary>
    **/
    void Start(){
        canvas = GameObject.Find("Canvas").transform;
        SetupGame();
    }

    /**
        <summary>
            Abstract class which sets up the card game
        </summary>
    **/
    public abstract void SetupGame();

}
