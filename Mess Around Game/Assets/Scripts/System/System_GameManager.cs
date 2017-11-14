using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class System_GameManager : MonoBehaviour {

    public enum States { Playing, Lose, Pause}
    public States currentState;

    public bool gameOver;

    Player_Move player;

    //replace these later with PlayerPrefs that can be modified from settings menu
    public float height;
    public float width;
    public int numOfObstacles;
    public GameObject obstacle;

    public Text highStreak;
    public Text streak;


    private void Awake()
    {
        GenerateGameBoard(height, width, numOfObstacles, obstacle);
    }

    // Use this for initialization
    void Start () {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        player = GameObject.FindObjectOfType<Player_Move>();
        gameOver = false;
    }
	
	// Update is called once per frame
	void Update () {
        streak.text = "Score: " + PlayerPrefs.GetInt("Streak").ToString();
        highStreak.text = "High Score: " + PlayerPrefs.GetInt("HighStreak").ToString();
    }

    public void GenerateGameBoard (float maxHeight, float maxWidth, int numOfPrefabs, GameObject obstacleObject)
    {
        GameObject prefabs;

        for (int i = 0; i < numOfPrefabs; i++)
        {
            prefabs = Instantiate(obstacleObject, new Vector2(Random.Range(-maxWidth, maxWidth), Random.Range(1.5f, maxHeight)), Quaternion.identity);
        }

        //need a goal
        prefabs = Instantiate(obstacleObject, new Vector2(Random.Range(-maxWidth, maxWidth), Random.Range(1.5f, maxHeight)), Quaternion.identity);
        prefabs.GetComponent<Obstacle_Base>().isGoal = true;
    }

    public void GameOver ()
    {
        Debug.Log("Game Over");

        gameOver = true;
        player.GetComponent<Rigidbody2D>().simulated = false;
        PlayerPrefs.SetInt("Streak", 0);

        SceneManager.LoadScene("Game_New"); //may want to go back to main menu or popup a UI here, instead
    }

    public void NextLevel ()
    {
        Debug.Log("Success!");

        int streakCount = PlayerPrefs.GetInt("Streak");
        PlayerPrefs.SetInt("Streak", streakCount += 1);
        if (PlayerPrefs.GetInt("Streak") > PlayerPrefs.GetInt("HighStreak"))
            PlayerPrefs.SetInt("HighStreak", PlayerPrefs.GetInt("Streak"));

        SceneManager.LoadScene("Game_New");
    }
}
