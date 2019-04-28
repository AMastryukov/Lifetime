using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : Movement
{

    // Update is called once per frame
    void FixedUpdate()
    {

        rigidBody.AddForce(directionVector * moveSpeed);

    }
}
