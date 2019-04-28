using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : Movement
{

    // Update is called once per frame
    void FixedUpdate()
    {
        //rigidBody.MovePosition(rigidBody.position + directionVector * moveSpeed * Time.deltaTime);

        rigidBody.AddForce(directionVector * moveSpeed);

        //rigidBody.velocity += (directionVector * Time.deltaTime * moveSpeed);
        //rigidBody.velocity.Normalize();
        //rigidBody.velocity *= moveSpeed;
    }
}
