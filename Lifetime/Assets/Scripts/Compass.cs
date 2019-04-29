using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Compass : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GameObject Target = GameObject.FindWithTag("Enemy");
        GameObject player = GameObject.FindWithTag("Player");

        if (Target == null) return;

        transform.rotation = Quaternion.LookRotation(Vector3.back, - player.transform.position + Target.transform.position);
    }
}
