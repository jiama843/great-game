using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceSpawn : MonoBehaviour
{
    public GameObject[] spawnLocs;
    public GameObject[] pieces;

    private int numberOfPieces;
    void Awake()
    {
        numberOfPieces = pieces.Length;
        SpawnPieces();
    }

    void SpawnPieces()
    {// Create a list to store available spawn positions
        List<GameObject> availableSpawnLocs = new List<GameObject>(spawnLocs);

        // Check if there are enough available spawn positions
        if (availableSpawnLocs.Count < numberOfPieces)
        {
            Debug.LogWarning("Not enough available spawn positions.");
            numberOfPieces = availableSpawnLocs.Count; // Adjust number of pieces to spawn
        }

        for (int i = 0; i < numberOfPieces; i++)
        {
            // Generate a random index to select a spawn location
            int randomIndex = Random.Range(0, availableSpawnLocs.Count);
            GameObject chosenSpawnLoc = availableSpawnLocs[randomIndex];

            // Remove the chosen spawn position from the available list
            availableSpawnLocs.RemoveAt(randomIndex);

            // Generate a random index to select a piece
            int randomPieceIndex = Random.Range(0, pieces.Length);

            // Get the selected spawn location
            Vector3 spawnPosition = chosenSpawnLoc.transform.localPosition;

            pieces[randomPieceIndex].transform.localPosition = spawnPosition;

        }
    }
}
