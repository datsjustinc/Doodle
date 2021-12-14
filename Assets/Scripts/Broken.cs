using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Broken : MonoBehaviour
{
    public EdgeCollider2D cc;
    public Sprite BrokenSprite;

    public Rigidbody2D rb; // create field variable for object's rigid body component
    
     private void Awake(){
        cc=GetComponent<EdgeCollider2D>();
        cc.isTrigger = true;
    }

    void Start(){
        //spriteRenderer = gameObject.GetComponent<SpriteRenderer>();

        rb = GameObject.Find("Character").GetComponent<Rigidbody2D>(); // get script from game object
    }

    
    
    private void OnTriggerEnter2D(Collider2D other){
        if(other.CompareTag("Character") && rb.velocity.y < 0.1){
           gameObject.GetComponent<SpriteRenderer>().sprite = BrokenSprite;
             //gameObject.SetActive(false);
          GetComponent<Rigidbody2D>().gravityScale = 1f;
         //  GetComponent<Rigidbody2D>().isStatic = false;
           Destroy (gameObject, 3);
           
           
        }
    }
}
