using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLockZ : MonoBehaviour
{
    public float lockedZPosition = -10f; // Set this to your desired Z position

    void LateUpdate()
    {
        Vector3 position = transform.position;
        position.z = lockedZPosition;
        transform.position = position;
    }
}