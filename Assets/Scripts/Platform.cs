using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{

    private GameObject player;

    Collider platformCollider;

    // Start is called before the first frame update
    void Start()
    {
        platformCollider = gameObject.GetComponent<Collider>();
        player = GameObject.Find("Player");
    }


    private void FixedUpdate()
    {
        CheckIfPlayerAbove();
    }
    // Update is called once per frame
    void Update()
    {
        DestroyPlatforms();
    }

    private void CheckIfPlayerAbove()
    {
        if (player.transform.position.y > gameObject.transform.position.y)
        {
            platformCollider.enabled = true;
        }
        else platformCollider.enabled = false;
    }

    private void DestroyPlatforms()
    {
        if(player.transform.position.y - gameObject.transform.position.y > 5)
        {
            Destroy(gameObject);
        }
    }
}
