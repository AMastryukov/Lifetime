using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SpawnManager : MonoBehaviour
{
    
    [SerializeField] public List<enemyInfo> enemies;
    private List<SpawnPoint> spawnPoints;
    [SerializeField] private float spawnRate;

    [SerializeField] private SpawnInfo[] spawnInfo;
    private int spawnsRemaining;
    private Coroutine spawner;
    private Dictionary<EnemyName, GameObject> enemyDic;

    [SerializeField] private UnityEvent SpawnComplete;

    // Start is called before the first frame update
    void Start()
    {
        GameObject[] points = GameObject.FindGameObjectsWithTag("SpawnPoint");
        spawnPoints = new List<SpawnPoint>();
        foreach (GameObject point in points) {
            spawnPoints.Add(point.GetComponent<SpawnPoint>());
        }
        enemyDic = new Dictionary<EnemyName, GameObject>();
        foreach (enemyInfo enemy in enemies) {
            enemyDic.Add(enemy.enemyName, enemy.enemyPrefab);
        }

        /*spawnsRemaining = 0;
        foreach (SpawnInfo spawn in spawnInfo)
        {
            spawnsRemaining += spawn.enemyCount;
        }
        Begin();*/
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Initialize(SpawnInfo[] spawnInfo, float spawnRate)
    {
        spawnsRemaining = 0;
        foreach (SpawnInfo spawn in spawnInfo) {
            spawnsRemaining += spawn.enemyCount;
        }

        this.spawnRate = spawnRate;
        this.spawnInfo = spawnInfo;
    }

    public void Begin() {

        print("Spawning Wave");
        spawner = StartCoroutine(SpawnCycle());
    }

    public void Begin(SpawnInfo[] spawns, float spawnRate)
    {
        Initialize(spawns, spawnRate);
        Begin();
    }

    private IEnumerator SpawnCycle()
    {
        while (spawnsRemaining > 0) {
            Spawn();
            yield return new WaitForSeconds(spawnRate);
        }
        SpawnComplete.Invoke();

    }

    public void Spawn() {
        int rand = Random.Range(0, spawnsRemaining - 1);
        foreach (SpawnInfo spawn in spawnInfo) {
            rand -= spawn.enemyCount;
            if (rand < 0) {
                
                int randSpawner = Random.Range(0, spawnPoints.Count - 1);
                SpawnPoint spawner = spawnPoints[randSpawner];
                while (!spawner.isActive()) {
                    randSpawner = (randSpawner + 1) % spawnPoints.Count;
                    spawner = spawnPoints[randSpawner];
                }
                GameObject enemyInstance = spawner.Spawn(enemyDic[spawn.enemyName]);

                if (!enemyInstance)
                {
                    //Debug.LogError("Was not able to spawn enemy");
                }
                else {
                    spawn.enemyCount--;
                    spawnsRemaining--;
                }
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
public class SpawnInfo
{
    public EnemyName enemyName;

    public int enemyCount;
}


