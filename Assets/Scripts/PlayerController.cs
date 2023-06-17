using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private readonly float speed = 320f;
    private readonly float fullSpeedJumpForce = 1200.0f;
    private readonly float jumpForce = 700.0f;
    private readonly float xBoundary = 10;
    private readonly float boundaryKnockbackMultiplayer = 100;

    private bool isGrounded = true;
    private bool moveRightDirection = true;
    private float horizontalInput;
    private float oldPositionX;
    private Rigidbody playerRB;

    public float currentSpeed;
    

    // Start is called before the first frame update
    void Start()
    {
        playerRB = GetComponent<Rigidbody>();
    }

    // Fixed Update is called once per frame before update
    private void FixedUpdate()
    {
        currentSpeed = CheckCurrentSpeed();
    }

    // Update is called once per frame
    void Update()
    {
        CheckBoundary();
        Move();
        Jump();
    }

    // Controls the movement of player
    private void Move()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        playerRB.AddForce(Vector3.right * horizontalInput * speed);
        DirectionChange();
    }


    // Checks the current player speed
    public float CheckCurrentSpeed()
    {
        float currentSpeedX = Mathf.Abs(Mathf.Abs(transform.position.x) - Mathf.Abs(oldPositionX)) * 100f;
        oldPositionX = transform.position.x;


        return currentSpeedX;
    }
    
    // Controls when to jump, and jump hight
    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) & isGrounded)
        {
            isGrounded = false;
            if(currentSpeed < 15) playerRB.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            else playerRB.AddForce(Vector3.up * fullSpeedJumpForce, ForceMode.Impulse);
        }
    }

    // Checks when the player collides with a platform and enables jumping again
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            isGrounded = true;
        }
    }

    // Resets the velocity and the rotation of player
    private void ResetVelocityAndRotation()
    {
        playerRB.velocity = Vector3.zero;
        playerRB.angularVelocity = Vector3.zero;
    }

    // Checks if the player changes direction, and if so kills the velocity and rotation
    private void DirectionChange()
    {
        if (moveRightDirection && Input.GetKeyDown(KeyCode.LeftArrow))
        {
            moveRightDirection = false;
            if (isGrounded) ResetVelocityAndRotation();
        }
        else if (!moveRightDirection && Input.GetKeyDown(KeyCode.RightArrow))
        {
            moveRightDirection = true;
            if (isGrounded) ResetVelocityAndRotation();
        }
    }

    // Checks if the player hits the boundary, and if so pushes him back to the other side
    private void CheckBoundary() 
    {
        if(transform.position.x >= xBoundary)
        {
            transform.position = new Vector3(xBoundary, transform.position.y, transform.position.z);
            playerRB.AddForce(Vector3.left * boundaryKnockbackMultiplayer, ForceMode.Impulse);
            moveRightDirection = false;
        }
        if (transform.position.x <= -xBoundary)
        {
            transform.position = new Vector3(-xBoundary, transform.position.y, transform.position.z);
            playerRB.AddForce(Vector3.right * boundaryKnockbackMultiplayer, ForceMode.Impulse);
            moveRightDirection = true;
        }

    }
}
