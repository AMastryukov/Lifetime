using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private MovementControls movementControls;

    private float horizontalInput, verticalInput;
    
    void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        movementControls.SetDirectionVector(new Vector2(horizontalInput, verticalInput));
    }
}
