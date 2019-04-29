using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Transform playerTransform;
    [SerializeField] private float deadzone;

    private Vector3 offset;
    private Vector3 mousePosition;

    private void Awake()
    {
        playerTransform = GameObject.FindWithTag("Player").transform;
        
    }

    private void Start()
    {
        transform.position.Set(playerTransform.position.x, playerTransform.position.y, transform.position.z);
        offset = transform.position - playerTransform.position;
    }

    private void LateUpdate()
    {
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        transform.position = playerTransform.position + offset + new Vector3(
            -(transform.position.x - mousePosition.x) / deadzone,
            -(transform.position.y - mousePosition.y) / deadzone,
            0.0f);
    }
}