﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelController : MonoBehaviour
{
    public bool levelFinished;
    void Start()
    {
        levelFinished = false;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Blue team")
        {
            Debug.Log("collision");
            if (levelFinished == true)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                levelFinished = false;
            }
        }
    }

}
