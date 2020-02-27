using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    GameObject player;
    Vector3 offset;

    void Start()
    {
    
        offset = transform.position - player.transform.position;
    }
    void Update()
    {

    }
    void LateUpdate()
    {
        float newXPosition = player.transform.position.x - offset.x;
        float newZPosition = player.transform.position.z - offset.z;

        transform.position = new Vector3(newXPosition, transform.position.y, newXPosition);
    }
}
