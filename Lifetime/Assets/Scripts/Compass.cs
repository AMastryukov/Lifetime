using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Compass : MonoBehaviour
{

    [SerializeField] private Image arrow;

    private GameObject player;
    private GameObject target;

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    void Update()
    {
        target = GameObject.FindWithTag("Enemy");

        if (target == null)
        {
            arrow.enabled = false;
            return;
        }
        
        arrow.enabled = true;

        transform.rotation = Quaternion.LookRotation(Vector3.back, - player.transform.position + target.transform.position);
    }
}
