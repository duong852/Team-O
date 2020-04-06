using UnityEngine;
using System.Collections;
public class NPCController : MonoBehaviour
{
	private bool ChangeWep = false;
	private bool canShoot = true;
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
	public int setScore = 30;
	// Use this for initialization
	void Start()
	{
		anim = GetComponent<Animator>();
		mainSetBullets = mainBullets;
		secSetBullets = secBullets;
		Damage = 0;
		Sound_wave = transform.Find("Sound_wave").GetComponent<CircleCollider2D>();
		SpawnBlood = transform.Find("Spawn_Blood");
	}
	void Update()
	{
		if (HP <= 0 && !DeathTest)
		{
			DeathTest = true;
			canShoot = false;
			GameObject uiMenu = GameObject.FindWithTag("GameController");
			if (uiMenu != null)
			{
				uiMenu.GetComponent<UI_Manager>().score += setScore;
			}
			Death();
		}
		if (Damage > 0)
		{
			float randomX = Random.Range(-3, 3);
			float randomY = Random.Range(-3, 3);
			Vector3 spawnBloodVector = new Vector3(SpawnBlood.position.x + randomX, SpawnBlood.position.y + randomY, SpawnBlood.position.z);
			Instantiate(hitBloodObject, spawnBloodVector, transform.rotation);
			HP -= Damage;
			Damage = 0;
		}
		if (mainBullets == 0 && Aim && !Reload)
		{
			ReloadWeapon();
		}

		if (secBullets == 0 && Aim && !Reload)
		{
			ReloadWeapon();
		}
	}
	// Update is called once per frame
	void FixedUpdate()
	{
		Debug.Log("TargetIn" + TargetIn);
		Debug.Log("ShootOn" + canShoot);
		if (mainWepFireRate == 0)
		{
			Debug.Log("shot player");
			if (canShoot && !ChangeWep && mainBullets > 0 && TargetIn)
				Shoot();
		}
		else if (Time.time > timeToFire && canShoot && !ChangeWep && mainBullets > 0 && TargetIn && !Reload)
		{
			timeToFire = Time.time + 1 / mainWepFireRate;
			Shoot();
		}
		if (secWepFireRate == 0)
		{
			if (canShoot && ChangeWep && secBullets > 0 && TargetIn)
				Shoot();
		}
		else if (Time.time > timeToFire && canShoot && ChangeWep && secBullets > 0 && TargetIn && !Reload)
		{
			timeToFire = Time.time + 1 / secWepFireRate;
			Shoot();
		}
	}

	void AimModON()
	{
		Aim = true;
	}

	void AimModOFF()
	{
		Aim = false;
	}

	void ShootON()
	{
		canShoot = true;
	}

	void ShootOFF()
	{
		canShoot = false;
	}

	void ReloadOFF()
	{
		Reload = false;
	}

	void FlipBoolWepon()
	{
		ChangeWep = !ChangeWep;
	}
	void Shoot()
	{
		if (!ChangeWep)
		{
			Rigidbody2D Bullet = Instantiate(mainBullet, SpawnBullet.transform.position, SpawnBullet.transform.rotation) as Rigidbody2D;
			Bullet.GetComponent<bulletController>().parentTransform = transform.transform;
			Bullet.GetComponent<bulletController>().parentTag = transform.tag;
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
			Rigidbody2D Bullet = Instantiate(secBullet, SpawnBullet.transform.position, SpawnBullet.transform.rotation) as Rigidbody2D;
			Bullet.GetComponent<bulletController>().parentTransform = transform.transform;
			Bullet.GetComponent<bulletController>().parentTag = transform.tag;
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
	void ReloadWeapon()
	{
		Reload = true;
		canShoot = false;
		if (!ChangeWep)
		{
			mainBullets = mainSetBullets;
		}
		if (ChangeWep)
		{
			secBullets = secSetBullets;
		}
		canShoot = true;
	}
	void Death()
	{
		Sound_wave.radius = 25;
		soundWave = true;
		Sound_wave.GetComponent<CircleCollider2D>().enabled = true;
		StartCoroutine("waitEcho");
		GetComponent<NPCPatrolController>().death = true;
		GetComponent<NPCPatrolController>().enabled = false;
		transform.gameObject.layer = LayerMask.NameToLayer("Death");
		transform.gameObject.tag = "Death";
		GetComponent<BoxCollider2D>().enabled = false;
		Destroy(gameObject);
		float bloodRandomRotation = Random.Range(1f, 360f);
		Instantiate(bloodObject, SpawnBlood.position, Quaternion.Euler(new Vector3(0, 0, bloodRandomRotation)));
		enabled = false;
	}
	IEnumerator waitEcho()
	{
		yield return new WaitForSeconds(0.1f);
		Sound_wave.GetComponent<CircleCollider2D>().enabled = false;
		soundWave = false;
	}
}
