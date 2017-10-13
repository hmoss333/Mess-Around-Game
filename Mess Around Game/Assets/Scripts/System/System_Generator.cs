using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class System_Generator : MonoBehaviour {

    public float rate;
    public GameObject target;

    bool spawning = false;
    
    // Use this for initialization
	void Start () {

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
        GameObject obstacle = Instantiate(target, new Vector2(Random.Range(-10f, 10f), 5f), Quaternion.identity);
        obstacle.transform.parent = this.transform;
        yield return new WaitForSeconds(rate);
        spawning = false;
    }
}
