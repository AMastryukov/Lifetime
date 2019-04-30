using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] protected Rigidbody2D rigidBody;
    [SerializeField] public float moveSpeed = 1f;

    protected Vector2 directionVector;
    private Vector3 lookPosition = Vector3.zero;
    private float moveSpeedModifier = 0f;

    private void FixedUpdate()
    {
        rigidBody.MovePosition(rigidBody.position + directionVector * (moveSpeed + moveSpeedModifier) * Time.deltaTime);
    }

    private void Update()
    {

        transform.rotation = Quaternion.LookRotation(Vector3.back, - transform.position + lookPosition);
        //transform.rotation = Quaternion.LookRotation(transform.position - lookPosition, Vector3.forward);
        //transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z);
    }

    public void SetDirectionVector(Vector2 newDirectionVector, float extraSpeed)
    {
        directionVector = newDirectionVector;
        directionVector.Normalize();

        moveSpeedModifier = extraSpeed;
    }

    public void SetLookPosition(Vector3 newLookPosition)
    {
        lookPosition = newLookPosition;
    }
}
