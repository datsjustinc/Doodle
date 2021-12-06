using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformGeneration : MonoBehaviour
{

    public int MinBlocks = 50; // set platform target spawn amount
    public int CurBlocks = 0; // count spawned blocks
    public float StartPositionY = -3.7f; // starting y coord to spawn blocks
    public float FactorY = 1f; // distance between each spawned block
    public GameObject RegularPlatform; // stores platform prefab
    public GameObject HorizontalPlatform; // stores platform prefab
    public GameObject BrokenPlatform; // stores platform prefabq
    public List<GameObject> platforms = new List<GameObject>(); // list to store platform instance ids
    public GameObject PlayerCharacter; // store player object
    GameObject PlatformType; // used to store randomly generated platform type
    public CharacterMovement game; // used to access variable condition in another script
    public bool check = true; // control code in update function

    void Start()
    {
        game = GameObject.Find("Character").GetComponent<CharacterMovement>(); // get script from game object
    }
    void RandomGeneration()
    {
        var rand = Random.Range(0, 100); // decide a random number within range to generate

        if (rand >= 0 && rand < 60)
        {
            PlatformType = RegularPlatform;
        }

        else if (rand >= 60 && rand < 90)
        {
            PlatformType = HorizontalPlatform; 
        }

        else if (rand >= 90 && rand <= 100)
        {
            PlatformType = BrokenPlatform;
        }

        if (CurBlocks < MinBlocks)
        {
            float random_xfactor = Random.Range(Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).x + 1f, Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0)).x - 1f); // gets random x value within screen size
            //float random_yfactor = Random.Range(Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).y, Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height)).y);
            StartPositionY += FactorY; // factor to increment next platform y position by
            
            var instance = Instantiate(PlatformType, new Vector3(random_xfactor, StartPositionY, 2.177098f), Quaternion.identity); // create new platform instance

            instance.transform.Rotate(0, 0, 90); // rotate new platform instance to be horizontal
            platforms.Add(instance); // add current instance of object to list
            CurBlocks += 1; // keep track of spawned blocks
        }

        else if (CurBlocks == MinBlocks && PlayerCharacter.transform.position.y > platforms[30].transform.position.y)
        {
            for (int i = 0; i < 10; i++)
            {
                Destroy(platforms[i]);
                platforms.RemoveAt(i);
                CurBlocks -= 1; 
            }
        }

        if (game.gameEnd == true)
        {
            foreach (var x in platforms) // goes through each platform instance in list
            {
                Destroy(x); // destroy each platform instance
            }

            check = false; // don't run this function again if game is over (code in update function)
        }
    }

    void Update()
    {
        if (check)
        {
            RandomGeneration();
        }
    }
}
