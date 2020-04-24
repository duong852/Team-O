using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelController : MonoBehaviour
{
    [HideInInspector]
    public bool levelFinished;
    [HideInInspector]
    public static int levelCounter;
    void Start()
    {
        levelFinished = false;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Blue team")
        {
            if (levelFinished == true)
            {
                if(levelCounter < 2)
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                levelFinished = false;
            }
        }
    }

}
