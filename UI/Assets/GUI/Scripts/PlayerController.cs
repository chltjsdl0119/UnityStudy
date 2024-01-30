using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5.0f;
    [SerializeField] private float jumpPower = 10.0f;
    
    private bool inputRight;
    private bool inputLeft;
    private bool inputJump;

    private Rigidbody2D _rigidbody2D;

    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            inputRight = true;
        }
        else
        {
            inputRight = false;
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            inputLeft = true;
        }
        else
        {
            inputLeft = false;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            inputJump = true;
        }
        
    }

    private void FixedUpdate()
    {
        if (inputRight)
        {
            inputRight = false;
            _rigidbody2D.velocity = new Vector2(moveSpeed, _rigidbody2D.velocity.y);
        }
        if (inputLeft)
        {
            inputLeft = false;
            _rigidbody2D.velocity = new Vector2(-moveSpeed, _rigidbody2D.velocity.y);

        }

        if (inputJump)
        {
            inputJump = false;
            _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, jumpPower);
        }
    }
    
}
