using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Compass : MonoBehaviour
{

    [SerializeField] private Image arrow;

    // Update is called once per frame
    void Update()
    {
        GameObject Target = GameObject.FindWithTag("Enemy");
        GameObject player = GameObject.FindWithTag("Player");

        if (Target == null)
        {
            arrow.enabled = false;
            return;
        }
        
        arrow.enabled = true;

        transform.rotation = Quaternion.LookRotation(Vector3.back, - player.transform.position + Target.transform.position);
    }
}
