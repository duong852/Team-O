using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : Entity
{
    
    
    // Start is called before the first frame update
    void Start()
    {
        health = 100;
        movementspeed = 5f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //we can replace this with the current scene if we implement multiple levels
    void gameOver()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
