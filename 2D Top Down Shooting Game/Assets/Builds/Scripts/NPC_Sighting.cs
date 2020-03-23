using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class NPC_Sighting : MonoBehaviour
{
    private bool startCoroutine = false;
    private bool aim = false;
    private bool targetDeath = false;
    private Path_Follow pathFollow;
    private NPC_Controller npcController;
    private Scene_Controller sceneController;

    private Transform lastView;
    public bool SightingDraw;

    public LayerMask targetLayer;
    public string targetTag;
    public string allyTag;
    [Range(0f, 100f)]
    public float viewRange;
    [Range(0f, 180f)]
    public float viewAngel;
    [HideInInspector]
    public bool death;
    public Transform rotationTransform;


    // Start is called before the first frame update
    void Start()
    {
        //sceneController = GameObject.FindWithTag("Respawn").GetComponent<Scene_Controller>();
        pathFollow = GetComponent<Path_Follow>();
        npcController = GetComponentInChildren<NPC_Controller>();
    }

    // Update is called once per frame
    void Update()
    {
        if (death) 
        {
            enabled = false;
        }
        if (targetDeath && !startCoroutine && aim)
        {
            StartCoroutine("waitTarget");
        }
    }

/*    private void FixedUpdate()
    {
        Collider2D[] targetColliders = Physics2D.OverlapCircleAll(transform.position, viewRange, targetLayer.value);
        foreach (var targetCollider in targetColliders)
        {
            if (targetCollider.gameObject.tag == targetTag || targetCollider.gameObject.tag == allyTag)
            {
                float distance = Vector2.Distance(targetCollider.transform.position, rotationTransform.position);
                Vector2 targetDir = targetCollider.transform.position - rotationTransform.position;
                Vector2 forward = rotationTransform.up;
                float angel = Vector2.Angle(targetDir, forward);
                if (distance <= viewRange && angel <= viewAngel)
                {
                    Vector2 direction = targetCollider.transform.position - transform.position;						
                    RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, Mathf.Infinity, targetLayer.value);
                    if (hit.collider != null && hit.collider.gameObject.tag == targetTag)
                    {
                        targetDeath = hit.collider.gameObject.transform.Find("Player").GetComponent<Player>().deathTest;
                        pathFollow.targetInSight = true;
                        pathFollow.aimTarget = targetCollider.transform;

                        if (startCoroutine)
                        {
                            pathFollow.targetHide = false;
                            StopCoroutine("waitTarget");
                            startCoroutine = false;
                        }

                        if (aim)
                            npcController.TargetIn = true;

                        if (hit.distance <= viewRange && !aim)
                        {
                            targetDeath = false;
                            aim = true;
                            npcController.AimWeapon();
                            npcController.TargetIn = true;
                        }
                        if (hit.distance > viewRange && aim)
                        {
                            npcController.TargetIn = false;

                            if (!startCoroutine)
                                StartCoroutine("waitTarget");
                        }
                    }
                    else
                    {
                        if (aim && !startCoroutine && targetCollider.gameObject.tag != allyTag)
                        {
                            StartCoroutine("waitTarget");
                        }
                    }

                    if (hit.collider != null && hit.collider.gameObject.tag == allyTag && hit.collider.gameObject.GetComponentInChildren<Enemy_Control>().DeathTest == true && sceneControl.Alarm == false)
                    {
                        //sceneControl.Alarm = true;
                        pathFollow.StartCoroutine("EnemyAlert");
                    }

                }
                else
                {
                    if (aim && !startCoroutine && targetCollider.gameObject.tag != allyTag)
                    {
                        StartCoroutine("waitTarget");
                    }
                }
            }
        }
    }
*/
    /*    IEnumerator waitTarget()
        {
            startCoroutine = true;
            npcController.TargetIn = false;
            pathFollow.targetHide = true;
            yield return new WaitForSeconds(5f);
            aim = false;
            pathFollow.targetHide = false;
            pathFollow.targetInSight = false;
            npcController.AimWeapon();
            startCoroutine = false;
            targetDeath = false;
        }*/
}
