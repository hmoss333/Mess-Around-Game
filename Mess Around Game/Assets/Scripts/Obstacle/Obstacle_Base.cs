using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle_Base : MonoBehaviour {

    public bool isGoal = false;
    private bool isHit;

    System_GameManager gm; //not in the scene yet
    
    // Use this for initialization
	void Start () {
        gm = GameObject.FindObjectOfType<System_GameManager>();

        if (isGoal)
            GetComponent<SpriteRenderer>().color = Color.green;
	}
	
	// Update is called once per frame
	void Update () {

	}

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player" && !isHit && !gm.gameOver)
        {
            Debug.Log("Hit");

            if (isGoal) {
                gm.NextLevel();
            }
            else {
                isHit = true;
                gm.GameOver();
                StartCoroutine(ColorChange(col.GetComponent<SpriteRenderer>()));
            }
        }
    }

    IEnumerator ColorChange(SpriteRenderer target)
    {
        target.color = Color.red;
        yield return new WaitForSeconds(0.25f);
        target.color = Color.white;
    }
}
