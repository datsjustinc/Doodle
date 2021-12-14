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

    public AudioSource shoot;
    
    // Start is called before the first frame update
    
    void Awake(){
        //bullet.transform.Translate((Vector2.up * Time.deltaTime) * speed);
    }

    void SpawnBullet()
    {
        var xpos = character.transform.position.x;
        var ypos = character.transform.position.y;
        GameObject b = Instantiate(bullet, new Vector3(xpos, ypos), Quaternion.identity);
         
    }


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            shoot.Play(); // play sound
            SpawnBullet();
        }   
        //bullet.transform.Translate((Vector3.up * Time.deltaTime) * speed);
    }
}
