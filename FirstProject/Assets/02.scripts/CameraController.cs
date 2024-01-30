using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Vector3 _offset;
    private Vector3 _angle;

    private void Awake()
    {
        _offset = transform.position;
        _angle = transform.eulerAngles;
    }

    // 매프레임마다 호출이되어야하지만 중요도가 떨어질 때 사용.
    private void LateUpdate()
    {
        Transform target = RaceManager.instance.lead.transform;

        if (target != null)
        {
            transform.position = target.position + _offset;
        }
    }
}
