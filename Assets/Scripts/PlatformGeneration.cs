using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlatformGeneration : MonoBehaviour
{

    public int MinBlocks = 50; // set platform target spawn amount
    public int CurBlocks = 0; // count spawned blocks
    public float StartPositionY = -3.7f; // starting y coord to spawn blocks
    public float FactorY = 1f; // distance between each spawned block
    public GameObject Spring; // stores power-up prefab
    public GameObject SpringShoes; // stores power-up prefab
    public GameObject Trampoline; // stores power-up prefab
    public GameObject PropellorHat; // stores power-up prefab
    public GameObject RegularPlatform; // stores platform prefab
    public GameObject HorizontalPlatform; // stores platform prefab
    public GameObject BrokenPlatform; // stores platform prefab
    public List<GameObject> platforms = new List<GameObject>(); // list to store platform instance ids
    public List<GameObject> powerups = new List<GameObject>(); // list to store pwer-up instance ids
    public GameObject PlayerCharacter; // store player object
    GameObject PowerupType; // used to store randomly generated power-up type
    GameObject PlatformType; // used to store randomly generated platform type
    public CharacterMovement game; // used to access variable condition in another script
    public bool check = true; // control code in update function
    public float RegularMin = 0;
    public float RegularMax = 60;
    public float HorizontalMin = 60;
    public float HorizontalMax = 90;
    public float BrokenMin = 90;
    public float BrokenMax = 100;

    void Start()
    {
        game = GameObject.Find("Character").GetComponent<CharacterMovement>(); // get script from game object
    }
    void RandomGeneration()
    {
        var rand = Random.Range(0, 100); // decide a random number within range to generate
        var rand2 = Random.Range(0, 100); // decide a random number within range to generate

        if (rand >= RegularMin && rand < RegularMax)
        {
            PlatformType = RegularPlatform;
        }

        else if (rand >= HorizontalMin && rand < HorizontalMax)
        {
            PlatformType = HorizontalPlatform; 
        }

        else if (rand >= BrokenMin && rand <= BrokenMax)
        {
            PlatformType = BrokenPlatform;
        }

        if (rand2 >= 20 && rand2 <= 25)
        {
            PowerupType = Spring;
        }

        else if (rand2 >= 45 && rand2 <= 50)
        {
            PowerupType = SpringShoes;
        }

        else if (rand2 >= 70 && rand2 <= 75)
        {
            PowerupType = Trampoline;
        }

        else if (rand2 >= 95 && rand2 <= 100)
        {
            PowerupType = PropellorHat; 
        }

        if (CurBlocks < MinBlocks)
        {
            float random_xfactor = Random.Range(Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).x + 1f, Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0)).x - 1f); // gets random x value within screen size
            //float random_yfactor = Random.Range(Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).y, Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height)).y);
            StartPositionY += FactorY; // factor to increment next platform y position by
            
            var instance = Instantiate(PlatformType, new Vector3(random_xfactor, StartPositionY, 2.177098f), PlatformType.transform.rotation); // create new platform instance
           
            
            if (PlatformType == RegularPlatform && PowerupType != null)
            {
                var instance2 = Instantiate(PowerupType, new Vector3(random_xfactor, StartPositionY + 0.5f, 2.177098f), PowerupType.transform.rotation); // create new power-up instance
                // powerups.Add(instance2); // add current instance of object to list
            }
            
            platforms.Add(instance); // add current instance of object to list
            CurBlocks += 1; // keep track of spawned blocks
        }

        
        else if (CurBlocks == MinBlocks && PlayerCharacter.transform.position.y > platforms[30].transform.position.y)
        {
            for (int i = 0; i < 10; i++)
            {
                Destroy(platforms[i]);
                platforms.RemoveAt(i);

                /*
                Destroy(powerups[i]);
                powerups.RemoveAt(i);
                */

                CurBlocks -= 1; 
            }
        }


        if (game.gameEnd == true)
        {
            foreach (var x in platforms) // goes through each platform instance in list
            {
                Destroy(x); // destroy each platform instance
            }

            /*
            foreach (var x in powerups) // goes through each platform instance in list
            {
                Destroy(x); // destroy each platform instance
            }
            */

            check = false; // don't run this function again if game is over (code in update function)
        }
    }

    IEnumerator Difficulty()
    {
        yield return new WaitForSeconds(20f); // difficulty increases every 10 seconds

        if (RegularMax > 30) // set maximum difficulty amount by setting minimum regular platform requirement
        {
            RegularMax -= 2; // chances for a regular platform spawn decrease
            HorizontalMin -= 2; // chances for a horizontal platform spawn increases
            HorizontalMax -= 1; // adjust to allow changes for broken platform to increase
            BrokenMin -= 1; // chances for a broken platform increases
        }
    }

    void Update()
    {
        if (check)
        {
            RandomGeneration();
        }

        StartCoroutine(Difficulty());

        if (Input.GetKeyDown(KeyCode.R)) 
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
