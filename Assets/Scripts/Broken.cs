using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Broken : MonoBehaviour
{
    public EdgeCollider2D cc;
    public Sprite BrokenSprite;
    

     private void Awake(){
        cc=GetComponent<EdgeCollider2D>();
        cc.isTrigger = true;
    }

    void Start(){
        //spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    
    
    private void OnTriggerEnter2D(Collider2D other){
        if( other.CompareTag("Character")){
           gameObject.GetComponent<SpriteRenderer>().sprite = BrokenSprite;
             //gameObject.SetActive(false);
          GetComponent<Rigidbody2D>().gravityScale = 1f;
         //  GetComponent<Rigidbody2D>().isStatic = false;
           Destroy (gameObject, 3);
           
           
        }
    }
}
