using System;
using System.IO;
using UnityEngine;

public class MainManager : MonoBehaviour
{
    public static MainManager Instance;
    public string playerName = "Player";
    public int gameTypeSelection;
    

    private void Awake(){
        if(Instance != null){
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        LoadGameData();
    }

    [Serializable]
    public class GameData{
        public string playerName;
        public int gameTypeSelection;
    }

    public void SaveGameData(){
        GameData data = new(){
            playerName = playerName,
            gameTypeSelection = gameTypeSelection
        };

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/saveFile.json", json);
    }

    public void LoadGameData(){
        string path = Application.persistentDataPath + "/saveFile.json";
        if(File.Exists(path)){
            string json = File.ReadAllText(path);
            GameData data = JsonUtility.FromJson<GameData>(json);

            playerName = data.playerName;
            gameTypeSelection = data.gameTypeSelection;
            
        } else {
            playerName = "Player";
        }
    }
}
