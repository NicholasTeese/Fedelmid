using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelManager : MonoBehaviour {


    public float tileSize = 30.0f;
    public List<GameObject> tiles = new List<GameObject>();
    public GameObject player;

    List<GameObject> currentTiles = new List<GameObject>();

	// Use this for initialization
	void Start () {

        GenStart();
    }
	
	// Update is called once per frame
	void Update () {

        List<Vector3> tilesToAdd = new List<Vector3>();
        List<GameObject> tilesToDel = new List<GameObject>();

		foreach(GameObject tile in currentTiles)
        {
            if (tile != null)
            {
                if (player.transform.position.x - tile.transform.position.x > (tileSize * 2))
                {
                    tilesToAdd.Add(new Vector3(currentTiles.Last().transform.position.x + tileSize, 0));
                    tilesToDel.Add(tile);
                }
            }
        }

        foreach(Vector3 pos in tilesToAdd)
            GenNextTile(pos);

        foreach(GameObject tile in tilesToDel)
        {
            currentTiles.Remove(tile);
            Destroy(tile);
        }
	}

    void GenNextTile(Vector3 a_pos)
    {
        GameObject nextTile = Instantiate(tiles[Random.Range(1, tiles.Capacity)], a_pos, new Quaternion()) as GameObject;
        nextTile.SetActive(true);

        currentTiles.Add(nextTile);
    }

    void GenStart()
    {
        GenNextTile(new Vector3(player.transform.position.x - tileSize, 0));

        GameObject nextTile = Instantiate(tiles[0], new Vector3(player.transform.position.x, 0), new Quaternion()) as GameObject;
        nextTile.SetActive(true);

        currentTiles.Add(nextTile);

        GenNextTile(new Vector3(player.transform.position.x + tileSize, 0));
    }

}
