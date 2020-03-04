using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Rigidbody2D characterBody;
    public Transform playerRotation;
    public float rotateSpeed = 20f;
    private bool setMove = false, setBuckMove = false;
    Vector2 move;
    private Animator animate;
    public GameObject mousePointer;

    private bool changeWeapon;
    private bool isShoot;
    private bool isReload;

    public AudioClip grassFootStep, woodenFootStep, gravelFootStep;
    public AudioClip changeWeaponClip;

    public Transform spawnBullet;
    public Transform spawnBlood;
    public CircleCollider2D soundWave;


    public Rigidbody2D rifleBullet;
    public AudioClip rifleFireClip;
    public AudioClip rifleReloadClip;
    private bool isRifleUsed;
    public GameObject rifleFlash1, rifleFlash2;
    public float rifleFireRate = 0f;
    public int rifleBullets = 30;
    public int rifleBulletStock = 90;
    private int setRifleBullets;

    public Rigidbody2D pistolBullet;
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

    private Component[] spriteRenders;
    private LineRenderer sniperAimLine;
    public LayerMask aimLineLayer, footStepLayer;

    // Start is called before the first frame update
    void Start()
    {
        Damage = 0;
        setPistolBullets = rifleBullets;
        setPistolBullets = pistolAmmo;

        isShoot = true;
        isPistolUsed = false;
        isRifleUsed = true;
        isSniperUsed = false;
        HP = 100;
        playerRotation = GetComponent<Transform>();
        characterBody = GetComponentInParent<Rigidbody2D>();
        spawnBlood = transform.Find("Spawn_Blood");

        spriteRenders = GetComponentsInChildren<SpriteRenderer>();
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
        if (Input.GetKeyDown(KeyCode.Mouse1)) 
        {
            Aim();
        }
        if (rifleFireRate == 0)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0) && isShoot && isRifleUsed && rifleBullets > 0 && !isReload)
            {
                Shoot();
            }

        }
        else if (Input.GetKeyDown(KeyCode.Mouse0) && Time.time > timeToFire && isRifleUsed && rifleBullets > 0 && !isReload) 
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
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            isPistolUsed = true;
        }
        if (Input.GetKeyDown(KeyCode.R)) 
        {
            if (isRifleUsed && rifleBulletStock > 0) 
            {
                //reload
            }
            if (isPistolUsed && pistolBulletStock > 0)
            {
                //reload
            }
            //similar to sniper
        }
        if (rifleBulletStock > 0 && rifleBullets == 0 && autoReload) 
        {
            //reload
        }
        if (pistolBulletStock > 0 && pistolAmmo == 0 && autoReload)
        {
            //reload
        }
        //similar to sniper

        //aim for sniper, coming soon
/*        if (aim && !isReload && !isDeath)
        {
            RaycastHit2D hit = Physics2D.Raycast(spawnBullet.position, spawnBullet.right, Mathf.Infinity, aimLineLayer.value);
            sniperAimLine.enabled = true;
            sniperAimLine.SetPosition(0, spawnBullet.position);
            sniperAimLine.SetPosition(1, hit.point);
        }
        else
        {
            sniperAimLine.enabled = false;
        }*/


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


            if (Input.GetAxis("Horizontal") > 0 || Input.GetAxis("Vertical") > 0)
            {
                setMove = true;
                if (!aim)
                {
                    //animate.SetFloat("Speed", 1f);
                }
                else
                {
                    //animate.SetFloat("Speed", 0.6f);
                }

            }
            else if (Input.GetAxis("Horizontal") < 0 || Input.GetAxis("Vertical") < 0)
            {
                setBuckMove = true;
                if (!aim)
                {
                    //animate.SetFloat("Speed", -1f);
                }
                else
                {
                    //animate.SetFloat("Speed", -0.6f);
                }
            }
            else if (Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") == 0)
            {
                setMove = false;
                setBuckMove = false;
            }

            //animate.SetBool("Move", setMove);
            //animate.SetBool("MoveBuck", setBuckMove);

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
    public void weaponChanged() 
    {
        if (!aim)
        {
            animate.SetTrigger("Change");
        }
        else if (aim) 
        {
            animate.SetTrigger("Aim");
            animate.SetTrigger("Change");
            animate.SetTrigger("Change_to_Aim");
        }
    }
    public void Shoot()
    {
        if (isRifleUsed) 
        {
            //animate.SetTrigger("Aim");
            //animate.SetTrigger("Shoot");
            Rigidbody2D bullet = Instantiate(rifleBullet,spawnBullet.transform.position,spawnBullet.transform.rotation) as Rigidbody2D;
            bullet.GetComponent<BulletController>().parentTransform = transform.parent.transform;
            bullet.GetComponent<BulletController>().parentTag = transform.parent.tag;
            rifleBullets--;
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
            animate.SetTrigger("Aim");
            animate.SetTrigger("shoot");
            soundWave.GetComponent<CircleCollider2D>().enabled = true;
            Rigidbody2D bullet = Instantiate(pistolBullet, spawnBullet.transform.position, spawnBullet.transform.rotation) as Rigidbody2D;
            bullet.GetComponent<BulletController>().parentTransform = transform.parent.transform;
            bullet.GetComponent<BulletController>().parentTag = transform.parent.tag;
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
