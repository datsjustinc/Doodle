using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Broken : MonoBehaviour
{
    public EdgeCollider2D cc;

     private void Awake(){
        cc=GetComponent<EdgeCollider2D>();
        cc.isTrigger = true;
    }

    
    
    private void OnTriggerEnter2D(Collider2D other){
        if( other.CompareTag("Player")){
             gameObject.SetActive(false);
            
           
           
        }
    }
}
