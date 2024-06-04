using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


#if UNITY_EDITOR
using UnityEditor;
# endif

/**
    <summary>
        Handles the Ui of the game's main menu
    </summary>
**/
public class MainMenuUIHandler : MonoBehaviour
{
    public TMP_Dropdown gameTypeDropdown;
    public TMP_InputField nameInput;
    
    /**
        <summary>
            sets the text and game type value of the UI
        </summary>
    **/
    void Start()
    {
        nameInput.text = MainManager.Instance.playerName;
        gameTypeDropdown.value = MainManager.Instance.gameTypeSelection;
    }

    /**
        <summary>
            Starts the selected card game, and saves the game
        </summary>
    **/
    public void StartGame(){
        SaveMenuData();

        SceneManager.LoadScene(gameTypeDropdown.value + 1);
    }

    /**
        <summary>
            Exits the application, and saves the game
        </summary>
    **/
    public void Exit(){
        SaveMenuData();

        # if UNITY_EDITOR
            EditorApplication.ExitPlaymode();
        # else
            Application.Quit();
        # endif
    }

    /**
        <summary>
            Saves the menu data using the main manager
        </summary>
    **/
    private void SaveMenuData(){
        MainManager.Instance.playerName = nameInput.text;
        MainManager.Instance.gameTypeSelection = gameTypeDropdown.value;
        MainManager.Instance.SaveGameData();
    }

}
