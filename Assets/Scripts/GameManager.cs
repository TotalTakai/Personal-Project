using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI floorText;
    [SerializeField] PlayerController player;
    [SerializeField] GameObject gameOverUI;

    private CameraScript cameraScript;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerController>();
        cameraScript = GameObject.Find("Main Camera").GetComponent<CameraScript>();
    }

    void FixedUpdate()
    {
        UpdateFloor();
    }

    private void Update()
    {
        if(cameraScript.isGameOver == true)
        {
            gameOverUI.SetActive(true);
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void UpdateFloor()
    {
        int floor = Mathf.RoundToInt( player.HighestY / 3.5f);
        floorText.SetText("Floor: " + floor);
    }
}
