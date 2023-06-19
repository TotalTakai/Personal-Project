using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{

    private readonly float xBoundary = 5.5f;
    private readonly float spawnDistanceFromPlayer = 14;
    private readonly float zSpawnLocation = -1;
    private float playerY = -4;
    private float startYSpawnLocation = 0;
    

    public GameObject platformPrefab;
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (player.transform.position.y - playerY > 3.5) // Makes sure there's a 3.5 meter difference between platforms
        {
            spawnPlatform();
        }
    }

    private void spawnPlatform()
    {
        Instantiate(platformPrefab, GenerateSpawnLocation(), platformPrefab.transform.rotation);
    }

    // Generates a random spawn location 14 meters above the player
    private Vector3 GenerateSpawnLocation()
    {
        float randomX = Random.Range(-xBoundary, xBoundary);
        if (startYSpawnLocation < 10.5)
        {
            startYSpawnLocation += 3.5f;
            return new Vector3(randomX, startYSpawnLocation, zSpawnLocation);
        }
        else
        {
            playerY = player.transform.position.y;
            return new Vector3(randomX, playerY + spawnDistanceFromPlayer, zSpawnLocation);
        }
    }
}
