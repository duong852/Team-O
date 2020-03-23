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
    [HideInInspector]
    public bool Alarm = false;
    public bool alarmPath = false;

    private bool moveToTarget, findEnemy, moveBlock, testNextTarget, coveringTest, SoundAlarm, setMove = false, bypass, enemyAlert;
    private float dist, distLastTarget, z, randomRotation;
    private Animator anim;
    private NPC_Controller npcController;
    private Scene_Controller sceneController;
    private GameObject[] WallPass;
    private GameObject setGameObject;
    private NPC_Sighting npcSighting;
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
        npcController = GetComponentInChildren<NPC_Controller>();
        npcSighting = GetComponent<NPC_Sighting>();
        sceneController = GameObject.FindWithTag("Respawn").GetComponent<Scene_Controller>(); ;
        WallPass = GameObject.FindGameObjectsWithTag("WallPass");
        rememberHP = npcController.HP;
    }

    // Update is called once per frame
    void Update()
    {
        if (pointRoutes.Length == 0)
        {
            return;
        }

        //if (sceneController.alarmOn == true) 
        //{
        //    Alarm = true;
        //}

        if (npcController.HP < rememberHP && !targetInSight && !findEnemy) 
        {
            rememberHP = npcController.HP;
            //findEnemy = true;
            //Alarm = true;
            //StartCoroutine("Alert");
        }
        //current target
        if (currentTarget != null)
        {
            dist = Vector3.Distance(currentTarget.transform.position, transform.position);
        }
        if (lastTarget != null)
        {
            distLastTarget = Vector3.Distance(lastTarget.transform.position, transform.position);
        }
        //next target
        if (Alarm && pointInt != pointRoutes.Length && alarmPath) 
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
        //covering target
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
        //aim for target
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
        //distance aim target
        if (dist <= 30f && targetInSight && currentTarget == aimTarget) 
        {
            moveToTarget = false;
        }
        if (dist >= 31f && targetInSight && currentTarget == aimTarget) 
        {
            moveToTarget = true;
        }
        //move enemy
        if (moveToTarget && !targetHide)
        {
            setMove = true;
            if (npcController.isAim == false)
            {
                //anim.SetFloat("Speed", 1);
            }
            else
            {
                //anim.SetFloat("Speed", 0.8f);
            }
        }
        else 
        {
            setMove = false;
        }



    }
    private void FixedUpdate()
    {
        //no patrol nodes
        if (pointRoutes.Length == 0) 
        {
            return;
        }
        //moving speed
        if (npcController.isAim == false)
        {
            if (setMoveSpeed < moveSpeed)
                setMoveSpeed += 3f;
            if (setMoveSpeed > moveSpeed)
                setMoveSpeed = moveSpeed;
        }
        else 
        {
            if (setMoveSpeed > moveSpeed)
                setMoveSpeed -= moveSpeed;
            if (setMoveSpeed < moveSpeed)
                setMoveSpeed = moveSpeed;
        }


        //aiming
        if (!findEnemy && !death && !targetHide && !SoundAlarm)
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
        //aiming find target
        if (findEnemy && !targetInSight && death && !SoundAlarm) 
        {
            if (waitFinder > 100) 
            {
                waitFinder = 0;
                randomRotation = Random.Range(0,360);
            }
        }
        waitFinder++;
        if (!Alarm)
        {
            Quaternion Angel = Quaternion.Euler(transform.rotation.x, transform.rotation.y, randomRotation);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Angel, Time.deltaTime * 25);
        }
        else 
        {
            Quaternion Angel = Quaternion.Euler(transform.rotation.x,transform.rotation.y,randomRotation);
            transform.rotation = Quaternion.RotateTowards(transform.rotation,Angel,Time.deltaTime*300);
        }
        //sound shoot
        if (!targetInSight && !death && SoundAlarm && !targetHide)
        {
            z = Mathf.Atan2((currentTarget.transform.position.y - transform.position.y), (currentTarget.transform.position.x - transform.position.x)) * Mathf.Rad2Deg - 90;
            Quaternion Angel = Quaternion.Euler(transform.rotation.x, transform.rotation.y, transform.rotation.z + z);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Angel, Time.deltaTime * 200);
        }

        //move to position
        if (moveToTarget && !death && !SoundAlarm && !targetHide)
        {
            if (!Alarm)
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
        //alarm path
        if (pointInt < pointRoutes.Length && Alarm && alarmPath)
        {
            currentTarget = pointRoutes[pointInt];
            lastTarget = currentTarget;
            pointInt += 1;
        }
        else if (pointInt == pointRoutes.Length && Alarm && alarmPath)
        {
            coveringTest = true;
        }

        yield return new WaitForSeconds(1f);
        testNextTarget = false;
    }
    //collision
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
    IEnumerator Alert() 
    {
        moveBlock = false;
        bool lastMove = moveToTarget;
        moveToTarget = false;
        if (targetInSight== true) 
        {
            currentTarget = aimTarget;
            SoundAlarm = false;
            moveToTarget = lastMove;
            StopCoroutine("Alert");
        }
        yield return new WaitForSeconds(3f);
        findEnemy = false;
        if (targetInSight) 
        {
            currentTarget = aimTarget;
            SoundAlarm = false;
            moveToTarget = lastMove;
        }
        if (!targetInSight) 
        {
            currentTarget = lastTarget;
            SoundAlarm = false;
            moveToTarget = lastMove;
        }
        if (moveBlock && !targetInSight) 
        {
            currentTarget = lastTarget;
            SoundAlarm = false;
            moveToTarget = lastMove;
        }
    }

    IEnumerator EnemyAlert() 
    {
        yield return new WaitForSeconds(2f);
        if (npcController.isDeath == false) 
        {
            sceneController.alarmOn = true;
        }
    }
}
