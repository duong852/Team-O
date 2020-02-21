using UnityEngine;
using System.Collections;

namespace GearsAndBrains
{

    public class Scene_Control : MonoBehaviour
    {
        public bool Alarm = false, ReinforcementON, EnemyExtractionReady = false, WaitExtraction = false, EnemyLeave = false;
        public float ReinforcmentsTime = 30f, EnemyExtractionTime = 30;
        private bool ReinforcementWait;
        private AudioSource myAudioSours;
        public GameObject[] reinforcement;
        private Sounds_Control soundControl;

        // Use this for initialization
        void Start()
        {
            ReinforcementON = false;
            myAudioSours = GetComponent<AudioSource>();
            soundControl = GameObject.FindWithTag("MainCamera").GetComponent<Sounds_Control>();
        }

        // Update is called once per frame
        void Update()
        {
            if (Alarm && !ReinforcementWait)
            {
                ReinforcementWait = true;
                myAudioSours.enabled = true;
                StartCoroutine("ReinforcementArrived");
            }

            if (EnemyExtractionReady && !WaitExtraction)
            {
                WaitExtraction = true;
                StartCoroutine("WaitEnemyExtraction");
            } 
            else if (!EnemyExtractionReady)
            {
                WaitExtraction = false;
                StopCoroutine("WaitEnemyExtraction");
            }       
        }        

        void OnTriggerStay2D(Collider2D trig)
        {
            if (trig.gameObject.tag == "Player")
            {
                soundControl.Street = false;
            }
        }
        void OnTriggerExit2D(Collider2D trig)
        {
            if (trig.gameObject.tag == "Player")
            {
                soundControl.Street = true;
            }
        }

        IEnumerator ReinforcementArrived()
        {
            yield return new WaitForSeconds(ReinforcmentsTime);
            foreach (var force in reinforcement)
            {
                force.SetActive(true);
            }
            ReinforcementON = true;
        }

        IEnumerator WaitEnemyExtraction()
        {
            yield return new WaitForSeconds(EnemyExtractionTime);
            EnemyLeave = true;
        }
    }
}
