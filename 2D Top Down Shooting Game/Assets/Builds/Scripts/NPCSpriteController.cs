using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCSpriteController : MonoBehaviour
{
    public Transform targetToLookAt;
    // Update is called once per frame
    void FixedUpdate()
    {
        transform.LookAt(targetToLookAt);
    }
}
