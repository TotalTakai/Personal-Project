using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    private float highestPlayerHight=3;

    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (player.transform.position.y > highestPlayerHight)
        {
            highestPlayerHight = player.transform.position.y;
        }
        transform.position = new Vector3(0, highestPlayerHight, -10);
    }
}
