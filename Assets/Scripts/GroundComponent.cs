using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundComponent : MonoBehaviour
{
    void Start()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision) 
    {
        //Debug.Log($"touching {collision.gameObject.tag} {Time.time}");
        if (collision.gameObject.tag == "Character") // if collision with spring shoe power-up
        {
            // collision.gameObject.GetComponentInParent<CharacterMovement>().SpringShoesJump = true; // keeps going up until gets to parent with Character Momvement script
            collision.gameObject.GetComponent<CharacterMovement>().SpringShoesJump = true; // keeps going up until gets to parent with Character Momvement script
        }
    } 

    // Update is called once per frame
    void Update()
    {
        
    }
}
