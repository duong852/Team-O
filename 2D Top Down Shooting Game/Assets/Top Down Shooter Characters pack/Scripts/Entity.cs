using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public float movementspeed;
    public int health;
    public string tag = "Entity";
    
    // Start is called before the first frame update
    void Start()
    {
        
       
    }

    // Update is called once per frame
    void Update()
    {
    
    }

    void checkHealth()
    {
        if(health <= 0)
        {
            gameOver();
        }
    }

    void gameOver()
    {

    }

    
}
