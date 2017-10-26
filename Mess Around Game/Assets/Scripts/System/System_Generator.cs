using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class System_Generator : MonoBehaviour {

    public float rate;
    int wallCounter = 0;
    public GameObject[] targets;

    bool spawning = false;

    System_GameManager sgm;

    // Use this for initialization
    void Start () {
        sgm = FindObjectOfType<System_GameManager>();
	}
	
	// Update is called once per frame
	void Update () {
        if (!spawning) // && !System_GameManager.gameOver)
        {
            spawning = true;
            StartCoroutine(Spawn());
        }
	}

    IEnumerator Spawn()
    {
        GameObject obstacle;

        switch (sgm.currentMode)
        {
            case System_GameManager.Modes.Dodge:
                obstacle = Instantiate(targets[0], new Vector2(Random.Range(-5f, 5f), 5f), Quaternion.identity);
                break;
            case System_GameManager.Modes.Maze:
                wallCounter++;
                if (wallCounter > Random.Range(3, 5))
                {
                    obstacle = Instantiate(targets[1], new Vector2(Random.Range(-5f, 5f), 5f), Quaternion.identity);
                    wallCounter = 0;
                }
                break;
            case System_GameManager.Modes.Challenge:
                wallCounter++;
                if (wallCounter > Random.Range(3, 5))
                {
                    obstacle = Instantiate(targets[1], new Vector2(Random.Range(-5f, 5f), 5f), Quaternion.identity);
                    wallCounter = 0;
                }
                else
                    obstacle = Instantiate(targets[0], new Vector2(Random.Range(-5f, 5f), 5f), Quaternion.identity);
                break;
            default:
                break;
        }

        //obstacle.transform.parent = this.transform;
        yield return new WaitForSeconds(rate);
        spawning = false;
    }
}
