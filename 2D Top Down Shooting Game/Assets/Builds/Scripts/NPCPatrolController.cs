using UnityEngine;
using System.Collections;
public class NPCPatrolController : MonoBehaviour
{
    private Rigidbody2D SoldierRigidBody;
    public Transform[] PointRouts;
    private int pointInt, waitFinde, rememberHP;
    public float moveSpeed = 120f, aimMoveSpeed = 80;
    private float setMoveSpeed;
    public float rotateSpeed = 250f;
    private Transform curentTarget, lastTarget;
    [HideInInspector]
    public Transform AimTarget;
    [HideInInspector]
    public bool targetInSight;
    [HideInInspector]
    public bool targetHide;
    [HideInInspector]
    public bool death = false;
    // [HideInInspector]
    public bool Alarm = false;
    public bool alarmPath = false;
    private float dist, distLastTarget, z, randomRotation;
    private bool moveToTarget, findEnemy, moveBlock, testNextTarget, coveringTest, SoundAlarm, setMove = false, bypass, enemyAlert;
    private Animator anim;

    private NPCController NPCcontroller;
    private Scene_Controller sceneControl;
    private GameObject[] WallPass;
    private GameObject SetGameobject;
    private NPCSighting NPCsighting;
    public LayerMask WallPassLayer, TargetCheckLayer;

