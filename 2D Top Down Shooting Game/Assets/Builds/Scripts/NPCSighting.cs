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
	private NPCController npcController;
	private Scene_Controller sceneControl;
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
		npcController = GetComponent<NPCController>();
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
						Debug.DrawRay(transform.position, direction, Color.green);
						GameObject playerGroup = GameObject.Find("PlayerGroup");
						TargetDeath = playerGroup.GetComponent<PlayerController>().isDeath;
						patrolController.targetInSight = true;
						patrolController.AimTarget = targetCollider.transform;
						if (startCoroutine)
						{
							patrolController.targetHide = false;
							StopCoroutine("waitTarget");
							startCoroutine = false;
						}
						if (Aim) 
						{
							npcController.TargetIn = true;
							npcController.canShoot = true;
						}
						if (hit.distance <= viewRange && !Aim)
						{
							TargetDeath = false;
							Aim = true;
							npcController.TargetIn = true;
							npcController.canShoot = true;
						}
						if (hit.distance > viewRange && Aim)
						{
							npcController.TargetIn = false;
							npcController.canShoot = false;

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
					if (hit.collider != null && hit.collider.gameObject.tag == allyTag && hit.collider.gameObject.GetComponentInChildren<NPCController>().isDeath == true && sceneControl.alarmOn == false)
					{
						sceneControl.alarmOn = true;
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
		npcController.TargetIn = false;
		npcController.canShoot = false;
		patrolController.targetHide = true;
		yield return new WaitForSeconds(5f);
		Aim = false;
		patrolController.targetHide = false;
		patrolController.targetInSight = false;
		startCoroutine = false;
		TargetDeath = false;
	}
}
