using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[ExecuteInEditMode]
public class Enemy_FieldOfView : MonoBehaviour {

	private bool startCoroutine = false;
	private bool Aim = false;
	private bool TargetDeath = false;
	private Folow_Point_Control folow_point_control;
	private Enemy_Control enemy_Control;
    private Scene_Controller sceneControl;
	private Transform lastView;	

	public bool fieldOfViewDraw;
	public LayerMask TargetLayer;
	public string targetTag;
    public string allyTag;
    [Range(0f, 100f)]
	public float viewRange;
	[Range(0f, 180f)]
	public float viewAngel;
	[HideInInspector]
	public bool Death;
		
	public Transform myRotationTransform;


		// Use this for initialization
	void Start()
		{
            // === SET START PARAMETERS === //
            sceneControl = GameObject.FindWithTag("Respawn").GetComponent<Scene_Controller>();
            folow_point_control = GetComponent<Folow_Point_Control>();
			enemy_Control = GetComponentInChildren<Enemy_Control>();
		}
	
	// Update is called once per frame
	void Update () 
		{
			if (Death) 
			{
				enabled = false;
			}

			if (TargetDeath && !startCoroutine && Aim)
			{	
				StartCoroutine ("waitTarget");
			}
		}


	void FixedUpdate () 
		{
			// === SYSTEM FIELD OF VIEW === //
			Collider2D[] targetColliders = Physics2D.OverlapCircleAll(transform.position, viewRange, TargetLayer.value);
			foreach (var targetCollider in targetColliders) 
			{
				if (targetCollider.gameObject.tag == targetTag || targetCollider.gameObject.tag == allyTag) 
				{ 
					float distance =  Vector2.Distance(targetCollider.transform.position, myRotationTransform.position);
					
					Vector2 targetDir = targetCollider.transform.position - myRotationTransform.position;
					Vector2 forward = myRotationTransform.up;
					float angel = Vector2.Angle (targetDir, forward);

                    //Debug.Log (angel.ToString ());

                    if (distance <= viewRange && angel <= viewAngel)
                    {
                        Vector2 direction = targetCollider.transform.position - transform.position;
                        //Debug.DrawRay(transform.position, direction, Color.green);						
                        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, Mathf.Infinity, TargetLayer.value);

                        //Debug.Log(hit.collider);

                        if (hit.collider != null && hit.collider.gameObject.tag == targetTag)
                        {                        
                            TargetDeath = hit.collider.gameObject.transform.Find("Player").GetComponent<PlayerController>().isDeath;
                            folow_point_control.targetInSight = true;
                            folow_point_control.AimTarget = targetCollider.transform;

                            if (startCoroutine)
                            {
                                folow_point_control.targetHide = false;
                                StopCoroutine("waitTarget");
                                startCoroutine = false;
                            }

                            if (Aim)
                                enemy_Control.TargetIn = true;

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

                        if (hit.collider != null && hit.collider.gameObject.tag == allyTag && hit.collider.gameObject.GetComponentInChildren<Enemy_Control>().DeathTest == true && sceneControl.alarmOn == false)
                        {                            
                            //sceneControl.Alarm = true;
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


		// === WAIT IF LOSE TARGET === //
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
