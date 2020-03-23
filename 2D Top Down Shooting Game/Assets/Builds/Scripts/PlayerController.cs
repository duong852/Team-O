using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f, aimMoveSpeed;
    private float setMoveSpeed;
    public Rigidbody2D characterBody;
    public Transform playerRotation;
    public float rotateSpeed = 20f;
    //private Animator animate;
    private bool changeWeapon;
    public bool isShoot;
    private bool isReload;

    public AudioClip grassFootStep, woodenFootStep, gravelFootStep;
    public AudioClip changeWeaponClip;

    public Transform spawnBullet;
    public Transform spawnBlood;
    public CircleCollider2D soundWave;


    public Rigidbody2D rifleBulletRB;
    public AudioClip rifleFireClip;
    public AudioClip rifleReloadClip;
    private bool isRifleUsed;
    public float RifleWepFireRate = 1f;
    public int RifleBullets = 30;
    public int RifleBulletsStock = 90;
    private int RifleSetBullets;

    public Rigidbody2D pistolBulletRB;
    private bool isPistolUsed;
    public AudioClip pistolFireClip;
    public AudioClip pistolReloadClip;
    public float PistolWepFireRate = 2f;
    public int PistolBullets = 12;
    public int PistolBulletsStock = 90;
    private int PistolSetBullets;

    //sniper coming soon
    private bool isSniperUsed;
    private bool aim = false;
