using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene_Controller : MonoBehaviour
{
    public bool alarmOn = false;
    public bool reinforcementON, enemyExtractionReady = false, waitForExtraction = false, enemyLeft = false;
    public float reinforcementTimes = 30f, enemyExtrctionTime = 30;
    private bool waitForReinforcement;
    private AudioSource audio_Source;
    public GameObject[] reinforment;
    private Sounds_Control sound_Controller;
    // Start is called before the first frame update
    void Start()
    {
        reinforcementON = false;
        audio_Source = GetComponent<AudioSource>();
        sound_Controller = GameObject.FindWithTag("MainCamera").GetComponent<Sounds_Control>();
    }

    // Update is called once per frame
    void Update()
    {
        if (alarmOn && !waitForReinforcement) 
        {
            waitForReinforcement = true;
            audio_Source.enabled = true;
            StartCoroutine("ReinforcementArrived");
        }
        if (enemyExtractionReady && !waitForExtraction)
        {
            waitForExtraction = true;
            StartCoroutine("WaitForEnemyExtraction");
        }
        else if (!enemyExtractionReady) 
        {
            waitForExtraction = false;
            StartCoroutine("WaitForEnemyExtraction");
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player") 
        {
            sound_Controller.Street = false;
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player") 
        {
            sound_Controller.Street = true;
        }
    }
    IEnumerator ReinforcementArrived() 
    {
        yield return new WaitForSeconds(reinforcementTimes);
        foreach (var force in reinforment) 
        {
            //force.SetActive(true);
        }
    }
    IEnumerator WaitForEnemyExtraction() 
    {
        yield return new WaitForSeconds(enemyExtrctionTime);
        enemyLeft = true;
    }
}
