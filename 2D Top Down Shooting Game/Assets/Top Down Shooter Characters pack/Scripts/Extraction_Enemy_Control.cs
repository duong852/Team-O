using UnityEngine;
using System.Collections;

namespace GearsAndBrains
{

    public class Extraction_Enemy_Control : MonoBehaviour
    {

        public GameObject Target;            // target for mission
        private Enemy_Control targetEnemyControl;
        private Scene_Control sceneControl;

        // Use this for initialization
        void Start()
        {
            sceneControl = GameObject.FindWithTag("Respawn").GetComponent<Scene_Control>();

            if (Target != null)
            {
                targetEnemyControl = Target.GetComponentInChildren<Enemy_Control>();
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (targetEnemyControl.DeathTest == true)
            {
                sceneControl.EnemyExtractionReady = false;
            }
           
            if (sceneControl.EnemyLeave == true)
            {
                Target.SetActive(false);
            }            
        }

        void OnTriggerStay2D(Collider2D trig)
        {
            if (trig.gameObject == Target)
            {
                sceneControl.EnemyExtractionReady = true;
            }
        }
    }
}
