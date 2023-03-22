using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class System_Generator : MonoBehaviour {

    public float rate;
    int wallCounter = 0;
    public GameObject[] targets;

    bool spawning = false;
    
    // Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        if (!spawning && System_GameManager.instance.currentState == System_GameManager.States.Playing)//!System_GameManager.gameOver)
        {
            spawning = true;
            StartCoroutine(Spawn());
        }
	}

    IEnumerator Spawn()
    {
        wallCounter++;
        GameObject obstacle;

        //if (wallCounter > Random.Range(3, 5))
        //{
        //    obstacle = Instantiate(targets[1], new Vector2(Random.Range(-5f, 5f), 5f), Quaternion.identity);
        //    wallCounter = 0;
        //}
        //else
            obstacle = Instantiate(targets[0], new Vector2(Random.Range(-5f, 5f), 5f), Quaternion.identity);
        //GameObject obstacle = Instantiate(targets[Random.Range(0,targets.Length)], new Vector2(Random.Range(-5f, 5f), 5f), Quaternion.identity);
        //obstacle.transform.parent = this.transform;
        yield return new WaitForSeconds(rate);
        spawning = false;
    }
}
