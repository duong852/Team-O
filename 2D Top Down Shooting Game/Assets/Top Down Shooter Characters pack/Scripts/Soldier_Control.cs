using System.Collections;
using UnityEngine;

namespace GearsAndBrains
{

public class Soldier_Control : MonoBehaviour 
{
		public Transform SoldierGroup;		// object for moving control
		private Rigidbody2D SoldierRigidBody;
		private bool ChangeWep = false, shieldON = false;						
		private bool shootON = false;						
		private bool Reload = false;
		private bool setMove = false, setBuckMove = false;
		private Animator anim;
		private bool Aim = false;
		private Transform Soldier;          // object for rotation control
		public float moveSpeed, aimMoveSpeed;
		public float rottationSpeed = 20f;
		public AudioClip FootStepGrassAudioClip, FootStepWoodenAudioClip, FootStepGravelAudioClip;
		private float setMoveSpeed;
		public Transform SpawnBullet;
		private Transform SpawnBlood;
		private CircleCollider2D Sound_wave;

		public AudioClip changeWepAudioClip;
		public bool MainWeapon;				// variable for the main weapon
		public bool MainWeaponSilencer;			// variable for the main weapon Silencer
		public Rigidbody2D mainBullet;
		public AudioClip mainFireAudioClip;
		public AudioClip mainSilencerFireAudioClip;
		public AudioClip mainReloadAudioClip;
		private GameObject MainWeaponSilencerObject1, MainWeaponSilencerObject2, MainWeaponFlash1, MainWeaponFlash2;

		public float mainWepFireRate = 0f; 
		public int mainBullets = 30, mainBulletsStock = 90;
		private int mainSetBullets;

		public bool SecWeapon;				// variable for the secondary weapon
		public bool SecWeaponSilencer;			// variable for the secondary weapon Silencer
		public Rigidbody2D  secBullet;
		public AudioClip secFireAudioClip;
		public AudioClip secSilencerFireAudioClip;
		public AudioClip secReloadAudioClip;
		private GameObject SecWeaponSilencerObject, SecWeaponFlash;

		public float secWepFireRate = 0f; 
		public int secBullets = 12, secBulletsStock = 90;
		private int secSetBullets;

		public bool Shield;              // variable for the tactical shield

		public bool Vip;              // variable for the vip person animation

		public bool AutoReload = false;

		float timeToFire = 0f; 

		public int HP = 30;

		[Range(0.5f, 1.5f)]
		public float deathScale;

		public Transform hitBloodObject;
		public Transform bloodObject;

		private	bool soundWave = false;

		[HideInInspector]	
		public int Damage;

		[HideInInspector]
		public bool DeathTest = false;

		private Component[] mySpriteRenders;
		public LayerMask AimLineLayer, FootStepLayer;

		public GameObject mousePointer;
        // Use this for initialization
        void Awake () 
		{
			// === SET START PARAMETERS === //
			Damage = 0;
			setMoveSpeed = moveSpeed;
			mainSetBullets = mainBullets;                  				
			secSetBullets = secBullets;
			Soldier = GetComponent<Transform>();
			SoldierRigidBody = GetComponentInParent<Rigidbody2D> (); 
			anim = GetComponent<Animator>();
			Sound_wave = transform.Find ("Sound_wave").GetComponent<CircleCollider2D>();

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


            if (!MainWeapon && SecWeapon)
			{			
				shootON = false;
				ChangeWeapon ();
			}           

			mySpriteRenders = GetComponentsInChildren<SpriteRenderer>();
		}
	
