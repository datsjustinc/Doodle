using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyShootingScript : MonoBehaviour
{
    public GameObject bullet;
    public GameObject character;
    public int speed = 5;
    public int x = 0;
    public int y = 0;
    
    // Start is called before the first frame update
    
    void Awake(){
        //bullet.transform.Translate((Vector2.up * Time.deltaTime) * speed);
    }

    void SpawnBullet(){
         GameObject b = Instantiate(bullet, new Vector3(character.transform.position.x, character.transform.position.y), Quaternion.identity);
         
    }


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0)){
            SpawnBullet();
        }   
        //bullet.transform.Translate((Vector3.up * Time.deltaTime) * speed);
    }
}