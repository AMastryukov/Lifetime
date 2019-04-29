using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    [Header("Manager Object References")]
    [Tooltip("In charge of spawning waves")] [SerializeField] private WaveManager waveManager;



    [Header("User Interfaces")]
    [SerializeField] private ShopPanel shopPanel;
    [SerializeField] private HUDPanel hudPanel;
    [SerializeField] private GameOverPanel gameOverPanel;


    /*
     Private variables
         */
    [Header("For debug")]
    [SerializeField] private int enemiesRemaining;


    private Player player;

    /*
     MonoBehaviour
         */
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

    }
    void Start()
    {

        ResetGame();
        StartGame();
    }

    /*
     CORE GAME MANAGEMENT METHODS (PRIVATE)
         */

    private void StartGame()
    {
        StartNextWave();
    }

    private void ResetGame()
    {
        ClearDisplay();
        DisablePlayer();
        waveManager.Reset();
        // Also reset player position to start
    }

    private void GameOver()
    {
        print("GameOver");
        DisablePlayer();
        ClearDisplay();
        EnableGameOverScreen();
        
    }

    /*
     This is where the next wave begins. The waveManager knows the following information:
        1. How many of each type of enemy to spawn
        2. Where to spawn enemies (through a SpawnManager)
        3. When the wave is over (see this.WaveOver())

    Any housekeeping before the round starts should be done here.
         */
    private void StartNextWave()
    {
        EnablePlayer();
        ClearDisplay();
        EnableHUD();
        enemiesRemaining = waveManager.SpawnNextWave();
        print(enemiesRemaining);
    }

    /*
     PUBLIC METHODS. MEANT TO BE CALLED FROM OUTSIDE BY EVENTS.
         */

    /*
     Signals that the all the enemies in the current wave have been slayed.
     
     NOTE: You don't have to worry about the case where there are no enemies alive, but
     enemies are still in the process of being spawned. This is called when there are no more enemies
     to be spawned (and of course, no enemies alive).
         */
    public void WaveOver() {

        if (waveManager.LastWaveEnded())
        {
            GameOver();
            return;
        }

        EnableShop();
        DisablePlayer();
    }

    /*
     When player kills enemy, handled here.
         */
    public void RegisterEnemyDeath() {
        enemiesRemaining--;
    }

    /*
     Basic Restart. ResetGame() then StartGame()
         */
    public void RestartGame() {
        ResetGame();
        StartGame();
    }

    /*
     HOUSEKEEPING. MASTERHAND. EXPLICIT CONTROL. THAT KINDA STUFF (ALSO PRIVATE);
         */

    private void DisablePlayer() {
        player.GetComponent<PlayerController>().enabled = false;
        player.GetComponent<Movement>().enabled = false;
    }

    private void EnablePlayer()
    {
        player.GetComponent<PlayerController>().enabled = true;
        player.GetComponent<Movement>().enabled = true;
    }

    private void DisableShop() {
        shopPanel.ClosePanel();
    }

    private void EnableShop()
    {
        ClearDisplay();
        shopPanel.OpenPanel();
    }

    private void DisableGameOverScreen()
    {
        gameOverPanel.ClosePanel();
    }

    private void EnableGameOverScreen()
    {
        ClearDisplay();
        gameOverPanel.OpenPanel();
    }

    private void DisableHUD()
    {
        hudPanel.ClosePanel();
    }

    private void EnableHUD()
    {
        ClearDisplay();
        hudPanel.OpenPanel();
    }

    private void ClearDisplay() {
        DisableHUD();
        DisableGameOverScreen();
        DisableShop();
    }
}
