using System;
using System.IO;
using UnityEngine;

public class MainManager : MonoBehaviour
{
    /// Singleton of MainManager
    public static MainManager Instance;
    public string playerName = "Player";
    public int gameTypeSelection;
    
    /**
        <summary>
            Loads save state or deletes this oject if a main manager already exists
        </summary>
        <param name="blank">a param</param>
        <returns>void</returns>
    **/
    private void Awake(){
        if(Instance != null){
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        LoadGameData();
    }

    /**
        <summary>
            Models game data for saving
        </summary>
        <param name="blank">a param</param>
        <returns>void</returns>
    **/
    [Serializable]
    public class GameData{
        public string playerName;
        public int gameTypeSelection;
    }
    /**
        <summary>
            save game data as a json file
        </summary>
        <returns>void</returns>
    **/
    public void SaveGameData(){
        GameData data = new(){
            playerName = playerName,
            gameTypeSelection = gameTypeSelection
        };

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/saveFile.json", json);
    }

    /**
        <summary>
            blah blah blah
        </summary>
        <param name="blank">a param</param>
        <returns>void</returns>
    **/
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
