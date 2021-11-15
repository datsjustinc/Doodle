using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyMonsterScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision){
        /*if(collision.CompareTag("Bullet")){
            Destroy(gameObject);
        }*/
        Debug.Log("Collide");
    }

    // Update is called once per frame
    void Update()
    {

    }
}
