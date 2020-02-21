using UnityEngine;
using System.Collections;

namespace GearsAndBrains
{
public class Block_Control : MonoBehaviour {

public Sprite wholeSprite;	
public Sprite brokenSprite;	
public Sprite destroySprite;
public Transform debris;

public int HP = 30;
private int HPInt;
private int Damage;
private	bool crack = false;

		void Start ()
		{	
			// === SET START PARAMETERS === //
			GetComponent<SpriteRenderer> ().sprite = wholeSprite;
			HPInt = HP;		
		}

		// === CHECK HIT, SPRITES CONTROL === //	
		void OnTriggerEnter2D(Collider2D trig)
		{
			if (trig.gameObject.tag == "Bullet" ) 
			{
				float chWait = trig.gameObject.GetComponent<Bullet>().Wait;

				if (HPInt > 0 && chWait >= 0.6f)
				{
					Damage = trig.gameObject.GetComponent<Bullet> ().Damage;				
					HPInt -= Damage;
				}
		
				if (HPInt < HP / 2 && brokenSprite != null && !crack)	
				{
					crack = true;
					GetComponent<SpriteRenderer> ().sprite = brokenSprite;
					if (debris != null) 
						Instantiate (debris, transform.position,  transform.rotation);
				}

				if (HPInt <= 0 && destroySprite != null) 
				{	
					GetComponent<SpriteRenderer> ().sprite = destroySprite;
					GetComponent<BoxCollider2D> ().enabled = false;
					GetComponent<SpriteRenderer> ().sortingOrder = -23; 
					if (debris != null) 			
						Instantiate (debris, transform.position, Quaternion.Euler (new Vector3 (0, 0, 0)));
				}
			}
		
	}


}
}