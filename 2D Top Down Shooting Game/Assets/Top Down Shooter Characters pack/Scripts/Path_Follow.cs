using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path_Follow : MonoBehaviour
{
    private Rigidbody2D rb;
    public Transform[] pointRoutes;
    private int pointInt, waitFinder, rememberHP;
    public float moveSpeed = 120f;
    private float setMoveSpeed;
    public float rotateSpeed = 250f;
    private Transform currentTarget, lastTarget;
    [HideInInspector]
    public Transform aimTarget;
    [HideInInspector]
    public bool targetHide;
    [HideInInspector]
    public bool targetInSight;
    [HideInInspector]
    public bool death = false;
    //[HideInInspector]
    public bool alarm = false;
    public bool alarmPath = false;

    private bool moveToTarget, findEnemy, moveBlock, testNextTarget, coveringTest, SoundAlarm, setMove = false, bypass, enemyAlert;
    private float dist, distLastTarget, z, randomRotation;
    private Animator anim;
    //private NPC_Controller npcController;
    //private Scene_Controller sceneController
    private GameObject[] WallPass;
    private GameObject setGameObject;
    //private NPC_Sighting npcSighting;
    public LayerMask WallPassLayer, targetChecklayer;

    // Start is called before the first frame update
    void Start()
    {
        waitFinder = 10;
        enemyAlert = false;
        coveringTest = false;
        SoundAlarm = false;
        testNextTarget = false;
        moveBlock = false;
        findEnemy = false;
        moveToTarget = true;
        targetInSight = false;

        if (currentTarget == null && !targetInSight && !coveringTest && pointRoutes.Length != 0)
        {
            pointInt = 0;
            currentTarget = pointRoutes[pointInt];
            pointInt++;
            lastTarget = currentTarget;
        }
        else if(pointRoutes.Length == 0)
        {
            Debug.LogError("Enemy Soldie: Transform (pointRoutes) not found. Please add one to the script");
        }
        if (pointRoutes.Length == 1 || alarmPath) 
        {
            coveringTest = true;
        }
        anim = gameObject.GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        //npcController = GetComponentInChildren<NPC_Controller>();
        //npcSighting = GetComponent<NPC_Sighting><>();
        //sceneController = gameObject.FindWithTag("Respawn").GetComponent<Scene_Controller>(); ;
        //WallPass = gameObject.FindGameObjectsWithTag("WallPass");
        //rememberHP = npcController.HP;
    }

    // Update is called once per frame
    void Update()
    {
        if (pointRoutes.Length == 0) 
        {
            return;
        }
        /*        if (sceneController.Alarm == true) 
                {
                    alarm = true;
                }*/
        /*        if (npcController.HP < rememberHP && targetInSight && !findEnemy) 
                {
                    rememberHP = npcController.HP;
                    alarm = true;
                }*/
        if (currentTarget != null) 
        {
            dist = Vector3.Distance(currentTarget.transform.position, transform.position);
        }
        if (lastTarget != null) 
        {
            distLastTarget = Vector3.Distance(lastTarget.transform.position, transform.position);
        }
        if (alarm && pointInt != pointRoutes.Length && alarmPath)
        {
            moveToTarget = true;
            findEnemy = false;
            coveringTest = false;
        }
        if (distLastTarget <= 1.5f && !targetInSight && !coveringTest && !testNextTarget)
        {
            testNextTarget = true;
            StartCoroutine("NextTarget");
        }
        if (distLastTarget <= 1.5f && !targetInSight && coveringTest)
        {
            moveToTarget = false;
            findEnemy = true;
        }
        if (!targetInSight && currentTarget == aimTarget && !moveBlock && !alarmPath)
        {
            currentTarget = lastTarget;
            lastTarget = currentTarget;
            moveToTarget = true;
        }

        if (!targetInSight && currentTarget == aimTarget && !moveBlock && alarmPath)
        {
            coveringTest = true;
            currentTarget = lastTarget;
            lastTarget = currentTarget;
            moveToTarget = true;
        }
        if (targetInSight && !moveBlock)
        {
            StopCoroutine("NextTarget");
            StopCoroutine("Alert");
            SoundAlarm = false;
            currentTarget = aimTarget;
            findEnemy = false;
            if (!enemyAlert)
            {
                enemyAlert = true;
                StartCoroutine("EnemyAlert");
            }
        }
        if (dist <= 30f && targetInSight && currentTarget == aimTarget)
        {
            moveToTarget = false;
        }

        if (dist > 31f && targetInSight && currentTarget == aimTarget)
        {
            moveToTarget = true;
        }
/*        if (moveToTarget && !targetHide)
        {
            setMove = true;
            anim.SetFloat("Speed", 1f);
        }
        else
        {
            setMove = false;
        }*/
    }
    private void FixedUpdate()
    {
        if (pointRoutes.Length == 0) 
        {
            return;
        }
        if (setMoveSpeed < moveSpeed)
            setMoveSpeed += 3f;
        if (setMoveSpeed > moveSpeed)
            setMoveSpeed = moveSpeed;
        if (!targetInSight && !death && SoundAlarm && !targetHide)
        {
            z = Mathf.Atan2((currentTarget.transform.position.y - transform.position.y), (currentTarget.transform.position.x - transform.position.x)) * Mathf.Rad2Deg - 90;
            Quaternion Angel = Quaternion.Euler(transform.rotation.x, transform.rotation.y, transform.rotation.z + z);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Angel, Time.deltaTime * 200);
        }

        /*        if (!findEnemy && !death && !targetHide && !SoundAlarm)
                {
                    if (npcController.Shield == false)
                    {
                        if (!bypass)
                        {
                            z = Mathf.Atan2((currentTarget.transform.position.y - transform.position.y), (currentTarget.transform.position.x - transform.position.x)) * Mathf.Rad2Deg - 90;
                            Quaternion Angel = Quaternion.Euler(transform.rotation.x, transform.rotation.y, transform.rotation.z + z);
                            transform.rotation = Quaternion.RotateTowards(transform.rotation, Angel, Time.deltaTime * rotateSpeed);
                        }
                        else
                        {
                            Quaternion Angel = Quaternion.Euler(transform.rotation.x, transform.rotation.y, randomRotation);
                            transform.rotation = Quaternion.RotateTowards(transform.rotation, Angel, Time.deltaTime * 200);
                        }
                    }
                    else
                    {
                        if (targetInSight)
                        {
                            if (!bypass)
                            {
                                z = Mathf.Atan2((currentTarget.transform.position.y - npcController.SpawnBullet.position.y), (currentTarget.transform.position.x - npcController.SpawnBullet.position.x)) * Mathf.Rad2Deg - 90;
                                Quaternion Angel = Quaternion.Euler(transform.rotation.x, transform.rotation.y, transform.rotation.z + z);
                                transform.rotation = Quaternion.RotateTowards(transform.rotation, Angel, Time.deltaTime * rotateSpeed);
                            }
                            else
                            {
                                Quaternion Angel = Quaternion.Euler(transform.rotation.x, transform.rotation.y, randomRotation);
                                transform.rotation = Quaternion.RotateTowards(transform.rotation, Angel, Time.deltaTime * 200);
                            }
                        }
                        else
                        {
                            if (!bypass)
                            {
                                z = Mathf.Atan2((currentTarget.transform.position.y - transform.position.y), (currentTarget.transform.position.x - transform.position.x)) * Mathf.Rad2Deg - 90;
                                Quaternion Angel = Quaternion.Euler(transform.rotation.x, transform.rotation.y, transform.rotation.z + z);
                                transform.rotation = Quaternion.RotateTowards(transform.rotation, Angel, Time.deltaTime * rotateSpeed);
                            }
                            else
                            {
                                Quaternion Angel = Quaternion.Euler(transform.rotation.x, transform.rotation.y, randomRotation);
                                transform.rotation = Quaternion.RotateTowards(transform.rotation, Angel, Time.deltaTime * 200);
                            }
                        }
                    }
                }

                if (findEnemy && !targetInSight && !death && !SoundAlarm)
                {
                    if (waitFinder > 100)
                    {
                        waitFinder = 0;
                        randomRotation = Random.Range(0, 360);
                    }

                    waitFinder++;
                    if (!alarm)
                    {
                        Quaternion Angel = Quaternion.Euler(transform.rotation.x, transform.rotation.y, randomRotation);
                        transform.rotation = Quaternion.RotateTowards(transform.rotation, Angel, Time.deltaTime * 25);
                    }
                    else
                    {
                        Quaternion Angel = Quaternion.Euler(transform.rotation.x, transform.rotation.y, randomRotation);
                        transform.rotation = Quaternion.RotateTowards(transform.rotation, Angel, Time.deltaTime * 300);
                    }
                }
        */

/*        if (findEnemy && !targetInSight && !death && !SoundAlarm)
        {
            if (waitFinder > 100)
            {
                waitFinder = 0;
                randomRotation = Random.Range(0, 360);
            }

            waitFinder++;
            if (!alarm)
            {
                Quaternion Angel = Quaternion.Euler(transform.rotation.x, transform.rotation.y, randomRotation);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, Angel, Time.deltaTime * 25);
            }
            else
            {
                Quaternion Angel = Quaternion.Euler(transform.rotation.x, transform.rotation.y, randomRotation);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, Angel, Time.deltaTime * 300);
            }
        }*/

        /*        if (!targetInSight && !death && SoundAlarm && !targetHide)
                {
                    z = Mathf.Atan2((currentTarget.transform.position.y - transform.position.y), (currentTarget.transform.position.x - transform.position.x)) * Mathf.Rad2Deg - 90;
                    Quaternion Angel = Quaternion.Euler(transform.rotation.x, transform.rotation.y, transform.rotation.z + z);
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, Angel, Time.deltaTime * 200);
                }*/

        if (moveToTarget && !death && !SoundAlarm && !targetHide)
        {
            if (!alarm)
                rb.AddForce(gameObject.transform.up * moveSpeed);
            else
                rb.AddForce(gameObject.transform.up * moveSpeed * 2);
        }
        //anim.SetBool("Move", setMove);
    }
    IEnumerator NextTarget() 
    {
        if (pointInt < pointRoutes.Length && !alarmPath)
        {
            currentTarget = pointRoutes[pointInt];
            lastTarget = currentTarget;
            pointInt += 1;
        }
        else if (pointInt == pointRoutes.Length && !alarmPath)
        {
            pointInt = 0;
            currentTarget = pointRoutes[pointInt];
            lastTarget = currentTarget;
        }
        if (pointInt < pointRoutes.Length && alarm && alarmPath)
        {
            currentTarget = pointRoutes[pointInt];
            lastTarget = currentTarget;
            pointInt += 1;
        }
        else if (pointInt == pointRoutes.Length && alarm && alarmPath)
        {
            coveringTest = true;
        }

        yield return new WaitForSeconds(1f);
        testNextTarget = false;
    }

