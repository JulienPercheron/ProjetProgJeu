using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMovable : MonoBehaviour
{
    public Transform startPoint;
    public Transform endPoint;

    public Rigidbody objectToMove;

    public float MoveSpeed = 0.8f;
    
    private Vector3 _pointToFollow;

    private void Start()
    {
        objectToMove.position = startPoint.position;
        _pointToFollow = endPoint.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(objectToMove.position, _pointToFollow) > MoveSpeed)
        {
            objectToMove.position = Vector3.Lerp(objectToMove.position, _pointToFollow, Time.deltaTime * 0.8f);
        }
        else
        {
            _pointToFollow = (_pointToFollow == startPoint.position) ? endPoint.position : startPoint.position;
        }
        
    }
}
