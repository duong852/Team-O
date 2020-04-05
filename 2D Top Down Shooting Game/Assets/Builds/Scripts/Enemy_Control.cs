using UnityEngine;
using System.Collections;
public class Enemy_Control : MonoBehaviour 
{
private bool ChangeWep = false;
private bool shootON = false;
private bool Reload = false;
[HideInInspector]
public bool Aim = false; 

public Transform SpawnBullet;
private CircleCollider2D Sound_wave;
float timeToFire = 0f; 
Vector2 SpawnBulletPosition;
public Transform hitBloodObject;
public Transform bloodObject;
public bool MainWeapon;             // variable for the main weapon
public Rigidbody2D mainBullet;
private Transform SpawnBlood;
public float mainWepFireRate = 0f;
public int mainBullets = 30;
private int mainSetBullets;
public bool SecWeapon;              // variable for the secondary weapon
public Rigidbody2D secBullet;
public float secWepFireRate = 0f;
public int secBullets = 12;
private int secSetBullets;
private Animator anim;
[HideInInspector]
public bool TargetIn;
public int HP = 30;
[Range(0.5f, 1.5f)]
public float deathScale;
[HideInInspector]
public int Damage;
[HideInInspector]
public bool DeathTest = false;
private bool soundWave = false;
public int setScore = 1;
private Component[] mySpriteRenders;
        // Use this for initialization
        void Start() 
		{
			anim = GetComponent<Animator>();
			mainSetBullets = mainBullets;
			secSetBullets = secBullets;
			Damage = 0;
            Sound_wave = transform.Find("Sound_wave").GetComponent<CircleCollider2D>();
            SpawnBlood = transform.Find("Spawn_Blood");
            mySpriteRenders = GetComponentsInChildren<SpriteRenderer>();
            if (SecWeapon)
				anim.SetTrigger ("Change");
		}
	void Update () 
		{	
			if (HP <= 0 && !DeathTest)
			{
				DeathTest = true;			
				shootON = false;
				GameObject uiMenu = GameObject.FindWithTag ("GameController");
				if (uiMenu != null)
				{
					uiMenu.GetComponent<UI_Manager>().score += setScore;
				}
				Death ();
			}
			if (Damage > 0)
			{
                float randomX = Random.Range(-3, 3);
                float randomY = Random.Range(-3, 3);
                Vector3 spawnBloodVector = new Vector3 (SpawnBlood.position.x + randomX, SpawnBlood.position.y + randomY, SpawnBlood.position.z);
				Instantiate (hitBloodObject, spawnBloodVector,  transform.rotation);							
				HP -= Damage;
				Damage = 0;
			}
            if (mainBullets == 0 && Aim && !Reload)
			{
				ReloadWeapon (); 
			}

			if (secBullets == 0 && Aim && !Reload)
			{
				ReloadWeapon (); 
			}
		}
	// Update is called once per frame
	void FixedUpdate () 
		{
			if (mainWepFireRate == 0) 
			{
				if (shootON && !ChangeWep && mainBullets > 0 && TargetIn)		
					Shoot ();
			}
			else if (Time.time > timeToFire && shootON && !ChangeWep && mainBullets > 0 && TargetIn && !Reload)
			{
				timeToFire = Time.time + 1 / mainWepFireRate;
				Shoot (); 
			}
			if (secWepFireRate == 0) 
			{
				if (shootON && ChangeWep && secBullets > 0 && TargetIn)		
					Shoot ();
			}
			else if (Time.time > timeToFire && shootON && ChangeWep && secBullets > 0 && TargetIn && !Reload)
			{
				timeToFire = Time.time + 1 / secWepFireRate;
				Shoot (); 
			}
		}
	  public void AimWeapon ()
		{
			anim.SetTrigger ("Aim");
		}
		void AimModON ()
		{
			Aim = true;
		}
		
		void AimModOFF ()
		{
			Aim = false;
		}
		
		void ShootON ()
		{
			shootON = true;
		}
		
		void ShootOFF ()
		{
			shootON = false;
		}
		
		void ReloadOFF ()
		{
			Reload = false;
		}
		
		void FlipBoolWepon ()
		{
			ChangeWep = !ChangeWep;
		}
        void Shoot ()
		{
			if (!ChangeWep)
			{
				anim.SetTrigger ("Shoot");				
                Rigidbody2D Bullet = Instantiate(mainBullet, SpawnBullet.transform.position, SpawnBullet.transform.rotation) as Rigidbody2D;
                Bullet.GetComponent<bulletController>().parentTransform = transform.parent.transform;
                Bullet.GetComponent<bulletController>().parentTag = transform.parent.tag;
                mainBullets -= 1;
                Sound_wave.radius = 60;
                if (!soundWave)
                {
                    soundWave = true;
                    Sound_wave.GetComponent<CircleCollider2D>().enabled = true;
                    StartCoroutine("waitEcho");
                }
            }
			if (ChangeWep)
			{
				anim.SetTrigger ("Shoot");
                Rigidbody2D Bullet = Instantiate(secBullet, SpawnBullet.transform.position, SpawnBullet.transform.rotation) as Rigidbody2D;
                Bullet.GetComponent<bulletController>().parentTransform = transform.parent.transform;
                Bullet.GetComponent<bulletController>().parentTag = transform.parent.tag;
                secBullets -= 1;
                Sound_wave.radius = 40;
                if (!soundWave)
                {
                    soundWave = true;
                    Sound_wave.GetComponent<CircleCollider2D>().enabled = true;
                    StartCoroutine("waitEcho");
                }
            }
		}
		void ReloadWeapon ()
		{
			Reload = true;
			shootON = false;
			anim.ResetTrigger("Shoot");
			anim.SetTrigger ("Reload");
			if (!ChangeWep)
			{
				mainBullets = mainSetBullets;
			}
			if (ChangeWep)
			{
				secBullets = secSetBullets;
			}
		}
		void Death ()
		{
            Sound_wave.radius = 25;
            soundWave = true;
            Sound_wave.GetComponent<CircleCollider2D>().enabled = true;
            StartCoroutine("waitEcho");      
            GetComponentInParent<Folow_Point_Control>().death = true;
            GetComponentInParent<Folow_Point_Control>().enabled = false;
            transform.gameObject.layer = LayerMask.NameToLayer("Death");
            transform.gameObject.tag = "Death";
			GetComponent<BoxCollider2D>().enabled = false;			
			Destroy(gameObject);

			float randomRotation = Random.Range (1f,360f);
			transform.localRotation = Quaternion.Euler (new Vector3 (0, 0, randomRotation));
            if (!ChangeWep)
			{
				anim.SetBool ("Main_Weapon_Death", true);
			} 
			else
			{
				anim.SetBool ("Secondary_Weapon_Death", true);
			}
            float bloodRandomRotation = Random.Range(1f, 360f);            
            Instantiate (bloodObject, SpawnBlood.position, Quaternion.Euler(new Vector3(0, 0, bloodRandomRotation)));
			enabled = false;
		}
        IEnumerator waitEcho()
        {            
            yield return new WaitForSeconds(0.1f);
            Sound_wave.GetComponent<CircleCollider2D>().enabled = false;
            soundWave = false;
        }
    }
