using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerScore : MonoBehaviour 
{
    public int pt; // create field variable to assign points to
    public int current; // gets players current y position
    public TMP_Text displayScore; // create field variable to hold text
    public GameObject PlayerCharacter; // store player object
    public CharacterMovement game; // used to access variable condition in another script
    

    public bool reset; // used to reset score

    public void Awake() // function called before game starts
    {
        game = GameObject.Find("Character").GetComponent<CharacterMovement>(); // get script from game object

        displayScore = GetComponent<TMP_Text>(); // get text component and store in display score

        pt = 0; // set player score to 0
        current = (int)PlayerCharacter.transform.position.y; // records player's initial y position
        
        reset = false;
    }

    // Update is called once per frame
    void Update() 
    {
        if (PlayerCharacter.transform.position.y > current) // score based on player's highest y position reached
        {
            pt += (int)PlayerCharacter.transform.position.y - current; // set score to different of highest y position reached and current y position
            displayScore.text = pt.ToString(); // print text to screen
            current = (int)PlayerCharacter.transform.position.y; // update highest y position score
        }

        if (game.gameEnd == true)
        {
            if (PlayerCharacter.transform.GetComponent<SpriteRenderer>().enabled == false)
            {
                //transform.position.x = 200f; 
            }
        }
     
    }
}
