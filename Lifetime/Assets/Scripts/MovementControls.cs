using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementControls : MonoBehaviour
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
}
