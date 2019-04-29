using UnityEngine;
using UnityEngine.Events;

enum PlayerStatus { ALIVE, DEAD }

public class Player : MonoBehaviour
{
    public float lifetime = 0;
    public float initialLifeTime = 0;
    private Transform spawnLocation;
    
    public IWeapon activeWeapon;

    [Header("Weapon Settings")]
    public SpecialWeapon specialWeapon;
    public RangedWeapon rangedWeapon;
    public MeleeWeapon meleeWeapon;

    [Header("Player Settings")]
    [SerializeField] private PlayerStatus playerStatus;
    [SerializeField] private UnityEvent playerDeath;

    private PlayerController playerController;

    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
        spawnLocation = GameObject.FindWithTag("PlayerSpawnPoint").transform;
    }
    private void Start()
    {
        if (spawnLocation == null) {
            Debug.LogError("No object tagged as PlayerSpawnPoint in scene");
        }
        Reset();
    }

    public void Reset()
    {
        playerStatus = PlayerStatus.ALIVE;
        SwapActiveWeapon(3);
        lifetime = initialLifeTime;
        //transform.position = spawnLocation.position;
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

    public void AddLifetime(float delta)
    {
        lifetime += delta;
    }

    private void Update()
    {

        // Return if player is already dead
        if (playerStatus == PlayerStatus.ALIVE)
        {
            // Time based life drain
            DrainLifetime(Time.deltaTime);
        }
        
    }

    private void DrainLifetime(float amount)
    {

        lifetime -= amount;

        // Kill player if they died on this frame
        if (lifetime < 0f)
        {
            lifetime = 0f;
            playerStatus = PlayerStatus.DEAD;
            playerDeath.Invoke();
        }
    }
}
