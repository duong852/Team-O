using UnityEngine;
using System.Collections;

namespace GearsAndBrains
{ 

public class Enemy_Control : MonoBehaviour 
{
private bool ChangeWep = false, shieldON = false, vipON = false;
private bool shootON = false;
private bool Reload = false;
[HideInInspector]
public bool Aim = false; 

public Transform SpawnBullet;

private CircleCollider2D Sound_wave;
public AudioClip FootStepGrassAudioClip, FootStepWoodenAudioClip, FootStepGravelAudioClip;
public bool FooIconShow;
float timeToFire = 0f; 
Vector2 SpawnBulletPosition;

public Transform hitBloodObject;
public Transform bloodObject;

[Tooltip("use main weapon")]
public bool MainWeapon;             // variable for the main weapon
[Tooltip("use main weapon silencer")]
public bool MainWeaponSilencer;         // variable for the main weapon Silencer
public Rigidbody2D mainBullet;
public AudioClip mainFireAudioClip;
public AudioClip mainSilencerFireAudioClip;
public AudioClip mainReloadAudioClip;
private GameObject MainWeaponSilencerObject1, MainWeaponSilencerObject2, MainWeaponFlash1, MainWeaponFlash2;
private Transform SpawnBlood;

public float mainWepFireRate = 0f;
public int mainBullets = 30;
private int mainSetBullets;

[Tooltip("use secondary weapon")]
public bool SecWeapon;              // variable for the secondary weapon
[Tooltip("use secondary weapon silencer")]
public bool SecWeaponSilencer;          // variable for the secondary weapon Silencer
public Rigidbody2D secBullet;
public AudioClip secFireAudioClip;
public AudioClip secSilencerFireAudioClip;
public AudioClip secReloadAudioClip;
private GameObject SecWeaponSilencerObject, SecWeaponFlash;

public float secWepFireRate = 0f;
public int secBullets = 12;
private int secSetBullets;

[Tooltip("use Tactical shield")]
public bool Shield;              // variable for the tactical shield
[Tooltip("use vip person animation")]
public bool Vip;              // variable for the vip person animation

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
public int FootIconRadiusShow = 50;
public LayerMask FootStepSoundLayer, FootStepShowLayer;
public GameObject FootIcon;
private Enemy_Icon_Control EnemyIconControl;

        // Use this for initialization
        void Start() 
		{
			// === SET START PARAMETERS === //
			anim = GetComponent<Animator>();
			mainSetBullets = mainBullets;
			secSetBullets = secBullets;
			Damage = 0;
            Sound_wave = transform.Find("Sound_wave").GetComponent<CircleCollider2D>();
            EnemyIconControl = GetComponentInParent<Enemy_Icon_Control>();

            SpawnBlood = transform.Find("Spawn_Blood");

            if (transform.Find("Torso_Group/Rifle1/Rifle1_Silencer") != null)
                MainWeaponSilencerObject1 = transform.Find("Torso_Group/Rifle1/Rifle1_Silencer").gameObject;
            if (transform.Find("Torso_Group/Rifle2/Rifle2_Silencer") != null)
                MainWeaponSilencerObject2 = transform.Find("Torso_Group/Rifle2/Rifle2_Silencer").gameObject;
            if (transform.Find("Torso_Group/Rifle1/Rifle1_flash") != null)
                MainWeaponFlash1 = transform.Find("Torso_Group/Rifle1/Rifle1_flash").gameObject;
            if (transform.Find("Torso_Group/Rifle2/Rifle2_flash") != null)
                MainWeaponFlash2 = transform.Find("Torso_Group/Rifle2/Rifle2_flash").gameObject;
            if (transform.Find("Torso_Group/Pistol/Pistol_Silencer") != null)
                SecWeaponSilencerObject = transform.Find("Torso_Group/Pistol/Pistol_Silencer").gameObject;
            if (transform.Find("Torso_Group/Pistol/Pistol_flash") != null)
                SecWeaponFlash = transform.Find("Torso_Group/Pistol/Pistol_flash").gameObject;
            

            if (MainWeaponSilencer)
            {
                if (MainWeaponSilencerObject1 != null)
                    MainWeaponSilencerObject1.SetActive(true);
                if (MainWeaponSilencerObject2 != null)
                    MainWeaponSilencerObject2.SetActive(true);
                if (MainWeaponFlash1 != null)
                    MainWeaponFlash1.SetActive(false);
                if (MainWeaponFlash2 != null)
                    MainWeaponFlash2.SetActive(false);
            }
            else
            {
                if (MainWeaponSilencerObject1 != null)
                    MainWeaponSilencerObject1.SetActive(false);
                if (MainWeaponSilencerObject2 != null)
                    MainWeaponSilencerObject2.SetActive(false);
                if (MainWeaponFlash1 != null)
                    MainWeaponFlash1.SetActive(true);
                if (MainWeaponFlash2 != null)
                    MainWeaponFlash2.SetActive(true);
            }

            if (SecWeaponSilencer)
            {
                if (SecWeaponSilencerObject != null)
                    SecWeaponSilencerObject.SetActive(true);
                if (SecWeaponFlash != null)
                    SecWeaponFlash.SetActive(false);
            }
            else
            {
                if (SecWeaponSilencerObject != null)
                    SecWeaponSilencerObject.SetActive(false);
                if (SecWeaponFlash != null)
                    SecWeaponFlash.SetActive(true);
            }

            mySpriteRenders = GetComponentsInChildren<SpriteRenderer>();

            // === CHANGE WEAPON ANIMATION === //
            if (SecWeapon)
				anim.SetTrigger ("Change");

		}


