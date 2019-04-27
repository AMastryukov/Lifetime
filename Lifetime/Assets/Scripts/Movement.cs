using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 1f;

    private Vector2 directionVector;
    private Rigidbody2D rigidBody;

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        rigidBody.MovePosition(rigidBody.position + directionVector * moveSpeed * Time.deltaTime);
    }

    public void SetDirectionVector(Vector2 newDirectionVector)
    {
        directionVector = newDirectionVector;
        directionVector.Normalize();
    }

    public void SetLookPosition(Vector3 newLookPosition)
    {
        transform.rotation = Quaternion.LookRotation(transform.position - newLookPosition, Vector3.forward);
        transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z);
    }
}
