using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private UI_Manager UIManager;
    public float moveSpeed = 5f, aimMoveSpeed;
    private float setMoveSpeed;
    public Rigidbody2D characterBody;
    public Transform playerRotation;
    public float rotateSpeed = 20f;
    private bool changeWeapon;
    [HideInInspector]
    public bool canShoot;
    [HideInInspector]
    private bool isReload;
    public Transform spawnBullet;
    public Transform spawnBlood;
    public CircleCollider2D soundWave;


    public Rigidbody2D rifleBulletRB;
    private bool isRifleUsed;
    public float RifleWepFireRate = 1f;
    public int RifleBullets = 30;
    public int RifleBulletsStock = 120;
    private int RifleSetBullets;

    public Rigidbody2D pistolBulletRB;
    private bool isPistolUsed;
    public float PistolWepFireRate = 2f;
    public int PistolBullets = 14;
    public int PistolBulletsStock = 60;
    private int PistolSetBullets;

    private bool isSniperUsed;
    public Rigidbody2D sniperBulletRB;
    public float sniperWepFireRate = 4f;
    public int SniperBullets = 10;
    public int SniperBulletsStock = 30;
    private int SniperSetBullets;


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



    // Start is called before the first frame update
    void Start()
    {
        UIManager = GameObject.FindWithTag("GameController").GetComponent<UI_Manager>();
        autoReload = true;
        Damage = 0;
        setMoveSpeed = moveSpeed;

        RifleSetBullets = RifleBullets;
        PistolSetBullets = PistolBullets;
        SniperSetBullets = SniperBullets;

        canShoot = true;
        isPistolUsed = false;
        isRifleUsed = true;
        isSniperUsed = false;
        HP = 100;
        playerRotation = GetComponent<Transform>();
        characterBody = GetComponent<Rigidbody2D>();
        spawnBlood = transform.Find("Spawn_Blood");
    }

    // Update is called once per frame
    void Update()
    {
        if (HP < 1 && !isDeath) 
        {
            isDeath = true;
            moveSpeed = 0f;
            canShoot = false;
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
        if (RifleWepFireRate == 0)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0) && canShoot && isRifleUsed && RifleBullets > 0 && !isReload)
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
        else if (Input.GetKeyDown(KeyCode.Mouse0) && Time.time > timeToFire && canShoot && isPistolUsed && PistolBullets > 0 && !isReload)
        {
            timeToFire = Time.time + 1.5f / PistolWepFireRate;
            Shoot();
        }


        if (sniperWepFireRate == 0)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0) && isSniperUsed && SniperBullets > 0 && !isReload)
            {
                Shoot();
            }
        }
        else if (Input.GetKeyDown(KeyCode.Mouse0) && Time.time > timeToFire && canShoot && isSniperUsed && SniperBullets > 0 && !isReload)
        {
            timeToFire = Time.time + 1.5f / sniperWepFireRate;
            Shoot();
        }




        if (Input.GetKeyDown(KeyCode.Alpha1)) 
        {
            if (RifleBullets > 0)
                canShoot = true;
            UIManager.SniperSelector.enabled = false;
            UIManager.PistolSelector.enabled = false;
            UIManager.RifleSelector.enabled = true;
            isRifleUsed = true;
            isPistolUsed = false;
            isSniperUsed = false;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (PistolBullets > 0)
                canShoot = true;
            UIManager.SniperSelector.enabled = false;
            UIManager.PistolSelector.enabled = true;
            UIManager.RifleSelector.enabled = false;
            isPistolUsed = true;
            isRifleUsed = false;
            isSniperUsed = false;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            UIManager.SniperSelector.enabled = true;
            UIManager.PistolSelector.enabled = false;
            UIManager.RifleSelector.enabled = false;
            isSniperUsed = true;
            isPistolUsed = false;
            isRifleUsed = false;
        }

        if (Input.GetKeyDown(KeyCode.R)) 
        {
            Reload();

        }
        if (RifleBulletsStock > 0 && RifleBullets == 0 && autoReload) 
        {
            Reload();
        }
        if (PistolBulletsStock > 0 && PistolBullets == 0 && autoReload)
        {
            Reload();
        }
        if (SniperBulletsStock > 0 && SniperBullets == 0 && autoReload)
        {
            Reload();
        }
    }

    void FixedUpdate()
    {
        if (setMoveSpeed > aimMoveSpeed)
        {
            setMoveSpeed -= 3f;
        }
        if (setMoveSpeed < aimMoveSpeed)
        {
            setMoveSpeed = aimMoveSpeed;
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

    public void Shoot()
    {
        if (isRifleUsed) 
        {
            Rigidbody2D bullet = Instantiate(rifleBulletRB,spawnBullet.transform.position,spawnBullet.transform.rotation) as Rigidbody2D;
            bullet.GetComponent<BulletControll>().parentTransform = transform;
            bullet.GetComponent<BulletControll>().parentTag = transform.tag;
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
            soundWave.GetComponent<CircleCollider2D>().enabled = true;
            Rigidbody2D bullet = Instantiate(pistolBulletRB, spawnBullet.transform.position, spawnBullet.transform.rotation) as Rigidbody2D;
            bullet.GetComponent<BulletControll>().parentTransform = transform;
            bullet.GetComponent<BulletControll>().parentTag = transform.tag;
            PistolBullets--;
            soundWave.radius = 40;
            if (!soundWaves) 
            {
                soundWaves = true;
                soundWave.GetComponent<CircleCollider2D>().enabled = true;
                StartCoroutine("waitForEcho");
            }
        }

        if (isSniperUsed)
        {
            soundWave.GetComponent<CircleCollider2D>().enabled = true;
            Rigidbody2D bullet = Instantiate(sniperBulletRB, spawnBullet.transform.position, spawnBullet.transform.rotation) as Rigidbody2D;
            bullet.GetComponent<BulletControll>().parentTransform = transform;
            bullet.GetComponent<BulletControll>().parentTag = transform.tag;
            SniperBullets--;
            soundWave.radius = 40;
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
        transform.localScale = new Vector3(deathScale, deathScale, deathScale);
        float randomRotation = Random.Range(1f,360f);
        transform.localRotation = Quaternion.Euler(new Vector3(0, 0, randomRotation));
        float bloodRandomRotation = Random.Range(1f,360f);
        Instantiate(bloodObject,spawnBlood.position, Quaternion.Euler(new Vector3(0, 0, bloodRandomRotation)));
        UIManager.PlayerDeath();
    }
    void Reload() 
    {
        isReload = true;
        if (isRifleUsed) 
        {
            if (RifleBulletsStock >= RifleSetBullets)
            {
                canShoot = false;
                RifleBulletsStock -= RifleSetBullets - RifleBullets;
                StartCoroutine("waitForReload");
                RifleBullets = RifleSetBullets;
                canShoot = true;
                isReload = false;
            }
            else
            {
                canShoot = false;
                RifleBullets = RifleBulletsStock;
                StartCoroutine("waitForReload");
                RifleBulletsStock -= RifleBulletsStock;
                canShoot = true;
                isReload = false;
            }

            if (RifleBulletsStock <= 0)
            {
                RifleBulletsStock = 0;
                canShoot = false;
                isReload = false;
                UIManager.rifleImage.enabled = false;
            }
        }
        if (isPistolUsed) 
        {
            if (PistolBulletsStock >= PistolSetBullets)
            {
                canShoot = false;
                PistolBulletsStock -= PistolSetBullets - PistolBullets;
                StartCoroutine("waitForReload");
                PistolBullets = PistolSetBullets;
                canShoot = true;
                isReload = false;
            }
            else
            {
                canShoot = false;
                PistolBullets = PistolBulletsStock;
                StartCoroutine("waitForReload");
                PistolBulletsStock -= PistolBulletsStock;
                canShoot = true;
                isReload = false;
            }

            if (PistolBulletsStock <= 0)
            {
                PistolBulletsStock = 0;
                canShoot = false;
                isReload = false;
                UIManager.pistolImage.enabled = false;
            }
        }
        if (isSniperUsed) 
        {
            if (SniperBulletsStock >= SniperSetBullets)
            {
                canShoot = false;
                SniperBulletsStock -= SniperSetBullets - SniperBullets;
                StartCoroutine("waitForReload");
                SniperBullets = SniperSetBullets;
                canShoot = true;
                isReload = false;
            }
            else
            {
                canShoot = false;
                SniperBullets = SniperBulletsStock;
                StartCoroutine("waitForReload");
                SniperBulletsStock -= SniperBulletsStock;
                canShoot = true;
                isReload = false;
            }

            if (SniperBulletsStock <= 0)
            {
                SniperBulletsStock = 0;
                canShoot = false;
                isReload = false;
                UIManager.sniperImage.enabled = false;
            }
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
        transform.gameObject.GetComponent<CircleCollider2D>().enabled = false;
    }
    IEnumerator waitForReload() 
    {
        yield return new WaitForSeconds(1.5f);
    }
}
