﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] protected Rigidbody2D rigidBody;
    [SerializeField] protected float moveSpeed = 1f;

    protected Vector2 directionVector;
    private Vector3 lookPosition = Vector3.zero;

    private void FixedUpdate()
    {
        rigidBody.MovePosition(rigidBody.position + directionVector * moveSpeed * Time.deltaTime);
    }

    private void Update()
    {

        transform.rotation = Quaternion.LookRotation(Vector3.back, - transform.position + lookPosition);
        //transform.rotation = Quaternion.LookRotation(transform.position - lookPosition, Vector3.forward);
        //transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z);
    }

    public void SetDirectionVector(Vector2 newDirectionVector)
    {
        directionVector = newDirectionVector;
        directionVector.Normalize();
    }

    public void SetLookPosition(Vector3 newLookPosition)
    {
        lookPosition = newLookPosition;
    }
}
