using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    private void Start()
    {
        transform.rotation = Quaternion.Euler(0, 90, 0);
    }
}