    void Start()
    {
        waitFinde = 10;
        enemyAlert = false;
        coveringTest = false;
        SoundAlarm = false;
        testNextTarget = false;
        moveBlock = false;
        findEnemy = false;
        moveToTarget = true;
        targetInSight = false;
        if (curentTarget == null && !targetInSight && !coveringTest && PointRouts.Length != 0)
        {
            pointInt = 0;
            curentTarget = PointRouts[pointInt];
            pointInt += 1;
            lastTarget = curentTarget;
        }
        else if (PointRouts.Length == 0)
        {
            Debug.LogError("Enemy Soldier: Transform (point 1) not found. Please add one to the script");
        }
        if (PointRouts.Length == 1 || alarmPath)
        {
            coveringTest = true;
        }
        anim = gameObject.GetComponentInChildren<Animator>();
        SoldierRigidBody = GetComponent<Rigidbody2D>();
        NPCcontroller = GetComponentInChildren<NPCController>();
        NPCsighting = GetComponent<NPCSighting>();
        sceneControl = GameObject.FindWithTag("Respawn").GetComponent<Scene_Controller>();
        WallPass = GameObject.FindGameObjectsWithTag("WallPass");
        rememberHP = NPCcontroller.HP;
    }
    void Update()
    {
        //  check nulls for friendly warning
        if (PointRouts.Length == 0)
        {
            return;
        }
        if (sceneControl.alarmOn == true)
        {
            Alarm = true;
        }
        if (NPCcontroller.HP < rememberHP && !targetInSight && !findEnemy)
        {
            rememberHP = NPCcontroller.HP;
            // findEnemy = true;
            Alarm = true;
            // StartCoroutine("Alert");
        }
        if (curentTarget != null)
        {
            dist = Vector3.Distance(curentTarget.transform.position, transform.position);
        }
        if (lastTarget != null)
        {
            distLastTarget = Vector3.Distance(lastTarget.transform.position, transform.position);
        }
        if (Alarm && pointInt != PointRouts.Length && alarmPath)
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
        if (!targetInSight && curentTarget == AimTarget && !moveBlock && !alarmPath)
        {
            curentTarget = lastTarget;
            lastTarget = curentTarget;
            moveToTarget = true;
        }
        if (!targetInSight && curentTarget == AimTarget && !moveBlock && alarmPath)
        {
            coveringTest = true;
            curentTarget = lastTarget;
            lastTarget = curentTarget;
            moveToTarget = true;
        }
        if (targetInSight && !moveBlock)
        {
            StopCoroutine("NextTarget");
            StopCoroutine("Alert");
            SoundAlarm = false;
            curentTarget = AimTarget;
            findEnemy = false;
            if (!enemyAlert)
            {
                enemyAlert = true;
                StartCoroutine("EnemyAlert");
            }
        }
        if (dist <= 30f && targetInSight && curentTarget == AimTarget)
        {
            moveToTarget = false;
        }
        if (dist > 31f && targetInSight && curentTarget == AimTarget)
        {
            moveToTarget = true;
        }
        if (moveToTarget && !targetHide)
        {
            setMove = true;
            if (NPCcontroller.Aim == false)
            {
                anim.SetFloat("Speed", 1f);
            }
            else
            {
                anim.SetFloat("Speed", 0.6f);
            }
        }
        else
        {
            setMove = false;
        }
    }
    void FixedUpdate()
    {
        //  check nulls for friendly warning
        if (PointRouts.Length == 0)
        {
            return;
        }
        if (NPCcontroller.Aim == false)
        {
            if (setMoveSpeed < moveSpeed)
                setMoveSpeed += 3f;
            if (setMoveSpeed > moveSpeed)
                setMoveSpeed = moveSpeed;
        }
        else
        {
            if (setMoveSpeed > aimMoveSpeed)
                setMoveSpeed -= 3f;
            if (setMoveSpeed < aimMoveSpeed)
                setMoveSpeed = aimMoveSpeed;
        }
        if (!findEnemy && !death && !targetHide && !SoundAlarm)
        {
            if (!bypass)
            {
                z = Mathf.Atan2((curentTarget.transform.position.y - transform.position.y), (curentTarget.transform.position.x - transform.position.x)) * Mathf.Rad2Deg - 90;
                Quaternion Angel = Quaternion.Euler(transform.rotation.x, transform.rotation.y, transform.rotation.z + z);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, Angel, Time.deltaTime * rotateSpeed);
            }
            else
            {
                Quaternion Angel = Quaternion.Euler(transform.rotation.x, transform.rotation.y, randomRotation);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, Angel, Time.deltaTime * 200);
            }

        }
        if (findEnemy && !targetInSight && !death && !SoundAlarm)
        {
            if (waitFinde > 100)
            {
                waitFinde = 0;
                randomRotation = Random.Range(0, 360);
            }
            waitFinde++;
            if (!Alarm)
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
        if (!targetInSight && !death && SoundAlarm && !targetHide)
        {
            z = Mathf.Atan2((curentTarget.transform.position.y - transform.position.y), (curentTarget.transform.position.x - transform.position.x)) * Mathf.Rad2Deg - 90;
            Quaternion Angel = Quaternion.Euler(transform.rotation.x, transform.rotation.y, transform.rotation.z + z);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Angel, Time.deltaTime * 200);
        }
        if (moveToTarget && !death && !SoundAlarm && !targetHide)
        {
            if (!Alarm)
                SoldierRigidBody.AddForce(gameObject.transform.up * moveSpeed);
            else
                SoldierRigidBody.AddForce(gameObject.transform.up * moveSpeed * 2);
        }
        anim.SetBool("Move", setMove);

    }
    IEnumerator NextTarget()
    {
        if (pointInt < PointRouts.Length && !alarmPath)
        {
            curentTarget = PointRouts[pointInt];
            lastTarget = curentTarget;
            pointInt += 1;
        }
        else if (pointInt == PointRouts.Length && !alarmPath)
        {
            pointInt = 0;
            curentTarget = PointRouts[pointInt];
            lastTarget = curentTarget;
        }
        if (pointInt < PointRouts.Length && Alarm && alarmPath)
        {
            curentTarget = PointRouts[pointInt];
            lastTarget = curentTarget;
            pointInt += 1;
        }
        else if (pointInt == PointRouts.Length && Alarm && alarmPath)
        {
            coveringTest = true;
        }

        yield return new WaitForSeconds(1f);
        testNextTarget = false;
    }
    void OnCollisionEnter2D(Collision2D coll)
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
                        curentTarget = Enter1.transform;
                    if (distEnter1 > distEnter2)
                        curentTarget = Enter2.transform;
                }
                else if (chiSet == 2 && coll.gameObject.transform.Find("Enter_point2") == null)
                {
                    GameObject Enter1 = (coll.gameObject.transform.Find("Enter_point1").gameObject);
                    curentTarget = Enter1.transform;
                }
                if (chiSet == 1)
                {
                    GameObject Enter1 = (coll.gameObject.transform.Find("Enter_point1").gameObject);
                    curentTarget = Enter1.transform;
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
                            SetGameobject = Pass;
                        }
                    }
                }
                if (SetGameobject != null)
                {
                    curentTarget = SetGameobject.transform;
                    SetGameobject = null;
                    StartCoroutine("enterWall");
                }
            }
        }
    }
    void OnTriggerStay2D(Collider2D trig)
    {
        if (trig.gameObject != null && trig.gameObject.tag == "Sound wave" && !targetInSight && !SoundAlarm)
        {
            Collider2D targetCollider = Physics2D.OverlapCircle(transform.position, Mathf.Infinity, TargetCheckLayer.value);
            if (targetCollider != null && targetCollider.gameObject.tag == NPCsighting.targetTag)
            {
                Vector2 direction = targetCollider.transform.position - transform.position;
                RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, Mathf.Infinity, NPCsighting.TargetLayer);

                if (hit.collider != null && hit.collider.gameObject.tag == NPCsighting.targetTag)
                {
                    curentTarget = hit.collider.transform;
                    AimTarget = curentTarget;
                    targetInSight = true;
                    Alarm = true;
                }
            }
            else if (targetCollider == null && !targetInSight)
            {

                StopCoroutine("ChangeTarget");
                StopCoroutine("enterWall");
                SoundAlarm = true;
                curentTarget = trig.transform;
                StartCoroutine("Alert");
            }
        }
    }
    IEnumerator ChangeTarget()
    {
        yield return new WaitForSeconds(0.6f);
        bypass = false;
        if (targetInSight)
        {
            curentTarget = AimTarget;
            moveBlock = false;
        }
        if (!targetInSight)
        {
            curentTarget = lastTarget;
            moveBlock = false;
        }

    }
    IEnumerator enterWall()
    {
        yield return new WaitForSeconds(1f);

        if (targetInSight)
        {
            curentTarget = AimTarget;
            moveBlock = false;
        }
        if (!targetInSight)
        {
            curentTarget = lastTarget;
            moveBlock = false;
        }
    }
    IEnumerator Alert()
    {
        moveBlock = false;
        bool lastSetMove = moveToTarget;
        moveToTarget = false;
        if (targetInSight == true)
        {
            curentTarget = AimTarget;
            SoundAlarm = false;
            moveToTarget = lastSetMove;
            StopCoroutine("Alert");
        }
        yield return new WaitForSeconds(3f);
        findEnemy = false;
        if (NPCcontroller.DeathTest == false)
        {
            sceneControl.alarmOn = true;
        }
        if (targetInSight)
        {
            curentTarget = AimTarget;
            SoundAlarm = false;
            moveToTarget = lastSetMove;
        }
        if (!targetInSight)
        {
            curentTarget = lastTarget;
            SoundAlarm = false;
            moveToTarget = lastSetMove;
        }

        if (moveBlock && !targetInSight)
        {
            curentTarget = lastTarget;
            SoundAlarm = false;
            moveToTarget = lastSetMove;
        }
    }
    IEnumerator EnemyAlert()
    {
        yield return new WaitForSeconds(2f);
        if (NPCcontroller.DeathTest == false)
        {
            sceneControl.alarmOn = true;
        }
    }
}
