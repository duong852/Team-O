using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController : MonoBehaviour
{
    public UnityEngine.AI.NavMeshAgent navMeshAgent;
    public Transform Destination;
    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent.destination = Destination.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
