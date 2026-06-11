using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowHook : MonoBehaviour
{
    [SerializeField] private Transform hook;

    void LateUpdate()
    {
        transform.position = new Vector3(transform.position.x,hook.position.y,transform.position.z);
    }
}
