using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public Rigidbody2D rb; // create field variable for object's rigid body component
    public float moveSpeed, jumpPower = 16.0f; // create and intialize object's move and jump values
    public bool canJump = true; // create control variable for object's jump
    bool spring = false; // create control variable for power-up
    bool trampoline = false; // create control variable for power-up
    bool springShoes = false; // create control variable for power-up
    bool propellorHat = false; // create control variable for power-up
    public GameObject springShoes_obj; // create variable to store spring object
    public GameObject propellorHat_obj; // create variable to store spring object
    public GameObject brokenPlatform_obj; // create variable to store broken platform object
    bool isGround = true; // create control variable for ground
    public bool regularPlatform = false; // create control variable for platform
    public bool horizontalPlatform = false; // create control variable for platform
    public bool brokenPlatform = false; // create control variable for platform
    public int springShoesCount = 0; // create variable to keep track of shoes lifespan
    float moveX = 1.0f; // create value to store object's move direction
    //public Joint2D joint; // create variable to store joint component used to attach character to spring shoe power-up
    public GroundComponent platform; // create variable to store script GroundComponent in Platform object
    public bool gameEnd = false; // controls whether game ends or not

    public AudioSource jump;
    public AudioSource springs;
    public AudioSource trampolines;
    public AudioSource propellor;
    public AudioSource death;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>(); // get rigid body component of object
        rb.freezeRotation = true; // set character to not rotate
        // platform = GameObject.Find("Platform").GetComponent<GroundComponent>(); // gets platform object
        canJump = true; // initial jump of player when game starts
    }

    IEnumerator PlayerControls()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGround == true) // if key pressed
        {
            //jumpPower = 14.0f; // boost jump power from power-up
            //canJump = true; // set jump to true
        }

        if (spring) // if key pressed
        {
            springs.Play(); // play sound
            jumpPower = 22.0f; // boost jump power from power-up
            canJump = true; // set jump to true
            spring = false; // set spring bounce to false so it can be triggered again
        }

        if (trampoline) // if key pressed
        {
            trampolines.Play(); // play sound
            jumpPower = 22.0f; // boost jump power from power-up
            canJump = true; // set jump to true
            trampoline = false; // set spring bounce to false so it can be triggered again
        }

        if (springShoes) // this could be the springShoesCount, and start from range(6, 0), then reset to 6
        {
            Debug.Log("SpringShoes");
            Destroy(springShoes_obj); // destroy spring object
            if (platform.isGrounded == true) // if spring shoes object has touched the platform
            {
                //Debug.Log(platform.isGrounded);
                jumpPower = 22.0f; // boost jump power from power-up
                Jump(); // skip directly to character jump
                springShoesCount += 1;  // keep track of spring shoes jump amount
                platform.isGrounded = false;
                yield return new WaitForSeconds(1f); // difficulty increases every 1 seconds
                Destroy(springShoes_obj); // destroy spring object

                if (springShoesCount >= 6) // if spring shoes object has reached its max use/lifespan
                {
                    springShoes = false; // stop the power-up
                    springShoesCount = 0; // reset power-up jump amount

                    //Destroy(joint); // breaks spring shoes from character
                    Destroy(springShoes_obj); // destroy spring object

                }
            }
        }


        if (propellorHat) // if key pressed
        {
            propellor.Play(); // play sound
            jumpPower = 50.0f; // boost jump power from power-up
            canJump = true; // set jump to true
            propellorHat = false; // reset bounce
            yield return new WaitForSeconds(2.5f); // difficulty increases every 2.5 seconds
            propellor.Stop(); // play sound
            Destroy(propellorHat_obj); // destroy broken platform object
        }
        

        if (regularPlatform) // if key pressed
        {
            jumpPower = 16.0f; // boost jump power from power-up
            canJump = true; // set jump to true
            regularPlatform = false; // reset bounce
        }

        if (horizontalPlatform) // if key pressed
        {
            jumpPower = 16.0f; // boost jump power from power-up
            canJump = true; // set jump to true
            horizontalPlatform = false; // reset bounce
        }

        if (brokenPlatform) // if key pressed
        {
            jumpPower = 0f; // boost jump power from power-up
            canJump = true; // set jump to true
            brokenPlatform = false; // reset bounce
            Destroy(brokenPlatform_obj); // destroy broken platform object
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
        jump.Play(); // play sound
        jumpPower = 16.0f; // reset jump power to default value
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        OnCollisionEnter2D(collision, "PropellorHat");
    }

    private void OnCollisionEnter2D(Collision2D collision, string v) 
    {
        spring = false; // just in case, all power-up collision variables are set to false before comparing collision tag
        trampoline = false; // just in case, all power-up collision variables are set to false before comparing collision tag
        springShoes = false; // just in case, all power-up collision variables are set to false before comparing collision tag
        propellorHat = false; // just in case, all power-up collision variables are set to false before comparing collision tag

        if (collision.gameObject.tag == "Spring" && rb.velocity.y < 0.1 && gameEnd != true) // if collision with spring power-up
        {   
            rb.freezeRotation = true; // reset character rotation back to freeze
            rb.rotation = 0.0f; 
            spring = true; // set spring bounce to true
            //Destroy(collision.gameObject); 
        }

        if (collision.gameObject.tag == "Trampoline" && rb.velocity.y < 0.1 && gameEnd != true) // if collision with trampoline power-up
        {
            //rb.freezeRotation = false; // allow character to rotate
            rb.freezeRotation = true; // reset character rotation back to freeze
            rb.rotation = 0.0f; 
            trampoline = true; // set spring bounce to true
            //Destroy(collision.gameObject); 
        }

        if (collision.gameObject.tag == "Platform" && rb.velocity.y < 0.1 && gameEnd != true) // if collision with platform power-up
        {
            rb.freezeRotation = true; // reset character rotation back to freeze
            rb.rotation = 0.0f;
        }

        if (collision.gameObject.tag == "SpringShoes" && rb.velocity.y < 0.1 && gameEnd != true) // if collision with spring shoe power-up
        {
            rb.freezeRotation = true; // reset character rotation back to freeze
            rb.rotation = 0.0f; 
            
            transform.position = new Vector3(collision.transform.position.x, collision.transform.position.y + 0.864327f, collision.transform.position.z);
            
            //joint = gameObject.AddComponent<FixedJoint2D>(); // create new fixed joint component on character
            //joint.connectedBody = collision.rigidbody; // attach joint to collision object, which is spring shoes

            springShoes = true; // set spring shoes on character to true
            springShoes_obj = collision.gameObject; // store collision object

            springShoes_obj.transform.SetParent(transform);
        }

        if (collision.gameObject.tag == "PropellorHat" && rb.velocity.y < 0.1 && gameEnd != true) // if collision with spring shoe power-up
        {
            rb.freezeRotation = true; // reset character rotation back to freeze
            rb.rotation = 0.0f; 
            
            //joint = gameObject.AddComponent<FixedJoint2D>(); // create new fixed joint component on character
            //joint.connectedBody = collision.rigidbody; // attach joint to collision object, which is spring shoes

            collision.transform.position = new Vector3(transform.position.x, transform.position.y + 0.58f, transform.position.z);

            propellorHat = true; // set propellor hat on character to true
            propellorHat_obj = collision.gameObject; // store collision object

            propellorHat_obj.transform.SetParent(transform);
        }

        if (collision.gameObject.tag == "RegularPlatform" && rb.velocity.y < 0.1 && gameEnd != true) // if collision with platform power-up
        {
            rb.freezeRotation = true; // reset character rotation back to freeze
            rb.rotation = 0.0f;

            regularPlatform = true; // set to bounce

            platform = collision.gameObject.GetComponent<GroundComponent>(); // gets platform object
        }

        if (collision.gameObject.tag == "HorizontalPlatform" && rb.velocity.y < 0.1 && gameEnd != true) // if collision with platform power-up
        {
            rb.freezeRotation = true; // reset character rotation back to freeze
            rb.rotation = 0.0f;

            horizontalPlatform = true; // set to bounce

            platform = collision.gameObject.GetComponent<GroundComponent>(); // gets platform object
        }

        if (collision.gameObject.tag == "BrokenPlatform" && rb.velocity.y < 0.1 && gameEnd != true) // if collision with platform power-up
        {
            rb.freezeRotation = true; // reset character rotation back to freeze
            rb.rotation = 0.0f;

            brokenPlatform_obj = collision.gameObject; // store collision object

            platform.isGrounded = false;

            brokenPlatform = true; // set to bounce
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("RegularMonster")) // if collision with platform power-up
        {
            death.Play(); // play sound
            gameEnd = true;
            transform.GetComponent<BoxCollider2D>().isTrigger = true; 
        }

        if (collision.CompareTag("MovingMonster")) // if collision with platform power-up
        {
            death.Play(); // play sound
            gameEnd = true;
            transform.GetComponent<BoxCollider2D>().isTrigger = true; 
        }
    }
        

    private void FixedUpdate()
    {
        if (transform.position.x < -5f)
        {
            transform.position = new Vector3(5f, transform.position.y, transform.position.z);
        }
        
        else if (transform.position.x > 5f)
        {
            transform.position = new Vector3(-5f, transform.position.y, transform.position.z);
        }

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
        StartCoroutine(PlayerControls()); // call player control function to run and detect movement always
        //Debug.Log(rb.velocity.y);
        
        // if (rb.velocity.y < -15.0f)
        // {
        //     gameEnd = true;
        // }

        if (transform.position.y < Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).y - 1f)
        {
            death.Play(); // play sound
            gameEnd = true;
        }

        if (moveX == -1)
        {
            transform.GetComponent<SpriteRenderer>().flipX = true;
        }

        else if (moveX == 1)
        {
            transform.GetComponent<SpriteRenderer>().flipX = false;
        }
    }
}
