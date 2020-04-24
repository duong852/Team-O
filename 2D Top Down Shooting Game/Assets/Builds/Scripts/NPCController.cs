using UnityEngine;
using System.Collections;
public class NPCController : MonoBehaviour
{
	private bool ChangeWep = false;
	[HideInInspector]
	public bool canShoot = false;
	[HideInInspector]
	public bool Reload = false;
	public Transform SpawnBullet;
	private CircleCollider2D Sound_wave;
	float timeToFire = 0f;
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

	[HideInInspector]
	public bool TargetIn;
	public int HP = 30;
	[HideInInspector]
	public int Damage;
	[HideInInspector]
	public bool isDeath = false;
	private bool soundWave = false;
	public int setScore = 30;
	// Use this for initialization
	void Start()
	{
		mainSetBullets = mainBullets;
		secSetBullets = secBullets;
		Damage = 0;
		Sound_wave = transform.Find("Sound_wave").GetComponent<CircleCollider2D>();
		SpawnBlood = transform.Find("Spawn_Blood");
	}
	void Update()
	{
		if (HP <= 0 && !isDeath)
		{
			isDeath = true;
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
		if (mainBullets == 0 && !Reload)
		{
			ReloadWeapon();
		}

		if (secBullets == 0 && !Reload)
		{
			ReloadWeapon();
		}
	}
	// Update is called once per frame
	void FixedUpdate()
	{
		if (mainWepFireRate == 0)
		{
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
	void FlipBoolWepon()
	{
		ChangeWep = !ChangeWep;
	}
	void Shoot()
	{
		if (!ChangeWep)
		{
			Rigidbody2D Bullet = Instantiate(mainBullet, SpawnBullet.transform.position, SpawnBullet.transform.rotation) as Rigidbody2D;
			Bullet.GetComponent<BulletControll>().parentTransform = transform.transform;
			Bullet.GetComponent<BulletControll>().parentTag = transform.tag;
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
			Bullet.GetComponent<BulletControll>().parentTransform = transform.transform;
			Bullet.GetComponent<BulletControll>().parentTag = transform.tag;
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
		Reload = false;
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
		GameObject.Find("ExtractionZone").GetComponent<ExtractionZone>().enemyCount--;
	}
	IEnumerator waitEcho()
	{
		yield return new WaitForSeconds(0.1f);
		Sound_wave.GetComponent<CircleCollider2D>().enabled = false;
		soundWave = false;
	}
}
