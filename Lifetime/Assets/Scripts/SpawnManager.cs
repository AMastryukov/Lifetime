using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SpawnManager : MonoBehaviour
{
    
    [SerializeField] public List<enemyInfo> enemies;
    private List<SpawnPoint> spawnPoints;
    private float spawnRate;

    private SpawnInfo[] spawnInfo;
    private int spawnsRemaining;
    private Coroutine spawner;
    private Dictionary<EnemyName, GameObject> enemyDic;
    private float healthMultiplier;

    [SerializeField] private UnityEvent SpawnComplete;

    private void Awake()
    {
        GameObject[] points = GameObject.FindGameObjectsWithTag("SpawnPoint");
        spawnPoints = new List<SpawnPoint>();
        foreach (GameObject point in points)
        {
            spawnPoints.Add(point.GetComponent<SpawnPoint>());
        }
        enemyDic = new Dictionary<EnemyName, GameObject>();
        foreach (enemyInfo enemy in enemies)
        {
            enemyDic.Add(enemy.enemyName, enemy.enemyPrefab);
        }
    }


    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Initialize(SpawnInfo[] spawnInfo, float spawnRate, float healthMultiplier)
    {
        this.spawnInfo = new SpawnInfo[spawnInfo.Length];
        spawnsRemaining = 0;
        for (int i =0; i < spawnInfo.Length; i++) {
            spawnsRemaining += spawnInfo[i].enemyCount;
            this.spawnInfo[i].enemyCount = spawnInfo[i].enemyCount;
            this.spawnInfo[i].enemyName = spawnInfo[i].enemyName;
        }

        this.healthMultiplier = healthMultiplier;
        this.spawnRate = spawnRate;
    }

    public int Begin() {
        int startingSpawnNumber = spawnsRemaining;
        spawner = StartCoroutine(SpawnCycle());
        return startingSpawnNumber;
    }

    public int Begin(SpawnInfo[] spawns, float spawnRate, float healthMultiplier)
    {
        Initialize(spawns, spawnRate, healthMultiplier);
        return Begin();
    }

    public void StopSpawning()
    {
        StopCoroutine(spawner);
    }

    private IEnumerator SpawnCycle()
    {
        while (spawnsRemaining > 0) {
            Spawn();
            yield return new WaitForSeconds(1/spawnRate);
        }
        SpawnComplete.Invoke();

    }

    public void Spawn() {
        int rand = Random.Range(0, spawnsRemaining);
        for (int i = 0; i < spawnInfo.Length; i++) {
            rand -= spawnInfo[i].enemyCount;
            if (rand < 0) {
                int randSpawner = Random.Range(0, spawnPoints.Count);
                SpawnPoint spawner = spawnPoints[randSpawner];
                GameObject enemyInstance = spawner.Spawn(enemyDic[spawnInfo[i].enemyName]);

                if (!enemyInstance)
                {
                    //Debug.LogError("Was not able to spawn enemy");
                }
                else {
                    Enemy enemy = enemyInstance.GetComponent<Enemy>();
                    if (enemy)
                    { 
                        enemy.health *= healthMultiplier;
                    }
                    spawnInfo[i].enemyCount--;
                    spawnsRemaining--;
                }
                return;
            }
        }
    }


}

public enum EnemyName { EASY, MEDIUM, HARD };

[System.Serializable]
public struct enemyInfo
{
    public EnemyName enemyName;
    public GameObject enemyPrefab;
}

[System.Serializable]
public struct SpawnInfo
{
    public EnemyName enemyName;

    public int enemyCount;
}


