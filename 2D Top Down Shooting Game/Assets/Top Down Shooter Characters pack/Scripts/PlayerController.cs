using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    private float setMoveSpeed;
    public Rigidbody2D characterBody;
    public Transform playerRotation;
    public float rotateSpeed = 20f;
    private bool setMove = false, setBuckMove = false;
    Vector2 move;
    //private Animator animate;
    public GameObject mousePointer;

    private bool changeWeapon;
    private bool isShoot;
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
    public float rifleFireRate = 0f;
    public int rifleAmmo = 30;
    public int rifleBulletStock = 90;
    private int setRifleBullets;

    public Rigidbody2D pistolBulletRB;
    private bool isPistolUsed;
    public AudioClip pistolFireClip;
    public AudioClip pistolReloadClip;
    public float pistolFireRate = 0f;
    public int pistolAmmo = 15;
    public int pistolBulletStock = 30;
    private int setPistolBullets;

    //sniper coming soon
    private bool isSniperUsed;
    private bool aim = false;

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

    public LineRenderer sniperAimLine;
    public LayerMask aimLineLayer, footStepLayer;

    // Start is called before the first frame update
    void Start()
    {
        Damage = 0;
        setPistolBullets = rifleAmmo;
        setPistolBullets = pistolAmmo;

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
            moveSpeed = 0;
            isShoot = false;
            //call death method
        }
        if (Damage > 0) 
        {
            float randomX = Random.Range(-3,3);
            float randomY = Random.Range(-3,3);
            Vector3 spawnBloodVec = new Vector3(spawnBlood.position.x+randomX,spawnBlood.position.y+randomY,spawnBlood.position.z);
            HP -= Damage;
            Damage = 0;
        }
        if (Input.GetKeyDown(KeyCode.Mouse1) && isSniperUsed) 
        {
            Aim();
        }
        if (rifleFireRate == 0)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0) && isShoot && isRifleUsed && rifleAmmo > 0 && !isReload)
            {
                Shoot();
            }

        }
        else if (Input.GetKeyDown(KeyCode.Mouse0) && Time.time > timeToFire && isRifleUsed && rifleAmmo > 0 && !isReload) 
        {
            timeToFire = Time.time + 1 / rifleFireRate;
            Shoot();
        }
        if (pistolFireRate == 0) 
        {
            if (Input.GetKeyDown(KeyCode.Mouse0) && isPistolUsed && pistolAmmo > 0 && !isReload)
            {
                Shoot();
            }
        }
        else if (Input.GetKeyDown(KeyCode.Mouse0) && Time.time > timeToFire && isShoot && isPistolUsed && pistolAmmo > 0 && !isReload)
        {
            timeToFire = Time.time + 1 / pistolFireRate;
            Shoot();
        }
        if (Input.GetKeyDown(KeyCode.Alpha1)) 
        {
            isRifleUsed = true;
            isPistolUsed = false;
            isSniperUsed = false;
            Debug.Log("Rifle is ussed");

        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            isPistolUsed = true;
            isRifleUsed = false;
            isSniperUsed = false;
            Debug.Log("Pistol is used");
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            isSniperUsed = true;
            //isPistolUsed = false;
            //isRifleUsed = false;
            Debug.Log("Sniper is used");
        }

        if (Input.GetKeyDown(KeyCode.R)) 
        {
            if (isRifleUsed && rifleBulletStock > 0) 
            {
                //reload
                Debug.Log("Rifle Reloading");
            }
            if (isPistolUsed && pistolBulletStock > 0)
            {
                //reload
                Debug.Log("Pistol Reloading");
            }
            if (isSniperUsed && pistolBulletStock > 0)
            {
                //reload
                Debug.Log("Pistol Reloading");
            }

            //similar to sniper
        }
        if (rifleBulletStock > 0 && rifleAmmo == 0 && autoReload) 
        {
            //reload
        }
        if (pistolBulletStock > 0 && pistolAmmo == 0 && autoReload)
        {
            //reload
        }
        //similar to sniper

        if (aim && !isReload && !isDeath && isSniperUsed)
        {
            RaycastHit2D hit = Physics2D.Raycast(spawnBullet.position, spawnBullet.right, Mathf.Infinity, aimLineLayer.value);
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
        /*        if (!aim)
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
                }*/
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
        mousePos.z = transform.position.z;
        mousePointer.transform.position = mousePos;
        Vector3 player_pos = Camera.main.WorldToScreenPoint(playerRotation.transform.position);
        mouse_pos.x = mouse_pos.x - player_pos.x;
        mouse_pos.y = mouse_pos.y - player_pos.y;
        float angle = Mathf.Atan2(mouse_pos.y, mouse_pos.x) * Mathf.Rad2Deg;
        playerRotation.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(new Vector3(0, 0, angle)), rotateSpeed);
    }

    public void Aim() 
    {
        aim = !aim;
        if(aim)
        Debug.Log("Aiming");
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
            Rigidbody2D bullet = Instantiate(rifleBulletRB,spawnBullet.transform.position,spawnBullet.transform.rotation) as Rigidbody2D;
            //bullet.GetComponent<BulletController>().parentTransform = transform.parent.transform;
            //bullet.GetComponent<BulletController>().parentTag = transform.parent.tag;
            rifleAmmo--;
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
            //bullet.GetComponent<BulletController>().parentTransform = transform.parent.transform;
            //bullet.GetComponent<BulletController>().parentTag = transform.parent.tag;
            pistolAmmo--;
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
}
