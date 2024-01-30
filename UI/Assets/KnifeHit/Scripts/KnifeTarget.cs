using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public partial class KnifeTarget : MonoBehaviour
{
    private float rotateSpeed = 100;
    private Vector3 rotateAngle = Vector3.forward;

    private SpriteRenderer spriteRenderer;

    private IEnumerator Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        
        while (true)
        {
            int time = Random.Range(1, 5);

            yield return new WaitForSeconds(time);

            int speed = Random.Range(10, 300);
            int dir = Random.Range(0, 2);

            rotateSpeed = speed;
            rotateAngle = new Vector3(0, 0, dir * 2 - 1);
        }
    }

    private void Update()
    {
        transform.Rotate(rotateAngle * (rotateSpeed * Time.deltaTime));
    }

    public void Hit()
    {
        StopCoroutine("OnHit");
        StartCoroutine("OnHit");
    }

    private IEnumerator OnHit()
    {
        spriteRenderer.color = new Color(0.15f, 0.15f, 0.15f, 1);

        yield return new WaitForSeconds(0.1f);
        
        spriteRenderer.color = Color.black;
    }
}
