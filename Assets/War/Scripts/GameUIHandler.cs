using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameUIHandler : MonoBehaviour
{
    public TextMeshProUGUI playerNameText;
    // Start is called before the first frame update
    void Start()
    {
        playerNameText.text = MainManager.Instance.playerName;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
