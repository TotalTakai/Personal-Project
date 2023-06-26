using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private readonly float speed = 10;
    private readonly float fullSpeedJumpForce = 1200.0f;
    private readonly float jumpForce = 700.0f;
    private readonly float xBoundary = 10.8f;
    private readonly float playerTopSpeed = 15;
    private readonly float boundaryKnockbackMultiplayer = 100;

    private bool isGrounded = true;
    private float horizontalInput;
    private Rigidbody playerRB;

    [SerializeField] Animator animator;
    [SerializeField] float currentSpeed;


    // Start is called before the first frame update
    void Start()
    {
        playerRB = transform.GetComponent<Rigidbody>();
        animator = transform.GetChild(0).GetComponent<Animator>();
    }


    private void FixedUpdate()
    {
        
        currentSpeed = CheckCurrentSpeed();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Jump();
        //AnimationUpdate();
        CheckBoundary();
    }

    // Checks if the player is grounded or not, and controls it's movement speed 
    private void Move()
    {
        horizontalInput = Input.GetAxis("Horizontal");

        // Controls running animations
        /* animator.SetFloat("Speed_f", Mathf.abs(horizontalInput)); 
         if (HorizontalInput == 0) animator.SetBool("Static_b", false)
        else animator.SetBool("Static_b", true)*/

        if (playerRB.velocity.magnitude > playerTopSpeed) // Limits player grounded top speed
        {
            playerRB.velocity = playerRB.velocity.normalized * (playerTopSpeed);
        }
        else playerRB.AddForce(Vector3.right * horizontalInput * speed, ForceMode.Impulse); // Accelerates 
    }


    // Checks the current player speed
    public float CheckCurrentSpeed()
    {
        float currentSpeedX = Mathf.RoundToInt(playerRB.velocity.magnitude);
        return currentSpeedX;
    }

    // Controls when to jump, and jump hight
    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) & isGrounded)
        {
            animator.SetTrigger("Jump_trig");
            if (currentSpeed < 10) playerRB.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
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

    // Checks if player left the platform and sets grounded as false
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            isGrounded = false;
        }
    }

    // Checks if the player hits the boundary, and if so pushes him back to the other side
    
    private void CheckBoundary()
    {
        if (transform.position.x >= xBoundary)
        {
            //transform.position = new Vector3(xBoundary, transform.position.y, transform.position.z);
            playerRB.AddForce(Vector3.left * boundaryKnockbackMultiplayer, ForceMode.Impulse);
        }
        if (transform.position.x <= -xBoundary)
        {
            //transform.position = new Vector3(-xBoundary, transform.position.y, transform.position.z);
            playerRB.AddForce(Vector3.right * boundaryKnockbackMultiplayer, ForceMode.Impulse);
        }
    }
}
