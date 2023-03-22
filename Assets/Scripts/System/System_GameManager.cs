using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class System_GameManager : MonoBehaviour {

    public static System_GameManager instance;

    public enum States { Playing, Pause, GameOver}
    public States currentState;

    //public static bool gameOver;

    Player_Move player;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    // Use this for initialization
    void Start () {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        player = GameObject.FindObjectOfType<Player_Move>();

        currentState = States.Playing;
	}
	
	// Update is called once per frame
	void Update () {
        if (player.isHit && currentState == States.Playing)
        {
            currentState = States.GameOver;
            Debug.Log("game over");
        }
	}
}
