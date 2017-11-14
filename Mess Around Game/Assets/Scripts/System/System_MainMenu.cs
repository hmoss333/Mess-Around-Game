﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class System_MainMenu : MonoBehaviour {

	public enum Screens { MainMenu = 0, LevelSelect, Exit}
    public Screens screens;

    [Header("Main Menu UI")]
    public GameObject mainMenu;

    [Header("Level Select UI")]
    public GameObject levelSelect;

    [Header("Exit UI")]
    public GameObject exitMenu;
    
    // Use this for initialization
	void Start () {
        PlayerPrefs.SetInt("Streak", 0);

        screens = Screens.MainMenu;

        ScreenStateChange();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void ScreenStateChange ()
    {
        switch (screens)
        {
            case Screens.MainMenu:
                //show main menu here
                mainMenu.SetActive(true);
                levelSelect.SetActive(false);
                exitMenu.SetActive(false);
                break;
            case Screens.LevelSelect:
                //show level select here
                mainMenu.SetActive(false);
                levelSelect.SetActive(true);
                exitMenu.SetActive(false);
                break;
            case Screens.Exit:
                //show exit here
                mainMenu.SetActive(false);
                levelSelect.SetActive(false);
                exitMenu.SetActive(true);
                break;
            default:
                break;
        }
    }

    public void StartGame()
    {
        if (screens != Screens.LevelSelect)
            screens = Screens.LevelSelect;

        ScreenStateChange();
    }

    public void ExitGame ()
    {
        if (screens != Screens.Exit)
            screens = Screens.Exit;

        ScreenStateChange();
    }

    public void PlayGame()
    {
        SceneManager.LoadSceneAsync("Game");
    }

    public void BackButton()
    {
        if (screens != Screens.MainMenu)
            screens = Screens.MainMenu;

        ScreenStateChange();
    }

    public void YesButton()
    {
        Application.Quit();
    }

    //public void NoButton()
    //{
    //    if (screens != Screens.MainMenu)
    //        screens = Screens.MainMenu;
    //}
}
