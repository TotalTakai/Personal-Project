using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    private float timer;
    private float highestPlayerHight = 3;
    private float raiseCameraPosition;

    [SerializeField] GameObject player;
    public bool isGameOver;
    public bool isTimerStarted;

    // Start is called before the first frame update
    void Start()
    {
        isTimerStarted = false;
        isGameOver = false;
        raiseCameraPosition = 0.005f;
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (player.transform.position.y > 6)
        {
            isTimerStarted = true;
            timer += Time.deltaTime;
        }
        if (player.transform.position.y > highestPlayerHight)
        {
            highestPlayerHight = player.transform.position.y;
        }
        if (transform.position.y < highestPlayerHight || !isTimerStarted)
        {
            transform.position = new Vector3(0, highestPlayerHight, -10);
        }
        else if (!isGameOver) transform.position = transform.position + new Vector3(0, raiseCameraPosition, 0);
        IfSixtySecondsPass();
        CheckIfGameOver();
    }
    public void CheckIfGameOver()
    {
        if (transform.position.y - 8 > player.transform.position.y)
        {
            isGameOver = true;
        }
    }
    private void IfSixtySecondsPass()
    {
        if(timer >= 60)
        {
            raiseCameraPosition += 0.005f;
            timer = 0;
        }
    }
}

