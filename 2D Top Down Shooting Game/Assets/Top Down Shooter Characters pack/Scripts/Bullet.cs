using UnityEngine;
using System.Collections;

namespace GearsAndBrains
{
public class Bullet : MonoBehaviour {

public string RedTeamTag, BlueTeamTag;

public Transform parentTransform;   // transform assigned to the parent object
public string parentTag;            // tag assigned to the parent object

public float bulletSpeed = 100f;
public int Damage = 1;

private bool hitShield;

[HideInInspector]
public float Wait = 0;

public Sprite hitSprite;	

	// Use this for initialization
	void Start ()
		{		
				// === SET START PARAMETERS === //
				GetComponent<Rigidbody2D>().AddRelativeForce( Vector2.right * bulletSpeed);
                hitShield = false;
                Destroy (gameObject, 2f);						

		}
		// === TIMER === //
		void FixedUpdate ()
		{
			if (Wait < 0.6f)
			Wait += 0.1f; 

		}

			
		// === CHECK HIT === //	
		void OnTriggerEnter2D(Collider2D trig)
		{

            if (trig.gameObject.tag == "Wall" || trig.gameObject.tag == "Door")
            {
                hitShield = true;
                GetComponent<SpriteRenderer>().sprite = hitSprite;
                Destroy (gameObject, 0.02f);
                //Destroy(gameObject);
            }

            if (trig.gameObject.tag == "Untagged")
			{
				GetComponent<SpriteRenderer> ().sprite = hitSprite;
				Destroy (gameObject, 0.02f);
			}
            // === hit Red Team === //	
            if (trig.gameObject.tag == RedTeamTag && !hitShield)
			{
				GetComponent<SpriteRenderer> ().sprite = hitSprite;
				trig.gameObject.GetComponentInChildren<Enemy_Control>().Damage = Damage;
                if (trig.gameObject != null && trig.gameObject.GetComponent<Folow_Point_Control>().targetInSight == false && parentTag == BlueTeamTag)
                {
                    trig.gameObject.GetComponent<Folow_Point_Control>().targetInSight = true;
                    trig.gameObject.GetComponent<Folow_Point_Control>().AimTarget = parentTransform;
                }
                Destroy (gameObject, 0.02f);
			}
            // === hit Blue Team === //	
            if (trig.gameObject.tag == BlueTeamTag && !hitShield)
			{                
                GetComponent<SpriteRenderer> ().sprite = hitSprite;
				trig.gameObject.GetComponentInChildren<Soldier_Control>().Damage = Damage;                
				Destroy (gameObject, 0.02f);
			}			

			if (trig.gameObject.tag == "Block" && Wait < 0.6f)
			{
				// check shot from the shelter
			}
			else if (trig.gameObject.tag == "Block" && Wait >= 0.6f)
			{
                int RandomInt = Random.Range(0, 5);

                // Debug.Log(RandomInt.ToString());

                if (RandomInt >= 1)
                {
                    GetComponent<SpriteRenderer>().sprite = hitSprite;
                    Destroy(gameObject, 0.02f);
                }
                else if (RandomInt == 0 )
                {
                    // bullet missed
                }
            }

            if (trig.gameObject.tag == "Table" && Wait < 0.6f)
            {
                // check shot from the shelter
            }
            else if (trig.gameObject.tag == "Table" && Wait >= 0.6f)
            {
                int RandomInt = Random.Range(0, 3);

               // Debug.Log(RandomInt.ToString());

                if (RandomInt == 0)
                {
                    GetComponent<SpriteRenderer>().sprite = hitSprite;
                    Destroy(gameObject, 0.02f);
                }
                else if (RandomInt >= 1)
                {
                    // bullet missed
                }


            }

        }
	}
}
