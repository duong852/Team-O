using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Enemy : Entity
{
    
    // Start is called before the first frame update
    void Start()
    {
        health = 50;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void gameOver()
    {
        Destroy(gameObject);
    }
}
