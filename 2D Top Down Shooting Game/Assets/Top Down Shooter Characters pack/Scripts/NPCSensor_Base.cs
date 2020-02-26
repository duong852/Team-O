using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NPCSensor_Base : MonoBehaviour
{
	public NPC_Enemy npcBase;
	protected List<GameObject> sensedObjects = new List<GameObject>();

	void Start()
	{
		if (npcBase == null)
			npcBase = gameObject.GetComponent<NPC_Enemy>();
		StartSensor();
	}

	void Update()
	{
		UpdateSensor();
	}
	protected virtual void StartSensor() { }
	protected virtual void UpdateSensor() { }

	protected List<GameObject> GetSensedObjects()
	{
		return sensedObjects;
	}

}

