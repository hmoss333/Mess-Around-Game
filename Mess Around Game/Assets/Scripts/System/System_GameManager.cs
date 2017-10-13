using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class System_GameManager : MonoBehaviour {

    public enum States { Playing, Lose, Pause}
    public States currentState;

    public static bool gameOver;

    Player_Move player;

    // Use this for initialization
    void Start () {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        player = GameObject.FindObjectOfType<Player_Move>();

        gameOver = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (player.isHit && !gameOver)
        {
            gameOver = true;
            currentState = States.Lose;
            Debug.Log("game over");
        }
	}
}
