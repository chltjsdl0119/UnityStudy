using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 20;
    [SerializeField] private Vector3 moveDirction = Vector3.up;

    private void Update()
    {
        transform.position += moveDirction * moveSpeed * Time.deltaTime;
    }

    public void MoveTo(Vector3 direction)
    {
        moveDirction = direction;
    }
}
