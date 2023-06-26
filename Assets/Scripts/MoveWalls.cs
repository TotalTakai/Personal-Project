using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveWalls : MonoBehaviour
{
    private float currentY;
    private float newY;

    [SerializeField] Transform player;

    // Start is called before the first frame update
    void Start()
    {
        currentY = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        newY = player.position.y - currentY;
        transform.position = transform.position + new Vector3(0, newY, 0);
        currentY = transform.position.y;
    }
}
