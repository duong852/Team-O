using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[ExecuteInEditMode]
public class NPCSighting : MonoBehaviour
{
	private bool startCoroutine = false;
	private bool Aim = false;
	private bool TargetDeath = false;
	private NPCPatrolController patrolController;
	private NPCController enemy_Control;
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
		sceneControl = GameObject.FindWithTag("Respawn").GetComponent<Scene_Controller>();
		patrolController = GetComponent<NPCPatrolController>();
		enemy_Control = GetComponentInChildren<NPCController>();
	}

	// Update is called once per frame
	void Update()
	{
		if (Death)
		{
			enabled = false;
		}
		if (TargetDeath && !startCoroutine && Aim)
		{
			StartCoroutine("waitTarget");
		}
	}
	void FixedUpdate()
	{
		Collider2D[] targetColliders = Physics2D.OverlapCircleAll(transform.position, viewRange, TargetLayer.value);
		foreach (var targetCollider in targetColliders)
		{
			if (targetCollider.gameObject.tag == targetTag || targetCollider.gameObject.tag == allyTag)
			{
				float distance = Vector2.Distance(targetCollider.transform.position, myRotationTransform.position);
				Vector2 targetDir = targetCollider.transform.position - myRotationTransform.position;
				Vector2 forward = myRotationTransform.up;
				float angel = Vector2.Angle(targetDir, forward);
				if (distance <= viewRange && angel <= viewAngel)
				{
					Vector2 direction = targetCollider.transform.position - transform.position;						
					RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, Mathf.Infinity, TargetLayer.value);
					if (hit.collider != null && hit.collider.gameObject.tag == targetTag)
					{
						TargetDeath = hit.collider.gameObject.transform.Find("Player").GetComponent<PlayerController>().isDeath;
						patrolController.targetInSight = true;
						patrolController.AimTarget = targetCollider.transform;
						if (startCoroutine)
						{
							patrolController.targetHide = false;
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
					if (hit.collider != null && hit.collider.gameObject.tag == allyTag && hit.collider.gameObject.GetComponentInChildren<NPCController>().DeathTest == true && sceneControl.alarmOn == false)
					{
						//sceneControl.Alarm = true;
						patrolController.StartCoroutine("EnemyAlert");
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
		patrolController.targetHide = true;
		yield return new WaitForSeconds(5f);
		Aim = false;
		patrolController.targetHide = false;
		patrolController.targetInSight = false;
		enemy_Control.AimWeapon();
		startCoroutine = false;
		TargetDeath = false;
	}
}
