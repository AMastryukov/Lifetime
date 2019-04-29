using UnityEngine;
using UnityEngine.Events;

enum PlayerStatus { ALIVE, DEAD }

public class Player : MonoBehaviour, IDamageable
{
    public Animator playerAnimator;

    public float lifetime = 0;
    public float initialLifeTime = 0;
    private Transform spawnLocation;

    [Header("Attribute Modifiers")]
    public float damageModifier = 1;
    public float luckModifier = 1;
    public float staminaModifier = 1;
    public float knockbackModifier = 1;
    public float speedModifier = 1;

    public Weapon defaultMeleeWeapon;
    public Weapon activeWeapon;

    [Header("Weapon Settings")]
    public Weapon specialWeapon;
    public Weapon rangedWeapon;
    public Weapon meleeWeapon;

    [Header("Player Settings")]
    [SerializeField] private PlayerStatus playerStatus;
    [SerializeField] private UnityEvent playerDeath;

    private PlayerController playerController;

    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
        //spawnLocation = GameObject.FindWithTag("PlayerSpawnPoint").transform;
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
        lifetime = initialLifeTime;


        equipMeleeWeapon(defaultMeleeWeapon);
        Destroy(rangedWeapon);
        Destroy(specialWeapon);
        rangedWeapon = null;
        specialWeapon = null;

        SwapActiveWeapon(3);


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

        UpdateIdleAnimation();
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

    public void DrainLifetime(float amount)
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

    public void equipMeleeWeapon(GameObject weapon) {
        Weapon weaponInstance = Instantiate(weapon, transform).GetComponent<Weapon>();
        if(meleeWeapon != null) Destroy(meleeWeapon.gameObject);
        meleeWeapon = weaponInstance;
        meleeWeapon.player = this;

        UpdateIdleAnimation();
    }

    public void equipMeleeWeapon(Weapon weapon)
    {
        Weapon weaponInstance = Instantiate(weapon, transform).GetComponent<Weapon>();
        if (meleeWeapon != null) Destroy(meleeWeapon.gameObject);
        meleeWeapon = weaponInstance;
        meleeWeapon.player = this;

        UpdateIdleAnimation();
    }

    public void equipRangeWeapon(GameObject weapon) {
        Weapon weaponInstance = Instantiate(weapon, transform).GetComponent<Weapon>();
        if (rangedWeapon != null) Destroy(rangedWeapon.gameObject);
        rangedWeapon = weaponInstance;
        meleeWeapon.player = this;

        UpdateIdleAnimation();
    }

    public void equipSpecialWeapon(GameObject weapon) {
        Weapon weaponInstance = Instantiate(weapon, transform).GetComponent<Weapon>();
        if (specialWeapon != null) Destroy(specialWeapon.gameObject);
        specialWeapon = weaponInstance;
        meleeWeapon.player = this;

        UpdateIdleAnimation();
    }
    
    private void UpdateIdleAnimation()
    {
        if (activeWeapon is Melee)
        {
            playerAnimator.Play("melee-idle");
        }
        else if (activeWeapon is Pistol)
        {
            playerAnimator.Play("pistol-idle");
        }
        else if (activeWeapon is AssaultRifle)
        {
            playerAnimator.Play("auto-idle");
        }
        else
        {
            playerAnimator.Play("melee-idle");
        }
    public void TakeDamage(float damage)
    {
        lifetime -= damage;
    }

    public void TakeCriticalDamage(float Damage)
    {
        lifetime -= Damage;
    }
}
