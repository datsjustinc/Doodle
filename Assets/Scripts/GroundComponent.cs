using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundComponent : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision) 
    {
        if (collision.gameObject.tag == "Character") // if collision with spring shoe power-up
        {
            Debug.Log("Colliding with " + collision.gameObject.tag);
            collision.gameObject.GetComponent<CharacterMovement>().isGrounded = true;

        }
    } 

    private void OnCollisionExit2D(Collision2D collision) 
    {

    } 

    // Update is called once per frame
    void Update()
    {
        
    }
}
