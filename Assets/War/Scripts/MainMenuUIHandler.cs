using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


#if UNITY_EDITOR
using UnityEditor;
# endif

public class MainMenuUIHandler : MonoBehaviour
{
    public TMP_Dropdown gameTypeDropdown;
    public TMP_InputField nameInput;
    // Start is called before the first frame update
    void Start()
    {
        nameInput.text = MainManager.Instance.playerName;
        gameTypeDropdown.value = MainManager.Instance.gameTypeSelection;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame(){
        SaveMenuData();

        SceneManager.LoadScene(gameTypeDropdown.value + 1);
    }

    public void Exit(){
        SaveMenuData();

        # if UNITY_EDITOR
            EditorApplication.ExitPlaymode();
        # else
            Application.Quit();
        # endif
    }

    private void SaveMenuData(){
        MainManager.Instance.playerName = nameInput.text;
        MainManager.Instance.gameTypeSelection = gameTypeDropdown.value;
        MainManager.Instance.SaveGameData();
    }

}
