using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalMove : MonoBehaviour
{
    // public float HPlatSpeed = 8.0f;
    // public float HPlatX = 1.0f;

    public float HPlatSpeed;
    public float HPlatX;
    bool moveRight = true;

    void Start()
    {   
        HPlatSpeed = Random.Range(0.0025f, 0.0030f);
        HPlatX = Random.Range(Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).x + 1f, Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0)).x - 1f); // gets random x value within screen size
    }
    void Update()
    {
        if(transform.position.x > Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0)).x - 1f)
        {
            moveRight = false;
        }
        if(transform.position.x < Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).x + 1f)
        {
            moveRight = true;
        }

        if(moveRight == true){
            transform.position = new Vector2(transform.position.x + HPlatSpeed * HPlatX,transform.position.y);
        }
        if(moveRight == false){
            transform.position = new Vector2(transform.position.x - HPlatSpeed * HPlatX,transform.position.y);
        }

        
    }
}
