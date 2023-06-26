using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{

    private readonly float xBoundary = 5.5f;
    private readonly float zSpawnLocation = -1;
    private readonly float spawnDistance = 3f;
    private readonly float minimumSpawnDistance = 15;
    private float currentYSpawnLocation = 0;
    
    

    public GameObject platformPrefab;
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (currentYSpawnLocation - player.transform.position.y < minimumSpawnDistance) 
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
        currentYSpawnLocation += spawnDistance;
        return new Vector3(randomX, currentYSpawnLocation, zSpawnLocation);
    }
}
