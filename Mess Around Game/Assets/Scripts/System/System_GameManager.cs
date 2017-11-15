using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class System_GameManager : MonoBehaviour {

    public bool gameOver;

    Player_Move player;

    //replace these later with PlayerPrefs that can be modified from settings menu
    public float height;
    public float width;
    public int numOfObstacles;
    public GameObject obstacle;

    public Text highStreak;
    public Text streak;
    public GameObject gameOverPanel;


    private void Awake()
    {
        GenerateGameBoard(height, width, numOfObstacles, obstacle);
    }

    // Use this for initialization
    void Start () {
        player = GameObject.FindObjectOfType<Player_Move>();
        gameOver = false;
        gameOverPanel.SetActive(false);

        streak.text = "Score: " + PlayerPrefs.GetInt("Streak").ToString();
        highStreak.text = "High Score: " + PlayerPrefs.GetInt("HighStreak").ToString();
    }
	
	// Update is called once per frame
	void Update () {
        //streak.text = "Score: " + PlayerPrefs.GetInt("Streak").ToString();
        //highStreak.text = "High Score: " + PlayerPrefs.GetInt("HighStreak").ToString();
    }

    public void GenerateGameBoard (float maxHeight, float maxWidth, int numOfPrefabs, GameObject obstacleObject)
    {
        GameObject prefabs;

        for (int i = 0; i < numOfPrefabs; i++)
        {
            prefabs = Instantiate(obstacleObject, new Vector3(Random.Range(-maxWidth, maxWidth), Random.Range(1.5f, maxHeight + 1f), 1f), Quaternion.identity);
        }

        //need a goal
        prefabs = Instantiate(obstacleObject, new Vector3(Random.Range(-maxWidth, maxWidth), Random.Range(1.5f, maxHeight + 1f), 1f), Quaternion.identity);
        prefabs.GetComponent<Obstacle_Base>().isGoal = true;
    }

    public void GameOver ()
    {
        Debug.Log("Game Over");

        gameOver = true;
        player.GetComponent<Rigidbody2D>().simulated = false;
        PlayerPrefs.SetInt("Streak", 0);
        StartCoroutine(TurnOnGameOverScreen(0.45f));
    }

    IEnumerator TurnOnGameOverScreen (float timeToWait)
    {
        yield return new WaitForSeconds(timeToWait);
        gameOverPanel.SetActive(true);
    }

    public void NextLevel ()
    {
        Debug.Log("Success!");

        streak.text = "Score: " + PlayerPrefs.GetInt("Streak").ToString();
        highStreak.text = "High Score: " + PlayerPrefs.GetInt("HighStreak").ToString();

        int streakCount = PlayerPrefs.GetInt("Streak");
        PlayerPrefs.SetInt("Streak", streakCount += 1);
        if (PlayerPrefs.GetInt("Streak") > PlayerPrefs.GetInt("HighStreak"))
            PlayerPrefs.SetInt("HighStreak", PlayerPrefs.GetInt("Streak"));

        SceneManager.LoadScene("Game_New");
    }

    public void Retry ()
    {
        SceneManager.LoadScene("Game_New");
    }

    public void MainMenu ()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
