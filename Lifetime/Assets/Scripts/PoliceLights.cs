using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliceLights : MonoBehaviour
{
    [SerializeField] private Light blue, red;
    [SerializeField] private float flashInterval = 0.5f;

    private void Start()
    {
        blue.enabled = true;
        red.enabled = false;

        StartCoroutine(FlashLights());
    }

    private IEnumerator FlashLights()
    {
        while(true)
        {
            blue.enabled = !blue.enabled;
            red.enabled = !red.enabled;

            yield return new WaitForSeconds(flashInterval);
        }
    }
}
