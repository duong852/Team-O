using UnityEngine;
using System.Collections;

namespace GearsAndBrains
{
	
public class Blood_Control : MonoBehaviour {

public Sprite bloodSprite1;
public Sprite bloodSprite2;
public Sprite bloodSprite3;

private int randomSprite;

	// Use this for initialization
	void Start () 
		{
			randomSprite = Random.Range (1,4);

			if (randomSprite == 1)
				GetComponent<SpriteRenderer>().sprite = bloodSprite1;

			if (randomSprite == 2)
				GetComponent<SpriteRenderer>().sprite = bloodSprite2;

			if (randomSprite == 3)
				GetComponent<SpriteRenderer>().sprite = bloodSprite3;


			float randomRotation = Random.Range (1f,360f);
			transform.localRotation = Quaternion.Euler (new Vector3 (0, 0,randomRotation));

			}
	
	// Update is called once per frame
	void Update () {
	
	}
  }
}