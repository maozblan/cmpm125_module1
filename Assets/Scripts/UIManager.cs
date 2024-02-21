using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum MENU
{
    Start = 0,
    End = 1,
    HUD = 2,
}

public class UIManager : MonoBehaviour
{
    public List<Canvas> Menus;
    public GameObject CoinManagerObj;
    private CoinManager Manager;

    public GameObject Enemy1;
    public GameObject Enemy2;

    // dirty
    public bool GamePlaying = false;

    // Start is called before the first frame update
    void Start()
    {
        SwapMenu(MENU.Start);
        Manager = CoinManagerObj.GetComponent<CoinManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void CloseAllMenus()
    {
        for (int i = 0; i < Menus.Count; ++i)
        {
            Menus[i].enabled = false;
        }
    }

    void SwapMenu(MENU M)
    {
        // close all menus
        CloseAllMenus();
        // enable the right one
        Menus[(int)M].enabled = true;
    }

    // functions for buttons
    public void StartGame()
    {
        SwapMenu(MENU.HUD);
        GamePlaying = true;
    }
    public void RestartGame()
    {
        Manager.Reset();

        // reset enemies
        Enemy1.GetComponent<Enemy>().Reset();
        Enemy2.GetComponent<Enemy>().Reset();

        StartGame();
    }

    // other managment
    public void GameOver()
    {
        GamePlaying = false;
        SwapMenu(MENU.End);
    }
}
