using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class System_GameManager : MonoBehaviour {

    public enum States { Playing, Lose, Pause}
    public States currentState;

    public enum Modes { Dodge, Maze, Challenge}
    public Modes currentMode;

    public static bool gameOver;

    Player_Move player;
    System_Generator sg;

    // Use this for initialization
    void Start () {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        player = GameObject.FindObjectOfType<Player_Move>();
        sg = GameObject.FindObjectOfType<System_Generator>();

        gameOver = false;

        if (PlayerPrefs.GetInt("GameState") == 0)
            currentMode = Modes.Dodge;
        else if (PlayerPrefs.GetInt("GameState") == 1)
            currentMode = Modes.Maze;
        else
            currentMode = Modes.Challenge;
    }
	
	// Update is called once per frame
	void Update () {
        if (player.isHit && !gameOver)
        {
            gameOver = true;
            currentState = States.Lose;
            Debug.Log("game over");
            SceneManager.LoadSceneAsync("MainMenu");
        }
	}
}
