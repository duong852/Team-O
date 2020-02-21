using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace GearsAndBrains
{

    public class Extraction_Zone_Control : MonoBehaviour
    {
        [Tooltip("select target for mission")]
        public Enemy_Control Target;            // target for mission
        public Image imgObjectives, imgLevelAlarm;
        public Text textObjectives, textLevelAlarm, TextObjectCommentObjectives;        
        private bool Wait, reinforcText, leftZoneText;
        private Scene_Control sceneControl;

        // Use this for initialization
        void Start()
        {
            sceneControl = GameObject.FindWithTag("Respawn").GetComponent<Scene_Control>();
        }

        // Update is called once per frame
        void Update()
        {
            if (Target.DeathTest == true)
            {
                imgObjectives.color = new Color(0.1f, 0.8f, 0, 1);
                textObjectives.text = "Completed";
            }
            if (sceneControl.Alarm == true)
            {
                imgLevelAlarm.color = new Color(0.7f, 0, 0, 1);
                textLevelAlarm.text = "Alarm";
            }

            if (sceneControl.ReinforcementON == true && !Wait && !reinforcText)
            {
                Wait = true;
                reinforcText = true;
                TextObjectCommentObjectives.text = "enemy reinforcements arrived";
                TextObjectCommentObjectives.enabled = true;
                StartCoroutine("wait");
            }

            if (sceneControl.EnemyLeave == true && !Wait && !leftZoneText)
            {
                Wait = true;
                leftZoneText = true;
                TextObjectCommentObjectives.text = "target left zone of action";
                textObjectives.text = "Failed";
                TextObjectCommentObjectives.enabled = true;
                StartCoroutine("wait");
            }

        }

        void OnTriggerStay2D(Collider2D trig)
        {
            if (trig.gameObject.tag == "Blue team")
            {
                if (Target.DeathTest == true)
                {
                    GameObject uiMenu = GameObject.FindWithTag("GameController");
                    if (uiMenu != null && !Wait)
                    {
                        Wait = true;
                        uiMenu.GetComponent<UI_menu>().MenuSet();
                    }
                }
                else if (Target.DeathTest != true && sceneControl.EnemyLeave != true)
                {
                    if (!Wait)
                    {
                    Wait = true;
                    TextObjectCommentObjectives.text = "the mission is not completed";
                    TextObjectCommentObjectives.enabled = true;
                    StartCoroutine("wait");
                    }
                }
                else if (Target.DeathTest != true && sceneControl.EnemyLeave == true)
                {
                    if (!Wait)
                    {
                        Wait = true;
                        GameObject uiMenu = GameObject.FindWithTag("GameController");
                        if (uiMenu != null && !Wait)
                        {
                            Wait = true;
                            uiMenu.GetComponent<UI_menu>().RestartSet();
                        }
                    }
                }
            }
        }

        IEnumerator wait()
        {
            yield return new WaitForSeconds(3f);
            TextObjectCommentObjectives.enabled = false;
            Wait = false;
        }
    }
}
