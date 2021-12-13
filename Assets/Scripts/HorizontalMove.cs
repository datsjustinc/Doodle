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

    Vector2 CurrentPosition;
    Vector2 EndPosition;

    void Start()
    {   
        HPlatSpeed = Random.Range(0.0030f, 0.0040f);
        HPlatX = Random.Range(Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).x + 1f, Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0)).x - 1f); // gets random x value within screen size
    }

    IEnumerator Move()
    {
        if(transform.position.x >= Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0)).x - 1f)
        {
            yield return new WaitForSeconds(1f);
            moveRight = false;
        }
        if(transform.position.x <= Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).x + 1f)
        {
            yield return new WaitForSeconds(1f);
            moveRight = true;
        }

        if(moveRight == true)
        {   
            transform.GetComponent<SpriteRenderer>().flipX = true; // flip sprite
            transform.position = new Vector2(transform.position.x + HPlatSpeed * HPlatX,transform.position.y);
            //transform.position = Vector2.Lerp((transform.position.x, transform.position.x + HPlatSpeed * HPlatX, 1.0f * Time.fixedDeltaTime));
        }

        if(moveRight == false)
        {
            transform.GetComponent<SpriteRenderer>().flipX = false; // flip sprite
            transform.position = new Vector2(transform.position.x - HPlatSpeed * HPlatX,transform.position.y);
            //transform.position = Vector2.Lerp((transform.position.x, transform.position.x - HPlatSpeed * HPlatX, 1.0f * Time.fixedDeltaTime));
        }
    }
    void Update()
    {
        StartCoroutine(Move());
    }
}
