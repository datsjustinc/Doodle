using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{

    public string nextScene; // create field variable to store scene name

    void Awake()
    {
        Time.timeScale = 0; // pause game at start
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Time.timeScale = 1; // start game
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(nextScene); // load next scene (to main game scene)
        }
    }
}