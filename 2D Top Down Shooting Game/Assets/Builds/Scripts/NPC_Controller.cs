using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Controller : MonoBehaviour
{
    //public bool isPistol, isRifle, isSniper;
    private bool reload;
    private bool canShoot;
    [HideInInspector]
    public bool isAim;
    public Transform spawnBullet;
    private CircleCollider2D sound_Wave;
    public AudioClip onGrassAudioClip, onWoodenAudioClip, onGravelAudioClip;
    float timeToFire = 0f;
    Vector2 spawnBulletPosition;
    public Transform hitBloodObject;
    public Transform bloodObject;

    //entity for rifle weapon
    public bool isRifle = true;
    public Rigidbody2D rifleBulletRB;
    public AudioClip rifleFireAudioClip;
    public AudioClip rifleReloadAudioClip;
    public float rifleFiteRate = 0f;
    public int rifleBullets = 30;
    private int setRifleBullets;

    //entity for pistol weapon
    public bool isPistol = false;
    public AudioClip pistolFireAudioClip;
    public AudioClip pistolReloadAudioClip;
    public Rigidbody2D pistolBulletRB;
    public float pistolFireRate;
    public int pistolBullets = 12;
    private int setPistolBullets;

    //entity for sniper weapon
    //public bool isSniper = false;
    //public AudioClip sniperFireAudioClip;
    //public AudioClip sniperReloadAudioClip;
    //public Rigidbody2D sniperBulletRB;
    //public float sniperFireRate;
    //public int sniperBullets = 10;
    //private int setsniperBullets;

    private Animator animator;
    [HideInInspector]
    public bool targetInSight;

    public int HP = 100;
    [Range(0.5f, 1.5f)]
    public float deathScale;

    [HideInInspector]
    public int Damage;
    [HideInInspector]
    public bool isDeath = false;
    private bool soundWave = false;
    public int setScore = 1;
    public LayerMask footStepSoundLayer;
    private Transform spawnBlood;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        setRifleBullets = rifleBullets;
        setPistolBullets = rifleBullets;
        //setSniperBullets = sniperBullets;
        Damage = 0;
        sound_Wave = transform.Find("Sound_wave").GetComponent<CircleCollider2D>();
        spawnBlood = transform.Find("Spawn_Blood");
        
    }

    // Update is called once per frame
    void Update()
    {
        if (HP <= 0 && isDeath) 
        {
            isDeath = true;
            canShoot = false;
            GameObject UI_Menu = GameObject.FindWithTag("GameController");
            if (UI_Menu != null) 
            {
                UI_Menu.GetComponent<UI_Manager>().score += setScore;
            }
            Death();
        }
        if (Damage > 0) 
        {
            float randomX = Random.Range(-3, 3);
            float randomY = Random.Range(-3,3);
            Vector3 spawnBloodVector = new Vector3(spawnBlood.position.x + randomX, spawnBlood.position.y + randomY, spawnBlood.position.y);
            Instantiate(hitBloodObject, spawnBloodVector, transform.rotation);
            HP -= Damage;
            Damage = 0;
        }
        if (rifleBullets == 0 && !reload && isRifle) 
        {
            reloadWeapon();
        }
        if (pistolBullets == 0 && !reload && isPistol)
        {
            reloadWeapon();
        }
        //if (sniperBullets == 0 && !isAim && !reload && isSniper)
        //{
        //    reloadWeapon();
        //}
    }
    private void FixedUpdate()
    {
        if (rifleFiteRate == 0)
        {
            if (canShoot && isRifle && rifleBullets > 0 && targetInSight)
                shoot();
        }
        else if (Time.time > timeToFire && canShoot && isRifle && rifleBullets > 0 && targetInSight && !reload) 
        {
            timeToFire = Time.time + 1 / rifleFiteRate;
            shoot();
        }

        if (pistolFireRate == 0)
        {
            if (canShoot && isPistol && pistolBullets > 0 && targetInSight)
                shoot();
        }
        else if (Time.time > timeToFire && canShoot && isPistol && pistolBullets > 0 && targetInSight && !reload)
        {
            timeToFire = Time.time + 1 / pistolFireRate;
            shoot();
        }

        //if (sniperFiteRate == 0)
        //{
        //    if (canShoot && isSniper && sniperBullets > 0 && targetInSight)
        //        shoot();
        //}
        //else if (Time.time > timeToFire && canShoot && isSniper && sniperBullets > 0 && targetInSight && !reload)
        //{
        //    timeToFire = Time.time + 1 / sniperFireRate;
        //    shoot();
        //}
    }

    void aimON() 
    {
        isAim = true;
    }
    void aimOFF()
    {
        isAim = false;
    }
    void shootON() 
    {
        canShoot = true;
    }
    void shootOFF()
    {
        canShoot = false;
    }
    void footStep() 
    {
        Collider2D overLapHit = Physics2D.OverlapPoint(transform.position, footStepSoundLayer.value);
        if (overLapHit != null && overLapHit.gameObject.tag == ("Floor")) 
        {
            AudioSource.PlayClipAtPoint(onWoodenAudioClip, transform.position);
        }
        if (overLapHit != null && overLapHit.gameObject.tag == ("Gravel"))
        {
            AudioSource.PlayClipAtPoint(onGravelAudioClip, transform.position);
        }
        if (overLapHit != null && overLapHit.gameObject.tag == ("Grass"))
        {
            AudioSource.PlayClipAtPoint(onGrassAudioClip, transform.position);
        }
    }
    void shoot() 
    {
        if (isRifle) 
        {
            Rigidbody2D Bullet = Instantiate(rifleBulletRB, spawnBullet.transform.position, spawnBullet.transform.rotation) as Rigidbody2D;
            Bullet.GetComponent<BulletController>().parentTransform = transform.parent.transform;
            Bullet.GetComponent<BulletController>().parentTag = transform.parent.tag;
            rifleBullets--;
            sound_Wave.radius = 60;
            AudioSource.PlayClipAtPoint(rifleFireAudioClip,transform.position);
            if (!soundWave) 
            {
                soundWave = true;
                sound_Wave.GetComponent<CircleCollider2D>().enabled = true;
                StartCoroutine("waitEcho");
            }
        }
        if (isPistol) 
        {
            Rigidbody2D Bullet = Instantiate(pistolBulletRB,spawnBullet.transform.position,spawnBullet.transform.rotation) as Rigidbody2D;
            Bullet.GetComponent<BulletController>().parentTransform = transform.parent.transform;
            Bullet.GetComponent<BulletController>().parentTag = transform.parent.tag;
            pistolBullets--;
            sound_Wave.radius = 40;
            AudioSource.PlayClipAtPoint(pistolFireAudioClip,transform.position);
            if (!soundWave) 
            {
                soundWave = true;
                sound_Wave.GetComponent<CircleCollider2D>().enabled = true;
                StartCoroutine("waitEcho");
            }
        }
        //if (isSniper)
        //{
        //    Rigidbody2D Bullet = Instantiate(sniperBulletRb, spawnBullet.transform.position, spawnBullet.transform.rotation) as Rigidbody2D;\
        //    Bullet.GetComponent<BulletController>().parentTransform = transform.parent.transform;
        //    Bullet.GetComponent<BulletController>().parentTag = transform.parent.tag;
        //    sniperBullets--; --;
        //    sound_Wave.radius = 70;
        //    AudioSource.PlayClipAtPoint(sniperFireAudioClip, transform.position);
        //    if (!soundWave)
        //    {
        //        soundWave = true;
        //        sound_Wave.GetComponent<CircleCollider2D>().enabled = true;
        //        StartCoroutine("waitEcho");
        //    }
        //}

    }
    void Death() 
    {
        sound_Wave.radius = 25;
        soundWave = true;
        sound_Wave.GetComponent<CircleCollider2D>().enabled = true;
        StartCoroutine("waitEcho");
        GetComponent<Path_Follow>().death = true;
        GetComponent<Path_Follow>().enabled = true;
        if (GetComponentInParent<NPC_Sighting>() != null) 
        {
            GetComponentInParent<NPC_Sighting>().Death = true;
        }
        transform.parent.gameObject.layer = LayerMask.NameToLayer("Death");
        transform.parent.gameObject.tag = "Death";
        GetComponent<BoxCollider2D>().enabled = false;
        //resize the dead body
        transform.localScale = new Vector3(deathScale, deathScale, deathScale);
        float randomRotation = Random.Range(1f,360f);
        transform.localRotation = Quaternion.Euler(new Vector3(0, 0, randomRotation));
        float bloodRandomRotation = Random.Range(1f,360f);
        Instantiate(bloodObject,spawnBlood.position,Quaternion.Euler(new Vector3(0,0,bloodRandomRotation)));
        enabled = false;
    }
    void reloadWeapon()
    {
        reload = true;
        canShoot = false;
        if (isRifle) 
        {
            rifleBullets = setRifleBullets;
            AudioSource.PlayClipAtPoint(rifleReloadAudioClip,transform.position);
        }
        if (isPistol) 
        {
            pistolBullets = setPistolBullets;
            AudioSource.PlayClipAtPoint(pistolReloadAudioClip,transform.position);
        }
        //if (isSniper) 
        //{
        //    sniperBullets = setSniperBullets;
        //    AudioSource.PlayClipAtPoint(sniperReloadAudioClip,transform.position);
        //}
    }

    IEnumerator waitEcho()
    {
        yield return new WaitForSeconds(0.1f);
        sound_Wave.GetComponent<CircleCollider2D>().enabled = false;
        soundWave = false;
    }
}
