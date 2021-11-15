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
    int springShoesCount = 0; // create variable to keep track of shoes lifespan
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
        if (spring) // if key pressed
        {
            jumpPower = 8.0f; // boost jump power from power-up
            canJump = true; // set jump to true
            spring = false; // set spring bounce to false so it can be triggered again
        }

        if (trampoline) // if key pressed
        {
            rb.freezeRotation = false; // allow character to rotate
            jumpPower = 8.0f; // boost jump power from power-up
            canJump = true; // set jump to true
            trampoline = false; // set spring bounce to false so it can be triggered again
        }

        if (springShoes) // if key pressed
        {
            if (springShoesCount < 6)
            {
                if (platform.isGrounded == true)
                {
                    jumpPower = 10.0f; // boost jump power from power-up
                    Jump(); // skip directly to player jump
                    springShoes = false; // set spring shoes bounce to false so it can be triggered again
                    springShoesCount += 1; 
                }
            }
        }


        moveX = Input.GetAxis("Horizontal"); // get movement input direction (-1, 1)
        //MOVEX = Input.GetAxis("Horizontal");
    } 

    void Jump()
    {
        rb.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse); // make object jump
        jumpPower = 3.0f; // reset jump power to default value
    }

    private void OnCollisionEnter2D(Collision2D collision) 
    {
        spring = false; // just in case, all power-up collision variables are set to false before comparing collision tag
        trampoline = false; // just in case, all power-up collision variables are set to false before comparing collision tag

        if (collision.gameObject.tag == "Spring") // if collision with spring power-up
        {   
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
            springShoes = true; // set spring shoes on character to true
            
            joint = gameObject.AddComponent<FixedJoint2D>(); // create new fixed joint component on character
            joint.connectedBody = collision.rigidbody; // attach joint to collision object, which is spring shoes
            
            //Jump(); // directly call jump 6 times for powwer-up lifespan
            //for (int i = 0; i < 6; i++)
        
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

