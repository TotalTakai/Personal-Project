using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private readonly float speed = 10;
    private readonly float fullSpeedJumpForce = 1500.0f;
    private readonly float jumpForce = 750.0f;
    private readonly float xBoundary = 10.8f;
    private readonly float playerTopSpeed = 15;
    private readonly float playerTopAirSpeed = 25;
    private readonly float boundaryKnockbackMultiplayer = 100;

    [SerializeField] bool facingRight = true;
    private bool isGrounded = true;
    private float horizontalInput;
    private Rigidbody playerRB;
    private Animator animator;

    public float HighestY =0;


    // Start is called before the first frame update
    void Start()
    {
        playerRB = transform.GetComponent<Rigidbody>();
        animator = transform.GetChild(0).GetComponent<Animator>();
    }


    private void FixedUpdate()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Jump();
        CheckBoundary();
    }

    // Checks if the player is grounded or not, and controls it's movement speed 
    private void Move()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        if ((horizontalInput > 0 && !facingRight) || (horizontalInput < 0 && facingRight)) ChangeDirection(); // changes animation direction according to player input

        // Controls running animations
        /* animator.SetFloat("Speed_f", Mathf.abs(horizontalInput)); 
         if (HorizontalInput == 0) animator.SetBool("Static_b", false)
        else animator.SetBool("Static_b", true)*/

        if (playerRB.velocity.magnitude > playerTopSpeed && isGrounded) // Limits player grounded top speed
        {
            playerRB.velocity = playerRB.velocity.normalized * (playerTopSpeed);
        }
        else if (playerRB.velocity.magnitude > playerTopAirSpeed && !isGrounded)
        {
            playerRB.velocity = playerRB.velocity.normalized * (playerTopAirSpeed);
        }
        else playerRB.AddForce(Vector3.right * horizontalInput * speed, ForceMode.Impulse); // Accelerates 
    }

    private void ChangeDirection()
    {
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);
    }

    // Checks the current player speed

    // Controls when to jump, and jump hight
    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) & isGrounded)
        {
            animator.SetTrigger("Jump_trig");
            if (Mathf.RoundToInt(playerRB.velocity.x) < 10) playerRB.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            else playerRB.AddForce(Vector3.up * fullSpeedJumpForce, ForceMode.Impulse);
        }
    }

    // Checks when the player collides with a platform and enables jumping again
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            isGrounded = true;
            if (transform.position.y > HighestY) HighestY = transform.position.y;
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
            playerRB.AddForce(Vector3.left * boundaryKnockbackMultiplayer, ForceMode.Impulse);
            playerRB.AddForce(Vector3.up * 40, ForceMode.Impulse);
        }
        else if (transform.position.x <= -xBoundary)
        {
            playerRB.AddForce(Vector3.right * boundaryKnockbackMultiplayer, ForceMode.Impulse);
            playerRB.AddForce(Vector3.up * 40, ForceMode.Impulse);

        }
    }
}
