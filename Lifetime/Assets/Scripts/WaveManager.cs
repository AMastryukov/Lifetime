using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WaveManager : MonoBehaviour
{

    private bool checkForEnemies = false;
    [SerializeField] private SpawnManager spawnManager;
    [SerializeField] private Wave[] Waves;
    [SerializeField] private int currentWave;
    [SerializeField] private bool waveInProgress;
    [SerializeField] private UnityEvent WaveOver;

    // Start is called before the first frame update
    void Start()
    {
        print(Waves.Length);
        currentWave = 0;
        waveInProgress = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (checkForEnemies) {
            if (GameObject.FindWithTag("Enemy") == null) {
                WaveEnded();
                checkForEnemies = false;
            }
        }
    }

    // checkForEnemies used to check if the wave is over
    public void CheckForEnemies() {
        checkForEnemies = true;
    }

    // Return false if no more waves
    public bool SpawnNextWave() {
        if (currentWave == Waves.Length) {
            return false;
        }
        spawnManager.Begin(Waves[currentWave].spawns, Waves[currentWave].rate);
        waveInProgress = true;
        return true;
    }

    public void WaveEnded() {
        
        waveInProgress = false;
        currentWave++;
        WaveOver.Invoke();
        ;
    }

    

}

[System.Serializable]
public class Wave {

    [SerializeField] public SpawnInfo[] spawns;
    [SerializeField] public float rate;

}
