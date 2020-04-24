using UnityEngine;
using UnityEngine.UI;
using System.Collections;
public class ExtractionZone : MonoBehaviour
{
    public Image imageObjectives, imageLevelAlarm;
    public Text textObjectives, textLevelAlarm;
    private bool Wait, reinforceText, leftZoneText;
    private Scene_Controller sceneControl;
    private LevelController levelController;
    [HideInInspector]
    public bool missionComplete;
    public int enemyCount;
    // Use this for initialization
    void Start()
    {
        missionComplete = false;
        levelController = GameObject.FindWithTag("LevelArrow").GetComponent<LevelController>();
        sceneControl = GameObject.FindWithTag("Respawn").GetComponent<Scene_Controller>();
    }
    // Update is called once per frame
    void Update()
    {
        if (enemyCount == 0) 
        {
            missionComplete = true;
        }
        if (missionComplete == true)
        {
            imageObjectives.color = new Color(0.1f, 0.8f, 0, 1);
            textObjectives.text = "Completed";
            levelController.levelFinished = true;

        }
        if (sceneControl.alarmOn == true)
        {
            imageLevelAlarm.color = new Color(0.7f, 0, 0, 1);
            textLevelAlarm.text = "ON";
        }
        if (sceneControl.reinforcementON == true && !Wait && !reinforceText)
        {
            Wait = true;
            reinforceText = true;
            StartCoroutine("wait");
        }
        if (sceneControl.enemyLeft == true && !Wait && !leftZoneText)
        {
            Wait = true;
            leftZoneText = true;
            StartCoroutine("wait");
        }

    }
    void OnTriggerStay2D(Collider2D trig)
    {
        if (trig.gameObject.tag == "Blue team")
        {
            if (missionComplete == true)
            {
                GameObject menuController = GameObject.FindWithTag("GameController");
                if (menuController != null && !Wait)
                {
                    Wait = true;
                    menuController.GetComponent<UI_Manager>().SetMenu();
                }
            }
            else if (missionComplete != true && sceneControl.enemyLeft != true)
            {
                if (!Wait)
                {
                    Wait = true;
                    StartCoroutine("wait");
                }
            }
            else if (missionComplete != true && sceneControl.enemyLeft == true)
            {
                if (!Wait)
                {
                    Wait = true;
                    GameObject uiMenu = GameObject.FindWithTag("GameController");
                    if (uiMenu != null && !Wait)
                    {
                        Wait = true;
                        uiMenu.GetComponent<UI_Manager>().SetRestart();
                    }
                }
            }
        }
    }

    IEnumerator wait()
    {
        yield return new WaitForSeconds(3f);
        Wait = false;
    }
}

