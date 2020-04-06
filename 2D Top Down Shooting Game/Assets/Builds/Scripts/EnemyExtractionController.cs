using UnityEngine;
using System.Collections;
public class EnemyExtractionController : MonoBehaviour
{
    public GameObject Target;            // target for mission
    private NPCController targetEnemyControl;
    private Scene_Controller sceneControl;
    // Use this for initialization
    void Start()
    {
        sceneControl = GameObject.FindWithTag("Respawn").GetComponent<Scene_Controller>();

        if (Target != null)
        {
            targetEnemyControl = Target.GetComponentInChildren<NPCController>();
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (targetEnemyControl.isDeath == true)
        {
            sceneControl.enemyExtractionReady = false;
        }
        if (sceneControl.enemyLeft == true)
        {
            //Target.SetActive(false);
        }
    }
    void OnTriggerStay2D(Collider2D trig)
    {
        if (trig.gameObject == Target)
        {
            sceneControl.enemyExtractionReady = true;
        }
    }
}

