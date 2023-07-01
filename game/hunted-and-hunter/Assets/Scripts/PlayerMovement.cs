using System.Diagnostics;
using System.Net.NetworkInformation;
using System.Numerics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody myRb;
    [SerializeField] public float moveSpeed = 6f;
    [SerializeField] public float jumpForce = 5f;
    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask ground;
    [SerializeField] AudioSource jumpSound;
    /**/ [SerializeField] float accelerationAmount = 3f;
    [SerializeField] float accelerationDuration = 1f;
    private bool isAccelerating = false; 


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Moving forward, back, left, right
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        myRb.velocity = new UnityEngine.Vector3(horizontalInput * moveSpeed, myRb.velocity.y, verticalInput * moveSpeed);

        // Jumping
        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            Jump();
        }
    }

    void Jump()
    {
        myRb.velocity = new UnityEngine.Vector3(myRb.velocity.x, jumpForce, myRb.velocity.z);
        jumpSound.Play();
    }    

    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.CompareTag("Enemy Head"))
        {
            Destroy(other.transform.parent.gameObject);
            Jump();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Coin"))
        {
            if (!isAccelerating)
            {
                StartCoroutine(AcceleratePlayer());
            }
            /* myRb.AddForce(transform.forward * 1000, ForceMode.Force); */
        }
    }

    // Check if Player is on the ground
    bool IsGrounded()
    {
        return Physics.CheckSphere(groundCheck.position, .1f, ground);
    }

   private IEnumerator AcceleratePlayer()
    {
        isAccelerating = true;                                          // Set the flag to indicate player acceleration

        float timer = 0f;                                               // Timer to track elapsed time
        float initialSpeed = myRb.velocity.magnitude;                   // Store initial speed
        float targetSpeed = initialSpeed + accelerationAmount;          // Calculate desired target speed

        while (timer < accelerationDuration)                            // Loop until acceleration duration is reached
        {
            // Interpolate between initial speed and target speed
            float currentSpeed = Mathf.Lerp(initialSpeed, targetSpeed, timer / accelerationDuration);
            // myRb.velocity = myRb.velocity.normalized * currentSpeed;    // Update player's velocity
            
            UnityEngine.Vector3 forwardForce = transform.forward * currentSpeed;
            forwardForce.y = 0f;
            myRb.AddForce(forwardForce, ForceMode.VelocityChange); 

            timer += Time.deltaTime;                                    // Increment timer
            yield return null;                                          // Yield briefly to the game loop
        }

        isAccelerating = false;                                         // Reset the flag to indicate acceleration is complete
    }  /*  */
}
