using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterBigScript : MonoBehaviour
{
    
    public int hp = 2;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision){
        if(collision.CompareTag("Bullet")){
            if(hp <= 2){
                --hp;  
                //Debug.Log("Hit");
            }
            if(hp <= 0){
                Destroy(gameObject);
            }
        }
        //Debug.Log("Collide");
    }

    // Update is called once per frame
    void Update()
    {

    }
}
