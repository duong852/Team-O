using UnityEngine;
using System.Collections;

namespace GearsAndBrains 
{
	public class Enemy_Icon_Control : MonoBehaviour 
	{
	[HideInInspector]
	public bool Hide, Death;
    public bool HideOn;              // hide out of the field of vision
    private Transform enemyIconTransform;    // icon enemy transform
	[HideInInspector]
	public Transform playerTransform;
	private float z;
	private Component[] mySpriteRenders;
	// Use this for initialization
	void Start()
	{
		Hide = true;
		enemyIconTransform = transform.Find ("Enemy_Icon");
		mySpriteRenders = GetComponentsInChildren<SpriteRenderer>();
        enemyIconTransform.gameObject.GetComponent<SpriteRenderer>().enabled = false;
    }
	// Update is called once per frame
	void Update () 
	{	
		if (!Hide && !Death) 
		{
			if (HideOn)
			{
				foreach (SpriteRenderer mySpriteRender in mySpriteRenders)
				{
							mySpriteRender.enabled = true;
				}
			}
			if (enemyIconTransform != null)
			{
				float distance =  Vector2.Distance(playerTransform.position, transform.position);
				if (distance >= 20)
				{
				enemyIconTransform.gameObject.GetComponent<SpriteRenderer>().enabled = true;
				enemyIconTransform.position = playerTransform.position;		  			
				z = Mathf.Atan2 ((transform.position.y -playerTransform.position.y), (transform.position.x - playerTransform.position.x)) * Mathf.Rad2Deg - 96;
				Quaternion Angel = Quaternion.Euler (enemyIconTransform.rotation.x, enemyIconTransform.rotation.y, enemyIconTransform.rotation.z + z); 
				enemyIconTransform.rotation = Quaternion.RotateTowards (enemyIconTransform.rotation, Angel, 500); 
				}
				else
				{
					enemyIconTransform.gameObject.GetComponent<SpriteRenderer>().enabled = false;
				}
			}
		}
		else if (Hide && !Death)
		{
			enemyIconTransform.gameObject.GetComponent<SpriteRenderer>().enabled = false;
			if (HideOn)
			{
				foreach (SpriteRenderer mySpriteRender in mySpriteRenders)
					{
							mySpriteRender.enabled = false;
					}
			}
		}
		else if (Death)
		{               
			foreach (SpriteRenderer mySpriteRender in mySpriteRenders)
				{
					mySpriteRender.enabled = true;
				}              
			enemyIconTransform.gameObject.GetComponent<SpriteRenderer>().enabled = false;					
			enabled = false;
		}
	}
	}
}
