using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveWalls : MonoBehaviour
{
    private float cameraYPosition;

    [SerializeField] new Transform camera;

    // Update is called once per frame
    void Update()
    {
        cameraYPosition = camera.position.y;
        transform.position = new Vector3(transform.position.x, cameraYPosition, transform.position.z);
    }
}
