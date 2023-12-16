using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnPlayerRandom : MonoBehaviour
{
    public GameObject playerPrefab; // Use a prefab for the player
    private GameObject currentPlayer; // Reference to the current player instance
    public GameObject[] gameObjects;

    void Start()
    {
        gameObjects = GameObject.FindGameObjectsWithTag("SpawnPoint");
        SpawnPlayerAtRandomLocation();
    }

    private void Update()
    {
        if (currentPlayer.transform.position.y < -25)
        {
            Destroy(currentPlayer); // Destroy the current player
            SpawnPlayerAtRandomLocation(); // Spawn a new player
        }
    }

    void SpawnPlayerAtRandomLocation()
    {
        int numberOfSpawnPoints = gameObjects.Length;
        int spawnPointIndex = Random.Range(0, numberOfSpawnPoints);

        // Get the position of the selected spawn point
        Vector3 spawnPosition = gameObjects[spawnPointIndex].transform.position;

        // Adjust the y-coordinate of the spawn position
        spawnPosition = new Vector3(spawnPosition.x, spawnPosition.y + 1, spawnPosition.z);

        // Instantiate the Player at the spawn position and update the reference
        currentPlayer = Instantiate(playerPrefab, spawnPosition, Quaternion.identity);
    }

}
