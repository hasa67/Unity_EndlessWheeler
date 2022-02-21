using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour {

    public GameObject[] tilePrefabs;
    public float tileLength = 20f;
    public int numberOfTiles = 5;
    public Transform playerTransform;

    private float zSpawn = 0f;
    public List<GameObject> activeTiles = new List<GameObject>();


    void Start() {
        for (int i = 0; i < numberOfTiles; i++) {
            if (i == 0) {
                SpawnTile(0);
            } else {
                SpawnTile(Random.Range(0, tilePrefabs.Length));
            }
        }

    }

    void Update() {
        if (playerTransform.position.z - 2 * tileLength > zSpawn - numberOfTiles * tileLength) {
            SpawnTile(Random.Range(0, tilePrefabs.Length));
            DeleteTiles();
        }

    }

    private void SpawnTile(int tileIndex) {
        GameObject go = Instantiate(tilePrefabs[tileIndex], transform.forward * zSpawn, transform.rotation);
        zSpawn += tileLength;
        activeTiles.Add(go);
    }

    private void DeleteTiles() {
        Destroy(activeTiles[0]);
        activeTiles.RemoveAt(0);
    }
}
