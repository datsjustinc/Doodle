using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public Rigidbody2D rb; // create field variable for object's rigid body component
    public float moveSpeed, jumpPower = 3.0f; // create and intialize object's move and jump values
    bool canJump = true; // create control variable for object's jump
    bool spring = false; // create control variable for power-up
    bool trampoline = false; // create control variable for power-up
    bool springShoes = false; // create control variable for power-up
    bool isGround = true; // create control variable for ground
    bool regularPlatform = false; // create control variable for platform
    bool horizontalPlatform = false; // create control variable for platform
    public int springShoesCount = 0; // create variable to keep track of shoes lifespan
    float moveX = 1.0f; // create value to store object's move direction
    public Joint2D joint; // create variable to store joint component used to attach character to spring shoe power-up
    public GroundComponent platform; // create variable to store script GroundComponent in Platform object



    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>(); // get rigid body component of object
        canJump = true; // set character to bounce at start of game
        rb.freezeRotation = true; // set character to not rotate
        platform = GameObject.Find("Platform").GetComponent<GroundComponent>(); // gets platform object
    }

    void PlayerControls()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGround == true) // if key pressed
        {
            jumpPower = 8.0f; // boost jump power from power-up
            canJump = true; // set jump to true
        }

        if (spring) // if key pressed
        {
            jumpPower = 12.0f; // boost jump power from power-up
            canJump = true; // set jump to true
            spring = false; // set spring bounce to false so it can be triggered again
        }

        if (trampoline) // if key pressed
        {
            rb.freezeRotation = false; // allow character to rotate
            jumpPower = 12.0f; // boost jump power from power-up
            canJump = true; // set jump to true
            trampoline = false; // set spring bounce to false so it can be triggered again
        }

        if (springShoes) // this could be the springShoesCount, and start from range(6, 0), then reset to 6
        {
            if (platform.isGrounded == true) // if spring shoes object has touched the platform
            {
                jumpPower = 12.0f; // boost jump power from power-up
                Jump(); // skip directly to character jump
                springShoesCount += 1;  // keep track of spring shoes jump amount
                platform.isGrounded = false; // reset jump once character jumps

                if (springShoesCount >= 6) // if spring shoes object has reached its max use/lifespan
                {
                    springShoes = false; // stop the power-up
                    springShoesCount = 0; // reset power-up jump amount

                    if (platform.isGrounded == false)
                    {
                        Destroy(joint); // breaks spring shoes from character
                    }
                }
            }
        }

        if (regularPlatform) // if key pressed
        {
            jumpPower = 8.0f; // boost jump power from power-up
            canJump = true; // set jump to true
            regularPlatform = false; // reset bounce
        }

        if (horizontalPlatform) // if key pressed
        {
            jumpPower = 8.0f; // boost jump power from power-up
            canJump = true; // set jump to true
            horizontalPlatform = false; // reset bounce
        }

        moveX = Input.GetAxis("Horizontal"); // get movement input direction (-1, 1)
        //MOVEX = Input.GetAxis("Horizontal");
    }
    
    
    /* //can be used to disguise a variable as another type and condition without literally having to change it, keeps style and pattern of codes consistent
    bool SpringShoes {
        get => springShoesCount > 0;
    }
    */

    void Jump()
    {
        rb.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse); // make object jump
        jumpPower = 8.0f; // reset jump power to default value
    }

    private void OnCollisionEnter2D(Collision2D collision) 
    {
        spring = false; // just in case, all power-up collision variables are set to false before comparing collision tag
        trampoline = false; // just in case, all power-up collision variables are set to false before comparing collision tag

        if (collision.gameObject.tag == "Spring") // if collision with spring power-up
        {   
            rb.freezeRotation = true; // reset character rotation back to freeze
            rb.rotation = 0.0f; 
            spring = true; // set spring bounce to true
            //Destroy(collision.gameObject); 
        }

        if (collision.gameObject.tag == "Trampoline") // if collision with trampoline power-up
        {
            trampoline = true; // set spring bounce to true
            //Destroy(collision.gameObject); 
        }

        if (collision.gameObject.tag == "Platform") // if collision with platform power-up
        {
            rb.freezeRotation = true; // reset character rotation back to freeze
            rb.rotation = 0.0f;
        }

        if (collision.gameObject.tag == "SpringShoes") // if collision with spring shoe power-up
        {
            rb.freezeRotation = true; // reset character rotation back to freeze
            rb.rotation = 0.0f; 
            
            transform.position = new Vector3(collision.transform.position.x, collision.transform.position.y + 0.864327f, collision.transform.position.z);
            
            joint = gameObject.AddComponent<FixedJoint2D>(); // create new fixed joint component on character
            joint.connectedBody = collision.rigidbody; // attach joint to collision object, which is spring shoes

            springShoes = true; // set spring shoes on character to true

            if (springShoesCount == 5)
            {
                Destroy(collision.gameObject); // doesn't work rn, won't destroy
            }
            
            //Jump(); // directly call jump 6 times for powwer-up lifespan
            //for (int i = 0; i < 6; i++)
        
        }

        if (collision.gameObject.tag == "RegularPlatform") // if collision with platform power-up
        {
            rb.freezeRotation = true; // reset character rotation back to freeze
            rb.rotation = 0.0f;

            regularPlatform = true; // set to bounce
        }

        if (collision.gameObject.tag == "HorizontalPlatform") // if collision with platform power-up
        {
            rb.freezeRotation = true; // reset character rotation back to freeze
            rb.rotation = 0.0f;

            horizontalPlatform = true; // set to bounce
        }

    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(moveX * moveSpeed, rb.velocity.y); // update object movement speed/direction

        if (canJump)
        {
            canJump = false; // set this boolean to false so it can be triggered again 
            Jump(); // call jump function to execute
        } 
    }

    // Update is called once per frame
    void Update()
    {
        PlayerControls(); // call player control function to run and detect movement always
    }
}

