using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyBulletMove : MonoBehaviour
{
    public int speed = 50; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void MoveObject(){
         transform.Translate((Vector2.up * Time.deltaTime) * speed);
    }

    // Update is called once per frame
    void Update()
    {
        MoveObject();

        if (transform.position.y >= 30){
            Destroy(gameObject);
        }
    }
}
