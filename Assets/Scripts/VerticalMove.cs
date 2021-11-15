using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalMove : MonoBehaviour
{
   public float VPlatSpeed = 8.0f;
    public float VPlatX = 1.0f;
    bool moveUp = true;

   
    void Update()
    {
        if(transform.position.y >2){
            moveUp= false;
        }
        if(transform.position.y < 0){
            moveUp = true;
        }

        if(moveUp == true){
            transform.position = new Vector2(transform.position.x,transform.position.y + VPlatSpeed * VPlatX);
        }
        if(moveUp == false){
            transform.position = new Vector2(transform.position.x,transform.position.y  - VPlatSpeed * VPlatX);
        }

        
    }
}
