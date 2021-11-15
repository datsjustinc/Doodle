using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformBounce : MonoBehaviour
{
     public float bounceForce = 6.0f;
     
    void OnCollisionEnter2D(Collision2D collision)
    {
        Rigidbody2D rb = collision.collider.GetComponent<Rigidbody2D>();
        if (rb != null){
            Vector2 velocity = rb.velocity;
            velocity.y = bounceForce;
            rb.velocity = velocity;
        }

    }
}
