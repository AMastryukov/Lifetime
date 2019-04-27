using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float moveSpeed = 1;
    private Vector2 movement;
    private Rigidbody2D rb;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    
    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed);
    }

    public void setMovement(Vector2 movement) {
        this.movement = movement;
        movement.Normalize();
    }
}
