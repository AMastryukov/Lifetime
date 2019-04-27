using UnityEngine;
using UnityEngine.Events;

enum PlayerStatus { ALIVE, DEAD }

public class Player : MonoBehaviour
{
    public float lifetime = 5f;
    public MeleeWeapon meleeWeapon;
    
    [SerializeField] private PlayerController playerController;
    [SerializeField] private PlayerStatus playerStatus = PlayerStatus.ALIVE;
    [SerializeField] private UnityEvent playerDeath;

    public void AdjustLifetime(float delta)
    {
        lifetime += delta;
    }

    private void Update()
    {
        DrainLifetime();
    }

    private void DrainLifetime()
    {
        // Return if player is already dead
        if (playerStatus == PlayerStatus.DEAD)
        {
            return;
        }

        // Decrease lifetime
        lifetime -= Time.deltaTime;

        // Kill player if they died on this frame
        if (lifetime < 0f)
        {
            lifetime = 0f;
            playerStatus = PlayerStatus.DEAD;
            playerDeath.Invoke();
        }
    }
}
