using UnityEngine;
using UnityEngine.UI;
using System.Collections;
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
	private PlayerController soldierControl;


	// Use this for initialization
		void Awake () 
		{
			soldierControl = GetComponentInChildren<PlayerController> ();
		}

		void Update ()  
		{
			if (soldierControl.isDeath == true)
			{
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

				  }

				}
		} 
		
}
