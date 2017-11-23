using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnDrawGizmosSelected()
    {
        GameObject GM = GameObject.FindGameObjectWithTag("GameManager");

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position, new Vector3(GM.GetComponent<LevelManager>().tileSize, GM.GetComponent<LevelManager>().tileSize, 1));
    }
}