/*    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Block" && !targetInSight || coll.gameObject.tag == "Table" && !targetInSight)
        {
            if (!moveBlock)
            {
                moveBlock = true;
                int chiSet = coll.transform.childCount;
                if (chiSet == 2 && coll.gameObject.transform.Find("Enter_point2") != null)
                {
                    GameObject Enter1 = (coll.gameObject.transform.Find("Enter_point1").gameObject);
                    GameObject Enter2 = (coll.gameObject.transform.Find("Enter_point2").gameObject);

                    float distEnter1 = new float();
                    distEnter1 = Vector3.Distance(Enter1.transform.position, transform.position);
                    float distEnter2 = new float();
                    distEnter2 = Vector3.Distance(Enter2.transform.position, transform.position);

                    if (distEnter1 <= distEnter2)
                        currentTarget = Enter1.transform;

                    if (distEnter1 > distEnter2)
                        currentTarget = Enter2.transform;
                }
                else if (chiSet == 2 && coll.gameObject.transform.Find("Enter_point2") == null)
                {
                    GameObject Enter1 = (coll.gameObject.transform.Find("Enter_point1").gameObject);
                    currentTarget = Enter1.transform;
                }
                if (chiSet == 1)
                {
                    GameObject Enter1 = (coll.gameObject.transform.Find("Enter_point1").gameObject);
                    currentTarget = Enter1.transform;
                }
                StartCoroutine("ChangeTarget");
            }
        }

        if (coll.gameObject.tag == "Red team" && !targetInSight)
        {
            if (!moveBlock)
            {
                randomRotation = Random.Range(0, 360);
                moveBlock = true;
                bypass = true;
                StartCoroutine("ChangeTarget");
            }
        }
        if (coll.gameObject.tag == "Wall" && !targetInSight)
        {
            if (!moveBlock)
            {
                moveBlock = true;
                float dist = new float();
                float RemmemberDist = new float();
                RemmemberDist = 0;

                foreach (var Pass in WallPass)
                {
                    Vector2 direction = Pass.transform.position - transform.position;
                    RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, Mathf.Infinity, WallPassLayer.value);
                    if (hit.collider != null && hit.collider.gameObject == Pass)
                    {
                        dist = Vector3.Distance(Pass.transform.position, transform.position);
                        if (dist < RemmemberDist || RemmemberDist == 0)
                        {
                            RemmemberDist = dist;
                            setGameObject = Pass;
                        }
                    }
                }
                if (setGameObject != null)
                {
                    currentTarget = setGameObject.transform;
                    setGameObject = null;
                    StartCoroutine("enterWall");
                }
            }
        }
    }
*/

    /*    void OnTriggerStay2D(Collider2D trig)
        {
            if (trig.gameObject != null && trig.gameObject.tag == "Sound wave" && !targetInSight && !SoundAlarm)
            {
                Collider2D targetCollider = Physics2D.OverlapCircle(transform.position, Mathf.Infinity, targetChecklayer.value);
                if (targetCollider != null && targetCollider.gameObject.tag == npcSighting.targetTag)
                {
                    Vector2 direction = targetCollider.transform.position - transform.position;
                    RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, Mathf.Infinity, npcSighting.TargetLayer);
                    if (hit.collider != null && hit.collider.gameObject.tag == npcSighting.targetTag)
                    {
                        currentTarget = hit.collider.transform;
                        aimTarget = currentTarget;
                        targetInSight = true;
                        alarm = true;
                    }
                }
                else if (targetCollider == null && !targetInSight)
                {
                    StopCoroutine("ChangeTarget");
                    StopCoroutine("enterWall");
                    SoundAlarm = true;
                    currentTarget = trig.transform;
                    StartCoroutine("Alert");
                }
            }
        }*/

    IEnumerator ChangeTarget()
    {
        yield return new WaitForSeconds(0.6f);
        bypass = false;
        if (targetInSight)
        {
            currentTarget = aimTarget;
            moveBlock = false;
        }
        if (!targetInSight)
        {
            currentTarget = lastTarget;
            moveBlock = false;
        }
    }
    IEnumerator enterWall()
    {
        yield return new WaitForSeconds(1f);

        if (targetInSight)
        {
            currentTarget = aimTarget;
            moveBlock = false;
        }
        if (!targetInSight)
        {
            currentTarget = lastTarget;
            moveBlock = false;
        }
    }

/*    IEnumerator Alert()
    {
        moveBlock = false;
        bool lastSetMove = moveToTarget;
        moveToTarget = false;

        if (targetInSight == true)
        {
            currentTarget = aimTarget;
            SoundAlarm = false;
            moveToTarget = lastSetMove;
            StopCoroutine("Alert");
        }

        yield return new WaitForSeconds(3f);

        findEnemy = false;

        if (npcController.DeathTest == false)
        {
            sceneController.Alarm = true;
        }

        if (targetInSight)
        {
            currentTarget = aimTarget;
            SoundAlarm = false;
            moveToTarget = lastSetMove;
        }
        if (!targetInSight)
        {
            currentTarget = lastTarget;
            SoundAlarm = false;
            moveToTarget = lastSetMove;
        }

        if (moveBlock && !targetInSight)
        {
            currentTarget = lastTarget;
            SoundAlarm = false;
            moveToTarget = lastSetMove;
        }

    }*/

/*    IEnumerator EnemyAlert()
    {
        yield return new WaitForSeconds(2f);
        if (npcController.DeathTest == false)
        {
            sceneController.Alarm = true;
        }
    }*/
}
