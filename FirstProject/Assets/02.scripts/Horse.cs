using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Horse : MonoBehaviour
{
    public float totalDistance => Vector3.Distance(transform.position, _startPos);

    public bool doMove;
    
    // [SerializeField] : 해당 필드를 유니티 에디터의 인스펙터창에 노출시키기 위한 Attribute.
    // 객체를 가져다가 특정 데이터 포멧으로 바꾸는 것을 직렬화라고 한다.
    [SerializeField] private float _speed;
    [Range(0.0f, 1.0f)] [SerializeField] private float _stability;

    private float _speedModified;
    private float _speedModifyingDistance = 1.0f;
    private float _speedModifyedDistanceMark;
    private Vector3 _startPos;

    private void Awake()
    {
        _startPos = transform.position;
        _speedModified = _speed * Random.Range(_stability, 1.0f);
    }

    private void Start()
    {
        RaceManager.instance.Resister(this);
    }


    private void FixedUpdate() 
    {
        if (doMove)
        {
            if (totalDistance - _speedModifyedDistanceMark > _speedModifyingDistance)
            {
                _speedModified = _speed * Random.Range(_stability, 1.0f);
                _speedModifyedDistanceMark = totalDistance;
            }
            
            Move();
        }
    }

    // 거리 = 속력 * 시간
    // 한 프레임당 거리 = 속력 * 한 프레임당 시간
    private void Move()
    {
        transform.position += Vector3.forward * _speedModified * Time.fixedDeltaTime;
    }
}
