using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{

    private bool active;
    private BoxCollider2D SpawnBlockArea;

    private void Awake()
    {
        active = true;
        SpawnBlockArea = GetComponent<BoxCollider2D>();
    }


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool isActive() {
        return active;
    }

    //Spawn enemy if active, and return reference to created GameObject
    public GameObject Spawn(GameObject enemy) {

        GameObject spawnedEnemy = null;

        //print(this.name + " is trying to spawn enemy");
        if (active) {
            float randx = Random.Range(-0.5f*SpawnBlockArea.size.x, 0.5f* SpawnBlockArea.size.x);
            float randy = Random.Range(-0.5f * SpawnBlockArea.size.y, 0.5f * SpawnBlockArea.size.y);
            spawnedEnemy = Instantiate(enemy, new Vector3(randx, randy, 0) + transform.position, Quaternion.identity);
        }

        return spawnedEnemy;
    }

    // Deactivate spawner when player enters
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            active = false;
        }
    }

    // Activate spawner when player exits
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            active = true;
        }
    }
}
