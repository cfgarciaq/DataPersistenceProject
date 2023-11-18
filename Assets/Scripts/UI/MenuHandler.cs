using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuHandler : MonoBehaviour
{
    [SerializeField] private Text userErrorMessage;
    [SerializeField] private InputField playerNameInputField;
    // Start is called before the first frame update
    void Start()
    {
        HideUserErrorMessage();        
    }    

    public void StartGame()
    {
        //to do
        bool canStartGame = DataManager.Instance.SetNewPlayerName(playerNameInputField.text);
        if (canStartGame)
        {
            SceneManager.LoadScene("main");
        }
        else
        {
            //Display error message for User for a couple seconds
            float delay = 5;
            string msg = $"You Must Enter a Player Name to Play";
            DisplayUserErrorMessage(msg, delay);
        }
    }

    private void DisplayUserErrorMessage(string msg, float delay)
    {
        userErrorMessage.gameObject.SetActive(true);
        userErrorMessage.text = msg;
        //call hide method after 5secs
        Invoke(nameof(HideUserErrorMessage), delay);
    }

    private void HideUserErrorMessage()
    {
        userErrorMessage.text = "";
        userErrorMessage.gameObject.SetActive(false);
    }
}