using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundComponent : MonoBehaviour
{
    public bool isGrounded; // used to detect if spring shoes power-up has touched the platform
    void Start()
    {
        isGrounded = false; // intialize this variable to false as player hasn't gotten the power-up yet
    }

    private void OnCollisionEnter2D(Collision2D collision) 
    {
        if (collision.gameObject.tag == "SpringShoes") // if collision with spring shoe power-up
        {
            isGrounded = true;
            //Debug.Log("Collision");
        }

        if (collision.gameObject.tag == "Character") // if collision with spring shoe power-up
        {
            isGrounded = true;
            //Debug.Log("Collision");
            //Debug.Log(isGrounded);
        }
    } 

    private void OnCollisionExit2D(Collision2D collision) 
    {
        if (collision.gameObject.tag == "SpringShoes") // if collision with spring shoe power-up
        {
            //isGrounded = false;
        }
    } 

    // Update is called once per frame
    void Update()
    {
        
    }
}