		// Update is called once per frame
	void Update () 
		{
			if (HP <= 0 && !DeathTest)
			{	
			DeathTest = true;
			moveSpeed = 0f;					
			shootON = false;			
			Death ();
			}

			// === CHECK HIT === //
			if (Damage > 0)
			{
                float randomX = Random.Range(-3, 3);
                float randomY = Random.Range(-3, 3);
                Vector3 spawnBloodVector = new Vector3(SpawnBlood.position.x + randomX, SpawnBlood.position.y + randomY, SpawnBlood.position.z);
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
			if (Input.GetKeyDown (KeyCode.Mouse1))
				{
					AimWeapon ();
				}

			if (mainWepFireRate == 0) 
				{
					if (Input.GetKeyDown (KeyCode.Mouse0) && shootON && !ChangeWep && mainBullets > 0 && !Reload)		
						Shoot ();
				}
			else if (Input.GetKey (KeyCode.Mouse0) && Time.time > timeToFire && shootON && !ChangeWep && mainBullets > 0 && !Reload)
				{
					timeToFire = Time.time + 2 / mainWepFireRate;
					Shoot (); 
				}

			if (secWepFireRate == 0) 
				{
					if (Input.GetKeyDown (KeyCode.Mouse0) && shootON && ChangeWep && secBullets > 0 && !Reload)		
						Shoot ();
				}
			else if (Input.GetKey (KeyCode.Mouse0) && Time.time > timeToFire && shootON && ChangeWep && secBullets > 0 && !Reload) 
				{
					timeToFire = Time.time + 1 / secWepFireRate;
						Shoot (); 
				}				
				
				// === CHANGE WEAPON === //			
			if (Input.GetKeyDown (KeyCode.F) && MainWeapon && SecWeapon) 
				{	
					ChangeWeapon ();			
				}
				
				// === RELOAD === //
			if (Input.GetKeyDown (KeyCode.R))
				{
                    if (!ChangeWep && mainBulletsStock > 0 || ChangeWep && secBulletsStock > 0)
					ReloadWeapon (); 
				}
			

            // === AUTO RELOAD === //
            if (mainBulletsStock > 0 && mainBullets == 0 && AutoReload)
			{
				ReloadWeapon (); 
			}

			if (secBulletsStock > 0 && secBullets == 0 && AutoReload)
			{
				ReloadWeapon (); 
			}
		}

	void FixedUpdate ()
		{
			// === smooth speed change === //
			if (!Aim)
			{
				if (setMoveSpeed < moveSpeed)
					setMoveSpeed += 3f;
				if (setMoveSpeed >  moveSpeed)
					setMoveSpeed = moveSpeed;
			}
			else
			{
				if (setMoveSpeed > aimMoveSpeed)
					setMoveSpeed -= 3f;
				if (setMoveSpeed < aimMoveSpeed)
					setMoveSpeed = aimMoveSpeed;
			}
				// === PC AIMING === //
				if (!DeathTest) 
				{
				Vector3 mouse_pos = Input.mousePosition;
				Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
				mousePointer.transform.position = mousePos;
				Vector3 player_pos = Camera.main.WorldToScreenPoint (Soldier.transform.position);
				mouse_pos.x = mouse_pos.x - player_pos.x;
				mouse_pos.y = mouse_pos.y - player_pos.y;
				float angle = Mathf.Atan2 (mouse_pos.y, mouse_pos.x) * Mathf.Rad2Deg;
                Soldier.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(new Vector3(0, 0, angle - 90)), rottationSpeed);
                }
				//moving
				if (!DeathTest) {
					Vector2 input = new Vector2 (Input.GetAxis ("Horizontal"), Input.GetAxis ("Vertical"));	
					SoldierRigidBody.AddForce (input * setMoveSpeed);
					if (Input.GetAxis ("Horizontal") > 0 || Input.GetAxis ("Vertical") > 0)
					{
						setMove = true;
						if (!Aim)
						{
							anim.SetFloat("Speed", 1f);
						}
						else
						{
							anim.SetFloat("Speed", 0.6f);
						}

					}
					else if (Input.GetAxis ("Horizontal") < 0 || Input.GetAxis ("Vertical") < 0)
					{
						setBuckMove = true;
						if (!Aim)
						{
							anim.SetFloat("Speed", -1f);
						}
						else
						{
							anim.SetFloat("Speed", -0.6f);
						}
					}
					else if (Input.GetAxis ("Horizontal") == 0 && Input.GetAxis ("Vertical") == 0)
					{
						setMove = false;
						setBuckMove = false;
					}
					anim.SetBool ("Move", setMove);	
					anim.SetBool ("MoveBuck", setBuckMove);
				}
		}
		public void AimWeapon ()
		{
			shootON = false;
			Aim = false;
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
			AudioSource.PlayClipAtPoint (changeWepAudioClip, transform.position);
		}
        void FootStep()
        {
            Collider2D overlapHit = Physics2D.OverlapPoint(transform.position, FootStepLayer.value);
            if (overlapHit != null && overlapHit.gameObject.tag == "Floor")
            {
                AudioSource.PlayClipAtPoint(FootStepWoodenAudioClip, transform.position);
            }
            if (overlapHit != null && overlapHit.gameObject.tag == "Gravel")
            {
                AudioSource.PlayClipAtPoint(FootStepGravelAudioClip, transform.position);
            }
            if (overlapHit != null && overlapHit.gameObject.tag == "Grass")
            {
                AudioSource.PlayClipAtPoint(FootStepGrassAudioClip, transform.position);
            }
        }
		public void ChangeWeapon ()
		{
			shootON = false;
			if (!Aim && !Shield || !Aim && !Vip)
			{
				anim.SetTrigger ("Change");
			} 
			else if (Aim && !Shield || Aim && !Vip)
			{	
				anim.SetTrigger ("Aim");
				anim.SetTrigger ("Change");
				anim.SetTrigger ("Change_to_Aim");
			}
		}
		void Shoot ()
			{
				if (!ChangeWep)
				{						
					anim.SetTrigger ("Shoot");
				Rigidbody2D Bullet = Instantiate (mainBullet, SpawnBullet.transform.position, SpawnBullet.transform.rotation) as Rigidbody2D;
				Bullet.GetComponent<Bullet>().parentTransform = transform.parent.transform;
                Bullet.GetComponent<Bullet>().parentTag = transform.parent.tag;
				mainBullets -= 1;
				if (MainWeaponSilencer)
				{
					Sound_wave.radius = 20;
					AudioSource.PlayClipAtPoint (mainSilencerFireAudioClip, transform.position);
				}
				else
					{
						Sound_wave.radius = 60;
						AudioSource.PlayClipAtPoint (mainFireAudioClip, transform.position);
					}					
		
				if (!soundWave)
					{
						soundWave = true;
						Sound_wave.GetComponent<CircleCollider2D>().enabled = true; 
						StartCoroutine ("waitEcho");
					}
				}
				if (ChangeWep)
				{
					anim.SetTrigger ("Shoot");
					Sound_wave.GetComponent<CircleCollider2D>().enabled = true;
                    Rigidbody2D Bullet = Instantiate(secBullet, SpawnBullet.transform.position, SpawnBullet.transform.rotation) as Rigidbody2D;
                    Bullet.GetComponent<Bullet>().parentTransform = transform.parent.transform;
                    Bullet.GetComponent<Bullet>().parentTag = transform.parent.tag;
                    secBullets -= 1;
					if (SecWeaponSilencer)
					{
						Sound_wave.radius = 5;
						AudioSource.PlayClipAtPoint (secSilencerFireAudioClip, transform.position);
					}
					else
					{
						Sound_wave.radius = 40;
						AudioSource.PlayClipAtPoint (secFireAudioClip, transform.position);
					}
					if (!soundWave)
					{
						soundWave = true;
						Sound_wave.GetComponent<CircleCollider2D>().enabled = true; 
						StartCoroutine ("waitEcho");
					}		
				}
			}
		public void ReloadWeapon ()
		{
			if (Aim && !Reload)
			{
				Reload = true;
				shootON = false;
				anim.SetTrigger ("Reload");
				anim.ResetTrigger ("Shoot");
				if (!ChangeWep) 
				{
                    if (mainBulletsStock >= mainSetBullets)
                    {
                        mainBulletsStock -= mainSetBullets - mainBullets;
                        mainBullets = mainSetBullets;
                    }
                    else
                    {
                        mainBullets = mainBulletsStock;
                        mainBulletsStock -= mainBulletsStock;
                    }
                    if (mainBulletsStock <= 0)
                    {
                        mainBulletsStock = 0;
                    }
                    AudioSource.PlayClipAtPoint (mainReloadAudioClip, transform.position);
				}
				if (ChangeWep)
				{               
                    if (secBulletsStock >= secSetBullets)
                    {
                        secBulletsStock -= secSetBullets - secBullets;
                        secBullets = secSetBullets;
                    }
                    else
                    {
                        secBullets = secBulletsStock;
                        secBulletsStock -= secBulletsStock;
                    }

                    if (secBulletsStock <= 0)
                    {
                        secBulletsStock = 0;
                    }
                    AudioSource.PlayClipAtPoint (secReloadAudioClip, transform.position);
				}
			}
		}
		void Death ()
		{
			StartCoroutine ("waitDeath");
			if (GetComponent<Outfits> () != null)
			{
				GetComponent<Outfits> ().enabled = false;
			}
            transform.localScale = new Vector3 (deathScale, deathScale, deathScale);
			float randomRotation = Random.Range (1f,360f);
			transform.localRotation = Quaternion.Euler (new Vector3 (0, 0,randomRotation));  		
			foreach (SpriteRenderer mySpriteRender in mySpriteRenders)
			{				
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
            Instantiate(bloodObject, SpawnBlood.position, Quaternion.Euler(new Vector3(0, 0, bloodRandomRotation)));
        }
		IEnumerator waitEcho()
		{
			yield return new WaitForSeconds (0.1f);	
			Sound_wave.GetComponent<CircleCollider2D>().enabled = false;
			soundWave = false;
		}
		IEnumerator waitDeath()
		{	
			yield return new WaitForSeconds (3f);	
			transform.parent.gameObject.GetComponent<CircleCollider2D>().enabled = false;
		}
	}
}