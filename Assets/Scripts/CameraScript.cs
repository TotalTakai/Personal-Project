using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    private float timer;
    private float highestPlayerHight=3;
    private float raiseCameraSpeed;
    private Vector3 riseCameraVector = new Vector3(0, 0.01f, 0);

    [SerializeField] GameObject player;
    public bool isGameOver;
    public bool isTimerStarted;

    // Start is called before the first frame update
    void Start()
    {
        isTimerStarted = false;
        isGameOver = false;
        raiseCameraSpeed = 0.05f;
        timer = 0;
    }

    private void FixedUpdate()
    {
        transform.position = transform.position + riseCameraVector;
    }
    // Update is called once per frame
    void Update()
    {
        if (isTimerStarted = false && player.transform.position.y > 6)
        {
            StartCoroutine(CountTime());
        }
        if (player.transform.position.y > highestPlayerHight)
        {
            highestPlayerHight = player.transform.position.y;
        }
        transform.position = new Vector3(0, highestPlayerHight, -10);
        checkIfGameOver();
    }
    public void checkIfGameOver()
    {
        if(transform.position.y - 8 > player.transform.position.y)
        {
            isGameOver = true;
        }
    }

    IEnumerator CountTime()
    {
        yield return new WaitForSeconds(1); // start Counting time after player reached the 2nd floor
        isTimerStarted = true;
        while (isGameOver == false)
        {
            if (timer == 60)
            {
                timer = 0;
                raiseCameraSpeed += 0.05f;
            }
            timer++;
        }
    }

}
