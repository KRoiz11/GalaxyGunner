using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5.0f; // Determines the speed of the player
    public float sprintSpeed = 10.0f; //Determines the speed of the player when sprinting
    public float jumpForce = 5.0f; //Determines the jump height of the player
    public bool isOnGround = true;
    private float horizontalInput;
    private float forwardInput;
    private Rigidbody playerRb;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>(); // allows access to the player's rigidbody component
    } 

    // Update is called once per frame
    void Update() 
    {
        //Get player input for horizontal and forward movement
        horizontalInput = Input.GetAxis("Horizontal");
        forwardInput = Input.GetAxis("Vertical");

        //get forward and right directions from cam orientation
        Vector3 forwardDirection = Camera.main.transform.forward;
        Vector3 rightDirection = Camera.main.transform.right;

        forwardDirection.y = 0f; 
        rightDirection.y = 0f;

        //Normalize to ensure consistent speed in all directions
        forwardDirection.Normalize();
        rightDirection.Normalize(); 

        // Calculate the movement direction based on camera orientation
        Vector3 moveDirection = forwardDirection * forwardInput + rightDirection * horizontalInput;
        moveDirection.Normalize();

        float currentSpeed = speed;

        if (Input.GetKey(KeyCode.LeftShift))
        {
            currentSpeed = sprintSpeed;
        }

        // Move the player
        transform.Translate(moveDirection * currentSpeed * Time.deltaTime);

        //Lets the player jump
        if(Input.GetKey(KeyCode.Space) && isOnGround)
        {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isOnGround = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
        }
    }
}
