using UnityEngine;

public abstract class CardGameManager : MonoBehaviour
{
    void Start(){
        SetupGame();
    }

    public abstract void SetupGame();

}