	void Update () 
		{	
			// === CHECK DEATH === //
			if (HP <= 0 && !DeathTest)
			{
				DeathTest = true;			
				shootON = false;

				GameObject uiMenu = GameObject.FindWithTag ("GameController");
				if (uiMenu != null)
				{
					uiMenu.GetComponent<UI_menu> ().Score += setScore;
				}
				Death ();
			}


			// === CHECK HIT === //
			if (Damage > 0)
			{
                float randomX = Random.Range(-3, 3);
                float randomY = Random.Range(-3, 3);
                Vector3 spawnBloodVector = new Vector3 (SpawnBlood.position.x + randomX, SpawnBlood.position.y + randomY, SpawnBlood.position.z);
				Instantiate (hitBloodObject, spawnBloodVector,  transform.rotation);							
				HP -= Damage;
				Damage = 0;
			}

            // === SHIELD === //
            if (Shield && !shieldON)
            {
                shieldON = true;
                anim.SetTrigger("Shield");
                anim.SetBool("ShieldOn", true);
                if (!ChangeWep)
                    ChangeWep = true;
            }
            else if (!Shield && shieldON)
            {
                shieldON = false;
                anim.SetTrigger("Shield");
                anim.SetBool("ShieldOn", false);
                if (ChangeWep)
                    ChangeWep = false;
            }

            // === VIP PERSON === //
            if (Vip && !vipON)
            {
                vipON = true;
                anim.SetTrigger("Vip");
                anim.SetBool("VipOn", true);
                if (!ChangeWep)
                    ChangeWep = true;
            }
            else if (!Vip && vipON)
            {
                vipON = false;
                anim.SetTrigger("Vip");
                anim.SetBool("VipOn", false);
                if (ChangeWep)
                    ChangeWep = false;
            }

            // === RELOAD === //
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
		// === FIRE MAIN === //
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

		// === FIRE SECONDARY=== //
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

		// === AIMING ANIMATION, CALL FROM FUNCTION Enemy_FieldOfView === //
	  public void AimWeapon ()
		{
			//Aim = false;
			//Debug.Log (Aim.ToString ()); 
			anim.SetTrigger ("Aim");
		}

		// === CHANGES VARIABLES FROM ANIMATION === //
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

        void FootStep()
        {
            Collider2D targetCollider = Physics2D.OverlapCircle(transform.position, FootIconRadiusShow, FootStepShowLayer.value);
            if (targetCollider != null && targetCollider.gameObject.tag == "Blue team")
            {             
                Collider2D overlapHit = Physics2D.OverlapPoint(transform.position, FootStepSoundLayer.value);
                if (overlapHit != null && overlapHit.gameObject.tag == "Floor")
                {                  
                    AudioSource.PlayClipAtPoint(FootStepWoodenAudioClip, transform.position);
                    if (EnemyIconControl != null && EnemyIconControl.Hide == true && FootIcon != null && FooIconShow)
                    {
                        Instantiate(FootIcon, transform.position, transform.rotation);
                    }
                }
                if (overlapHit != null && overlapHit.gameObject.tag == "Gravel")
                {
                    AudioSource.PlayClipAtPoint(FootStepGravelAudioClip, transform.position);
                    if (EnemyIconControl != null && EnemyIconControl.Hide == true && FootIcon != null && FooIconShow)
                    {
                        Instantiate(FootIcon, transform.position, transform.rotation);
                    }
                }
                if (overlapHit != null && overlapHit.gameObject.tag == "Grass")
                {
                    AudioSource.PlayClipAtPoint(FootStepGrassAudioClip, transform.position);
                    if (EnemyIconControl.Hide == true && FootIcon != null && FooIconShow)
                    {
                        Instantiate(FootIcon, transform.position, transform.rotation);
                    }
                }
            }
        }

        // === CALL BULLETS, SOUND, ANIMATION SHOT === //
        void Shoot ()
		{
			if (!ChangeWep)
			{
				anim.SetTrigger ("Shoot");				
                Rigidbody2D Bullet = Instantiate(mainBullet, SpawnBullet.transform.position, SpawnBullet.transform.rotation) as Rigidbody2D;
                Bullet.GetComponent<Bullet>().parentTransform = transform.parent.transform;
                Bullet.GetComponent<Bullet>().parentTag = transform.parent.tag;
                mainBullets -= 1;

                if (MainWeaponSilencer)
                {
                    Sound_wave.radius = 20;
                    AudioSource.PlayClipAtPoint(mainSilencerFireAudioClip, transform.position);
                }
                else
                {
                    Sound_wave.radius = 60;
                    AudioSource.PlayClipAtPoint(mainFireAudioClip, transform.position);
                }

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
                Bullet.GetComponent<Bullet>().parentTransform = transform.parent.transform;
                Bullet.GetComponent<Bullet>().parentTag = transform.parent.tag;
                secBullets -= 1;

                if (SecWeaponSilencer)
                {
                    Sound_wave.radius = 5;
                    AudioSource.PlayClipAtPoint(secSilencerFireAudioClip, transform.position);
                }
                else
                {
                    Sound_wave.radius = 40;
                    AudioSource.PlayClipAtPoint(secFireAudioClip, transform.position);
                }

                if (!soundWave)
                {
                    soundWave = true;
                    Sound_wave.GetComponent<CircleCollider2D>().enabled = true;
                    StartCoroutine("waitEcho");
                }
            }
		}

		// === RELOADING ANIMATION, CHANGES VARIABLES BULLETS === //
		void ReloadWeapon ()
		{
			Reload = true;
			shootON = false;
			anim.ResetTrigger("Shoot");
			anim.SetTrigger ("Reload");
			if (!ChangeWep)
			{
				mainBullets = mainSetBullets;
				AudioSource.PlayClipAtPoint (mainReloadAudioClip, transform.position);
			}
			if (ChangeWep)
			{
				secBullets = secSetBullets;
				AudioSource.PlayClipAtPoint (secReloadAudioClip, transform.position);
			}
		}


		// === ANIMATION OF DEATH, CHANGE SORTING LAYER, RANDOM ROTATION BODY === //
		void Death ()
		{
            Sound_wave.radius = 25;
            soundWave = true;
            Sound_wave.GetComponent<CircleCollider2D>().enabled = true;
            StartCoroutine("waitEcho");
            if (EnemyIconControl != null)
            {
                EnemyIconControl.Death = true;
            }	      
            GetComponentInParent<Folow_Point_Control>().death = true;
            GetComponentInParent<Folow_Point_Control>().enabled = false;
            if (GetComponentInParent<Enemy_FieldOfView>() != null)
            {
                GetComponentInParent<Enemy_FieldOfView>().Death = true;
            }
            transform.parent.gameObject.layer = LayerMask.NameToLayer("Death");
            transform.parent.gameObject.tag = "Death";
                      
            if (GetComponent<Outfits> () != null)
			{
				GetComponent<Outfits> ().enabled = false;
			}
			GetComponent<BoxCollider2D>().enabled = false;			

			// === resizing dead bodies === //
			transform.localScale = new Vector3 (deathScale, deathScale, deathScale);

			float randomRotation = Random.Range (1f,360f);
			transform.localRotation = Quaternion.Euler (new Vector3 (0, 0, randomRotation));

            foreach (SpriteRenderer mySpriteRender in mySpriteRenders)
            {
                // === TORSO === //
                if (mySpriteRender.gameObject.name == "Healm")
                    mySpriteRender.sortingOrder = -8;
                if (mySpriteRender.gameObject.name == "Hat2")
                    mySpriteRender.sortingOrder = -8;
                if (mySpriteRender.gameObject.name == "Hat1")
                    mySpriteRender.sortingOrder = -8;
                if (mySpriteRender.gameObject.name == "Police_Hat")
                    mySpriteRender.sortingOrder = -8;
                if (mySpriteRender.gameObject.name == "Balaclava")
                    mySpriteRender.sortingOrder = -9;
                if (mySpriteRender.gameObject.name == "Head")
                    mySpriteRender.sortingOrder = -10;
                if (mySpriteRender.gameObject.name == "Rifle2")
                    mySpriteRender.sortingOrder = -11;
                if (mySpriteRender.gameObject.name == "Rifle2_Silencer")
                    mySpriteRender.sortingOrder = -12;
                if (mySpriteRender.gameObject.name == "Rifle1")
                    mySpriteRender.sortingOrder = -11;
                if (mySpriteRender.gameObject.name == "Rifle1_Silencer")
                    mySpriteRender.sortingOrder = -12;
                if (mySpriteRender.gameObject.name == "Pistol")
                    mySpriteRender.sortingOrder = -11;
                if (mySpriteRender.gameObject.name == "Pistol_Silencer")
                    mySpriteRender.sortingOrder = -12;
                if (mySpriteRender.gameObject.name == "Shield")
                    mySpriteRender.sortingOrder = -13;
                if (mySpriteRender.gameObject.name == "Tactical_Vest")
                    mySpriteRender.sortingOrder = -12;
                if (mySpriteRender.gameObject.name == "Police_text")
                    mySpriteRender.sortingOrder = -11;
                if (mySpriteRender.gameObject.name == "Bulletproof_Vest")
                    mySpriteRender.sortingOrder = -13;
                if (mySpriteRender.gameObject.name == "Blue_Shirt")
                    mySpriteRender.sortingOrder = -14;
                if (mySpriteRender.gameObject.name == "Camo_Shirt")
                    mySpriteRender.sortingOrder = -14;
                if (mySpriteRender.gameObject.name == "Shirt")
                    mySpriteRender.sortingOrder = -14;
                if (mySpriteRender.gameObject.name == "Suit")
                    mySpriteRender.sortingOrder = -14;
                if (mySpriteRender.gameObject.name == "Police_Shirt")
                    mySpriteRender.sortingOrder = -14;
                if (mySpriteRender.gameObject.name == "Gloves2")
                    mySpriteRender.sortingOrder = -15;
                if (mySpriteRender.gameObject.name == "Gloves2_Dop")
                    mySpriteRender.sortingOrder = -15;
                if (mySpriteRender.gameObject.name == "Gloves1")
                    mySpriteRender.sortingOrder = -15;
                if (mySpriteRender.gameObject.name == "Gloves1_Dop")
                    mySpriteRender.sortingOrder = -15;
                if (mySpriteRender.gameObject.name == "Torso")
                    mySpriteRender.sortingOrder = -16;

                // === LEGS === //
                if (mySpriteRender.gameObject.name == "Holster")
                    mySpriteRender.sortingOrder = -17;
                if (mySpriteRender.gameObject.name == "Holster_empty")
                    mySpriteRender.sortingOrder = -17;
                if (mySpriteRender.gameObject.name == "Blue_pants")
                    mySpriteRender.sortingOrder = -18;
                if (mySpriteRender.gameObject.name == "Camo_pants")
                    mySpriteRender.sortingOrder = -18;
                if (mySpriteRender.gameObject.name == "Sand_pants")
                    mySpriteRender.sortingOrder = -18;
                if (mySpriteRender.gameObject.name == "Trousers")
                    mySpriteRender.sortingOrder = -18;
                if (mySpriteRender.gameObject.name == "Shorts")
                    mySpriteRender.sortingOrder = -18;
                if (mySpriteRender.gameObject.name == "Police_Pants")
                    mySpriteRender.sortingOrder = -18;
                if (mySpriteRender.gameObject.name == "Sand_boots")
                    mySpriteRender.sortingOrder = -19;
                if (mySpriteRender.gameObject.name == "Blue_boots")
                    mySpriteRender.sortingOrder = -19;
                if (mySpriteRender.gameObject.name == "Black_boots")
                    mySpriteRender.sortingOrder = -19;
                if (mySpriteRender.gameObject.name == "Legs")
                    mySpriteRender.sortingOrder = -20;
                if (mySpriteRender.gameObject.name == "Sneakers2")
                    mySpriteRender.sortingOrder = -21;
                if (mySpriteRender.gameObject.name == "Sneakers1")
                    mySpriteRender.sortingOrder = -21;
                if (mySpriteRender.gameObject.name == "Police_Boots")
                    mySpriteRender.sortingOrder = -21;
                if (mySpriteRender.gameObject.name == "Foots")
                    mySpriteRender.sortingOrder = -22;
            }

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
       
        // === WAIT SOUND WAVES === //
        IEnumerator waitEcho()
        {            
            yield return new WaitForSeconds(0.1f);
            Sound_wave.GetComponent<CircleCollider2D>().enabled = false;
            soundWave = false;
        }
    }
}