using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MenuOptions : MonoBehaviour
{
    
    // Dirty Tags
    public bool GameOver = false;

    // Scene Management
    public string CardGame = "CardGame";
    public Canvas MainMenu;
    public Canvas OptionMenu;

    // helpers
    private List<Canvas> Menus;
    private bool GameWon = false;

    // Start is called before the first frame update
    void Start()
    {
        Menus = new List<Canvas> { MainMenu, OptionMenu };
        foreach (Canvas Menu in Menus)
        {
            if (Menu != MainMenu)
            {
                Menu.enabled = false;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (GameOver)
        {
            GameOverMenu();
        }
    }

    // Starts up the Game
    public void StartGame()
    {
        Debug.Log("Starting Game");
        SceneManager.LoadScene(CardGame);
    }

    // Goes to credit scene
    public void ViewCredits()
    {
        Debug.Log("Viewing Credits");
    }

    // Goes to options menu
    public void ViewOptions()
    {
        Debug.Log("Viewing Options");
    }

    // Game over state
    public void GameOverMenu()
    {
        if (GameWon == true)
        {

        }
        else
        {

        }
    }
}
