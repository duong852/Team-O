using UnityEngine;
using System.Collections;
public class bulletController : MonoBehaviour
{
	public string RedTeamTag, BlueTeamTag;
	public Transform parentTransform;
	public string parentTag;
	public float bulletSpeed = 100f;
	public int Damage = 1;
	[HideInInspector]
	public float Wait = 0;
	public Sprite hitSprite;
	// Use this for initialization
	void Start()
	{
		GetComponent<Rigidbody2D>().AddRelativeForce(Vector2.right * bulletSpeed);
		Destroy(gameObject, 2f);
	}
	void FixedUpdate()
	{
		if (Wait < 0.6f)
			Wait += 0.1f;
	}
	void OnTriggerEnter2D(Collider2D trig)
	{

		if (trig.gameObject.tag == "Wall" || trig.gameObject.tag == "Door")
		{
			GetComponent<SpriteRenderer>().sprite = hitSprite;
			Destroy(gameObject, 0.02f);
		}
		if (trig.gameObject.tag == "Untagged")
		{
			GetComponent<SpriteRenderer>().sprite = hitSprite;
			Destroy(gameObject, 0.02f);
		}
		if (trig.gameObject.tag == RedTeamTag)
		{
			GetComponent<SpriteRenderer>().sprite = hitSprite;
			trig.gameObject.GetComponent<NPCController>().Damage = Damage;
			if (trig.gameObject != null && trig.gameObject.GetComponent<NPCPatrolController>().targetInSight == false && parentTag == BlueTeamTag)
			{
				trig.gameObject.GetComponent<NPCPatrolController>().targetInSight = true;
				trig.gameObject.GetComponent<NPCPatrolController>().AimTarget = parentTransform;
			}
			Destroy(gameObject, 0.02f);
		}
		if (trig.gameObject.tag == BlueTeamTag)
		{
			GetComponent<SpriteRenderer>().sprite = hitSprite;
			trig.gameObject.GetComponentInChildren<PlayerController>().Damage = Damage;
			Destroy(gameObject, 0.02f);
		}
	}
}

