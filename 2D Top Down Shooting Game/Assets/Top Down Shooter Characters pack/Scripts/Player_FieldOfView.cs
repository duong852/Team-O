using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace GearsAndBrains
{
[ExecuteInEditMode]
public class Player_FieldOfView : MonoBehaviour {
	public bool fieldOfViewDraw;	
	public Transform myRotationTransform;
	public LayerMask TargetLayer;
	public string targetTag;	
	[Range(0f, 100f)]
	public float viewRange;
	[Range(0f, 180f)]
	public float viewAngel;
	public Image fieldImage;		
	private Soldier_Control soldierControl;
		void Awake () 
		{
			soldierControl = GetComponentInChildren<Soldier_Control> ();
		}
		void Update ()  
		{
			if (fieldOfViewDraw) 
			{
				fieldImage.gameObject.SetActive(true);
				fieldImage.transform.localScale = new Vector3(viewRange / 25, viewRange / 25, viewRange / 25);
				fieldImage.fillAmount = viewAngel / 180;
				Quaternion Angel = Quaternion.Euler (0, 0, (viewAngel / 1f) + 90);
				fieldImage.transform.localRotation = Quaternion.RotateTowards (myRotationTransform.rotation, Angel, 500);
			}
			else
			{
				fieldImage.gameObject.SetActive(false);
			}

			if (soldierControl.DeathTest == true)
			{
				fieldImage.enabled = false;
				enabled = false;
			}
		}
	

	void FixedUpdate () 
		{
			Collider2D[] targetColliders = Physics2D.OverlapCircleAll(transform.position, viewRange, TargetLayer.value);
			foreach (var targetCollider in targetColliders) 
			{
				if (targetCollider.gameObject.tag == targetTag) 
				{
					float distance =  Vector2.Distance(targetCollider.transform.position, myRotationTransform.position);
					Vector2 targetDir = targetCollider.transform.position - myRotationTransform.position;
					Vector2 forward = myRotationTransform.up;
					float angel = Vector2.Angle (targetDir, forward);
					if (distance <= viewRange && angel <= viewAngel) 
					{								
						if (targetCollider.gameObject.GetComponent<Enemy_Icon_Control> () != null) 
						{
							Enemy_Icon_Control enemyIconControl = targetCollider.gameObject.GetComponent<Enemy_Icon_Control> ();
							
							Vector2 direction = targetCollider.transform.position - transform.position;			
							RaycastHit2D hit = Physics2D.Raycast (transform.position, direction, Mathf.Infinity, TargetLayer.value);	
							if (hit.collider.gameObject.tag == targetTag) 
							{
								enemyIconControl.Hide = false;
								enemyIconControl.playerTransform = transform;
							}
							else
							{
								enemyIconControl.Hide = true;
								enemyIconControl.playerTransform = null;
							}
						}							
					}
					else
					{
						if (targetCollider.gameObject.GetComponent<Enemy_Icon_Control> () != null) 
						{
							Enemy_Icon_Control enemyIconControl = targetCollider.gameObject.GetComponent<Enemy_Icon_Control> ();							
							enemyIconControl.Hide = true;
							enemyIconControl.playerTransform = null;
						}
					}		
				}
			}
		} 	
	}
}