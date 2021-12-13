using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Adapted from and Credits: Alexander Zotov (Youtube)
public class ScrollObject : MonoBehaviour
{
    [SerializeField]
    private Transform background;
    // public CharacterMovement game; // used to access variable condition in another script

    void Start()
    {
        // game = GameObject.Find("Character").GetComponent<CharacterMovement>(); // get script from game object
    }
    void Scroll()
    {

        if (transform.position.y >= background.position.y + 9.60f)
        {
            background.position = new Vector2(background.position.x, transform.position.y + 9.60f);
        }

        else if (transform.position.y <= background.position.y - 9.60f)
        {
            background.position = new Vector2(background.position.x, transform.position.y - 9.60f);
        }
    }

    void Update()
    {
        Scroll(); // calls scroll function
    }
}