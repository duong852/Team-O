using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace GearsAndBrains
{
[ExecuteInEditMode]
public class Enemy_FieldOfView : MonoBehaviour {
	private bool startCoroutine = false;
	private bool Aim = false;
	private bool TargetDeath = false;
	private Folow_Point_Control folow_point_control;
	private Enemy_Control enemy_Control;
	private Enemy_Icon_Control enemy_IconControl;
    private Scene_Control sceneControl;
	private Transform lastView;	
	public bool fieldOfViewDraw;
	public LayerMask TargetLayer;
	public string targetTag;
    public string allyTag;
    [Range(0f, 100f)]
	public float viewRange;
	[Range(0f, 180f)]
	public float viewAngel;
	public Image fieldImage;
	[HideInInspector]
	public bool Death;
	public Transform myRotationTransform;
	void Start()
	{
		sceneControl = GameObject.FindWithTag("Respawn").GetComponent<Scene_Control>();
        folow_point_control = GetComponent<Folow_Point_Control>();
		enemy_Control = GetComponentInChildren<Enemy_Control>();
		enemy_IconControl = GetComponent<Enemy_Icon_Control>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Death)
		{
			fieldImage.enabled = false;	
			enabled = false;
		}
		if (fieldOfViewDraw && !Death) 
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
		if (enemy_IconControl !=null && enemy_IconControl.enabled)
			{
				if (enemy_IconControl.Hide == true)
					fieldImage.enabled = false;
				else
					fieldImage.enabled = true;
			}
		if (TargetDeath && !startCoroutine && Aim)
			{	
				StartCoroutine ("waitTarget");
			}
	}
	void FixedUpdate () 
	{
		Collider2D[] targetColliders = Physics2D.OverlapCircleAll(transform.position, viewRange, TargetLayer.value);
		foreach (var targetCollider in targetColliders) 
		{
			if (targetCollider.gameObject.tag == targetTag || targetCollider.gameObject.tag == allyTag) 
			{ 
				float distance =  Vector2.Distance(targetCollider.transform.position, myRotationTransform.position);
				Vector2 targetDir = targetCollider.transform.position - myRotationTransform.position;
				Vector2 forward = myRotationTransform.up;
				float angel = Vector2.Angle (targetDir, forward);
                if (distance <= viewRange && angel <= viewAngel)
                {
					Vector2 direction = targetCollider.transform.position - transform.position;
                    RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, Mathf.Infinity, TargetLayer.value);
                    if (hit.collider != null && hit.collider.gameObject.tag == targetTag)
                    {                        
						TargetDeath = hit.collider.gameObject.transform.Find("Soldier_default").GetComponent<Soldier_Control>().DeathTest;
                        folow_point_control.targetInSight = true;
						folow_point_control.AimTarget = targetCollider.transform;
						if(startCoroutine)
                        {
							folow_point_control.targetHide = false;
                            StopCoroutine("waitTarget");
                            startCoroutine = false;
                        }
						if (Aim)
						{
							enemy_Control.TargetIn = true;
						}
						if (hit.distance <= viewRange && !Aim)
                        {
							TargetDeath = false;
							Aim = true;
                            enemy_Control.AimWeapon();
                            enemy_Control.TargetIn = true;
                        }
                        if (hit.distance > viewRange && Aim)
                            {
                                enemy_Control.TargetIn = false;
								if (!startCoroutine)
                                StartCoroutine("waitTarget");
                            }
                        }
                        else
                        {
                            if (Aim && !startCoroutine && targetCollider.gameObject.tag != allyTag)
                            {
                                StartCoroutine("waitTarget");
                            }
                        }
                        if (hit.collider != null && hit.collider.gameObject.tag == allyTag && hit.collider.gameObject.GetComponentInChildren<Enemy_Control>().DeathTest == true && sceneControl.Alarm == false)
                        {                            
                            folow_point_control.StartCoroutine("EnemyAlert");
                        }
                }
                else
                {
					if (Aim && !startCoroutine && targetCollider.gameObject.tag != allyTag)
                    {
						StartCoroutine("waitTarget");
                    }
                }		
			}
		}
	}
	IEnumerator waitTarget()
		{					
			startCoroutine = true;
			enemy_Control.TargetIn = false;
			folow_point_control.targetHide = true;
			yield return new WaitForSeconds (5f);
			Aim = false;
			folow_point_control.targetHide = false;
			folow_point_control.targetInSight = false;
			enemy_Control.AimWeapon	();
			startCoroutine = false;
			TargetDeath = false;
		}
	}
}