/*    public Rigidbody2D sniperBulletRB;
    public AudioClip sniperFireClip;
    public AudioClip sniperReloadClip;
    public float sniperWepFireRate = 5f;
    public int sniperBullets = 12;
    public int sniperBulletsStock = 90;
    private int sniperSetBullets;*/


    public bool autoReload = false;
    float timeToFire = 0f;
    public int HP;
    [Range(0.5f, 1.5f)]
    public float deathScale;

    public Transform hitBlood;
    public Transform bloodObject;
    private bool soundWaves = false;

    [HideInInspector]
    public int Damage;
    [HideInInspector]
    public bool isDeath = false;

    private LineRenderer sniperAimLine;
    public LayerMask aimLineLayer, footStepLayer;

    // Start is called before the first frame update
    void Start()
    {
        autoReload = true;
        Damage = 0;
        setMoveSpeed = moveSpeed;
        RifleSetBullets = RifleBullets;
        PistolSetBullets = PistolBullets;
        aim = false;
        isShoot = true;
        isPistolUsed = false;
        isRifleUsed = true;
        isSniperUsed = false;
        HP = 100;
        playerRotation = GetComponent<Transform>();
        characterBody = GetComponentInParent<Rigidbody2D>();
        spawnBlood = transform.Find("Spawn_Blood");
        sniperAimLine = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (HP < 1 && !isDeath) 
        {
            isDeath = true;
            moveSpeed = 0f;
            isShoot = false;
            Death();
        }
        if (Damage > 0) 
        {
            float randomX = Random.Range(-3,3);
            float randomY = Random.Range(-3,3);
            Vector3 spawnBloodVec = new Vector3(spawnBlood.position.x+randomX,spawnBlood.position.y+randomY,spawnBlood.position.z);
            Instantiate(hitBlood, spawnBloodVec, transform.rotation);
            HP -= Damage;
            Damage = 0;
        }
        if (Input.GetKeyDown(KeyCode.Mouse1) && isSniperUsed) 
        {
            Aim();
        }
        if (RifleWepFireRate == 0)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0) && isShoot && isRifleUsed && RifleBullets > 0 && !isReload)
            {
                Shoot();
            }

        }
        else if (Input.GetKeyDown(KeyCode.Mouse0) && Time.time > timeToFire && isRifleUsed && RifleBullets > 0 && !isReload) 
        {
            timeToFire = Time.time + 1 / RifleWepFireRate;
            Shoot();
        }
        if (PistolWepFireRate == 0) 
        {
            if (Input.GetKeyDown(KeyCode.Mouse0) && isPistolUsed && PistolBullets > 0 && !isReload)
            {
                Shoot();
            }
        }
        else if (Input.GetKeyDown(KeyCode.Mouse0) && Time.time > timeToFire && isShoot && isPistolUsed && PistolBullets > 0 && !isReload)
        {
            timeToFire = Time.time + 1.5f / PistolWepFireRate;
            Shoot();
        }
        if (Input.GetKeyDown(KeyCode.Alpha1)) 
        {
            isRifleUsed = true;
            isPistolUsed = false;
            isSniperUsed = false;
            sniperAimLine.enabled = false;
            aim = false;
            Debug.Log("Rifle is used");

        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            isPistolUsed = true;
            isRifleUsed = false;
            isSniperUsed = false;
            sniperAimLine.enabled = false;
            aim = false;
            Debug.Log("Pistol is used");
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            isSniperUsed = true;
            isPistolUsed = false;
            isRifleUsed = false;
            Debug.Log("Warning: sniper can not shoot");
            Debug.Log("Sniper is used");
        }

        if (Input.GetKeyDown(KeyCode.R)) 
        {
            Reload();
            sniperAimLine.enabled = false;
        }
        if (RifleBulletsStock > 0 && RifleBullets == 0 && autoReload) 
        {
            Reload();
            sniperAimLine.enabled = false;
        }
        if (PistolBulletsStock > 0 && PistolBullets == 0 && autoReload)
        {
            Reload();
            sniperAimLine.enabled = false;
        }
        //do similar for sniper

        //laser aim line
        if (aim && !isReload && !isDeath && isSniperUsed)
        {
            RaycastHit2D hit = Physics2D.Raycast(spawnBullet.position, spawnBullet.right, Mathf.Infinity, aimLineLayer.value);
            //Debug.DrawLine(spawnBullet.position, hit.point);
            sniperAimLine.enabled = true;
            sniperAimLine.SetPosition(0, spawnBullet.position);
            sniperAimLine.SetPosition(1, hit.point);
        }
        else
        {
            sniperAimLine.enabled = false;
        }
    }

    void FixedUpdate()
    {
        if (!aim)
        {
            if (setMoveSpeed < moveSpeed)
            {
                setMoveSpeed += 3f;
            }
            if (setMoveSpeed > moveSpeed)
            {
                setMoveSpeed = moveSpeed;
            }
        }
        else
        {
            if (setMoveSpeed > aimMoveSpeed)
            {
                setMoveSpeed -= 3f;
            }
            if (setMoveSpeed < aimMoveSpeed)
            {
                setMoveSpeed = aimMoveSpeed;
            }
        }
        if (!isDeath) 
        {
            faceCursor();
        }
        if (!isDeath) 
        {
            Vector2 input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            characterBody.AddForce(input * moveSpeed);
        }
    }

    void faceCursor()
    {
        Vector3 mouse_pos = Input.mousePosition;
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 player_pos = Camera.main.WorldToScreenPoint(playerRotation.transform.position);
        mouse_pos.x = mouse_pos.x - player_pos.x;
        mouse_pos.y = mouse_pos.y - player_pos.y;
        float angle = Mathf.Atan2(mouse_pos.y, mouse_pos.x) * Mathf.Rad2Deg;
        playerRotation.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(new Vector3(0, 0, angle)), rotateSpeed);
    }

    public void Aim() 
    {
        aim = !aim;
    }

    public void ChangeWeapon() 
    {
        changeWeapon = !changeWeapon;
        AudioSource.PlayClipAtPoint(changeWeaponClip, transform.position);
    }
    public void footSound() 
    {
        Collider2D footStep = Physics2D.OverlapPoint(transform.position,footStepLayer.value);
        if(footStep != null && footStep.gameObject.tag == "Floor")
        {
            AudioSource.PlayClipAtPoint(woodenFootStep, transform.position);
        }
        if (footStep != null && footStep.gameObject.tag == "Gravel")
        {
            AudioSource.PlayClipAtPoint(gravelFootStep, transform.position);
        }
        if (footStep != null && footStep.gameObject.tag == "Grass")
        {
            AudioSource.PlayClipAtPoint(grassFootStep, transform.position);
        }
    }
    public void Shoot()
    {
        if (isRifleUsed) 
        {
            Debug.Log("Rifle Bullets Instantiate");
            Rigidbody2D bullet = Instantiate(rifleBulletRB,spawnBullet.transform.position,spawnBullet.transform.rotation) as Rigidbody2D;
            //bullet.GetComponent<BulletController>().parentTransform = transform.parent.transform;
            //bullet.GetComponent<BulletController>().parentTag = transform.parent.tag;
            RifleBullets--;
            soundWave.radius = 60;
            if (!soundWaves) 
            {
                soundWaves = true;
                soundWave.GetComponent<CircleCollider2D>().enabled = true;
                StartCoroutine("waitForEcho");
            }
        }
        if (isPistolUsed) 
        {
            Debug.Log("Pistol Bullets Instantiate");
            soundWave.GetComponent<CircleCollider2D>().enabled = true;
            Rigidbody2D bullet = Instantiate(pistolBulletRB, spawnBullet.transform.position, spawnBullet.transform.rotation) as Rigidbody2D;
            //bullet.GetComponent<BulletController>().parentTransform = transform.parent.transform;
            //bullet.GetComponent<BulletController>().parentTag = transform.parent.tag;
            PistolBullets--;
            soundWave.radius = 40;
            AudioSource.PlayClipAtPoint(pistolFireClip,transform.position);
            if (!soundWaves) 
            {
                soundWaves = true;
                soundWave.GetComponent<CircleCollider2D>().enabled = true;
                StartCoroutine("waitForEcho");
            }
        }
    }
    void Death() 
    {
        //call ui controller and respawn
        StartCoroutine("waitForDeath");
        sniperAimLine.enabled = false;
        transform.localScale = new Vector3(deathScale, deathScale, deathScale);
        float randomRotation = Random.Range(1f,360f);
        transform.localRotation = Quaternion.Euler(new Vector3(0, 0, randomRotation));
        float bloodRandomRotation = Random.Range(1f,360f);
        Instantiate(bloodObject,spawnBlood.position, Quaternion.Euler(new Vector3(0, 0, bloodRandomRotation)));
    }
    void Reload() 
    {
        isReload = true;
        if (isRifleUsed) 
        {
            if (RifleBulletsStock >= RifleSetBullets)
            {
                isShoot = false;
                RifleBulletsStock -= RifleSetBullets - RifleBullets;
                StartCoroutine("waitForReload");
                Debug.Log("Rifle Reload Done");
                RifleBullets = RifleSetBullets;
                isShoot = true;
                isReload = false;
            }
            else
            {
                isShoot = false;
                RifleBullets = RifleBulletsStock;
                StartCoroutine("waitForReload");
                Debug.Log("Rifle Reload Done");
                RifleBulletsStock -= RifleBulletsStock;
                isShoot = true;
                isReload = false;
            }

            if (RifleBulletsStock <= 0)
            {
                RifleBulletsStock = 0;
                isShoot = false;
                isReload = false;
            }
        }
        if (isPistolUsed) 
        {
            if (PistolBulletsStock >= PistolSetBullets)
            {
                isShoot = false;
                PistolBulletsStock -= PistolSetBullets - PistolBullets;
                StartCoroutine("waitForReload");
                Debug.Log("Pistol Reload Done");
                PistolBullets = PistolSetBullets;
                isShoot = true;
                isReload = false;
            }
            else
            {
                isShoot = false;
                PistolBullets = PistolBulletsStock;
                StartCoroutine("waitForReload");
                Debug.Log("Pistol Reload Done");
                PistolBulletsStock -= PistolBulletsStock;
                isShoot = true;
                isReload = false;
            }

            if (PistolBulletsStock <= 0)
            {
                PistolBulletsStock = 0;
                isShoot = false;
                isReload = false;
            }
        }
        if (isSniperUsed) 
        {
            Debug.Log("reload Sniper");
        }
    }
    IEnumerator waitForEcho() 
    {
        yield return new WaitForSeconds(0.1f);
        soundWave.GetComponent<CircleCollider2D>().enabled = false;
        soundWaves = false;
    }
    IEnumerator waitForDeath() 
    {
        yield return new WaitForSeconds(3f);
        transform.parent.gameObject.GetComponent<CircleCollider2D>().enabled = false;
    }
    IEnumerator waitForReload() 
    {
        yield return new WaitForSeconds(1.5f);
    }
}
