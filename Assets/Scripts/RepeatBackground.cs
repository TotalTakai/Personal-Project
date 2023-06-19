using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatBackground : MonoBehaviour
{
    private Vector3 currentPos;
    private float repeatLength;

    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        currentPos = transform.position;
        repeatLength = GetComponent<BoxCollider>().size.z / 2;
    }

    // Update is called once per frame
    void Update()
    {
        MovePosition();
    }

    // Moves the background position once the player passes background halfway point
    void MovePosition()
    {
        if (player.transform.position.y > currentPos.y - repeatLength)
        {
            transform.position = new Vector3 (currentPos.x, player.transform.position.y, currentPos.z);
            currentPos = transform.position;
        }
    }
}
