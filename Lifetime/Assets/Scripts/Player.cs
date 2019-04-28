using UnityEngine;
using UnityEngine.Events;

enum PlayerStatus { ALIVE, DEAD }

public class Player : MonoBehaviour
{
    public float lifetime = 5f;
    
    public IWeapon activeWeapon;

    [Header("Weapon Settings")]
    public SpecialWeapon specialWeapon;
    public RangedWeapon rangedWeapon;
    public MeleeWeapon meleeWeapon;

    [Header("Player Settings")]
    [SerializeField] private PlayerStatus playerStatus = PlayerStatus.ALIVE;
    [SerializeField] private UnityEvent playerDeath;

    private PlayerController playerController;

    private void Start()
    {
        playerController = GetComponent<PlayerController>();
        SwapActiveWeapon(3);
        
    }

    public void SwapActiveWeapon(int weaponSlot)
    {
        switch(weaponSlot)
        {
            // Swap to special weapon
            case 1:
                if (specialWeapon)
                {
                    activeWeapon = specialWeapon;
                }
                break;

            // Swap to ranged weapon
            case 2:
                if (rangedWeapon)
                {
                    activeWeapon = rangedWeapon;
                }
                break;

            // Swap to melee weapon
            case 3:
                activeWeapon = meleeWeapon;
                break;

            default:
                activeWeapon = meleeWeapon;
                break;
        }
    }

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
