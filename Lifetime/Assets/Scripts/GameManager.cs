using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private bool readyForNextWave;
    [SerializeField] private WaveManager waveManager;

    [SerializeField] private ShopPanel shopPanel;
    //[SerializeField] private HUDPanel hudPanel;
    //[SerializeField] private GameOverPanel gameOverPanel;

    private Player player;
    // Start is called before the first frame update
    void Start()
    {
        readyForNextWave = true;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        DisableShop();

        ResetGame();
        StartGame();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void WaveOver() {

        readyForNextWave = true;

        if (waveManager.LastWaveEnded())
        {
            GameOver();
            return;
        }

        EnableShop();
        DisablePlayer();
    }

    public void StartGame() {
        StartNextWave();
    }

    public void ResetGame() {
        waveManager.StopWave();
    }

    public void StartNextWave() {
        DisableShop();
        EnablePlayer();
        waveManager.SpawnNextWave();
        readyForNextWave = false;
    }

    public void DisablePlayer() {
        player.GetComponent<PlayerController>().enabled = false;
        player.GetComponent<Movement>().enabled = false;
    }

    public void EnablePlayer()
    {
        player.GetComponent<PlayerController>().enabled = true;
        player.GetComponent<Movement>().enabled = true;
    }

    public void DisableShop() {
        shopPanel.GetComponent<Canvas>().enabled = false;
    }

    public void EnableShop()
    {
        shopPanel.GetComponent<Canvas>().enabled = true;
    }

    public void GameOver() {
        print("GameOver");
        DisablePlayer();
    }
}
