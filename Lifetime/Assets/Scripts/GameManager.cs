using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private bool readyForNextWave;
    [SerializeField] private WaveManager waveManager;
    // Start is called before the first frame update
    void Start()
    {
        readyForNextWave = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (readyForNextWave) {
            if (!waveManager.SpawnNextWave()) {
                print("GAME OVER");
            }
            readyForNextWave = false;
        }
    }

    public void WaveOver() {
        readyForNextWave = true;
    }
}
