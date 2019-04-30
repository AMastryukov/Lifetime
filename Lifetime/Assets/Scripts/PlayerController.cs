using UnityEngine;

struct PlayerInput
{
    // WASD or whatever
    public float horizontalInput;
    public float verticalInput;

    public Vector3 mousePosition;

    // Mouse buttons
    public bool mouseLeftClick;
    public bool mouseLeftHeld;
    public bool mouseRightClick;
    public bool mouseRightHeld;

    // Number keys at the top
    public bool numberKey1;
    public bool numberKey2;
    public bool numberKey3;
}

public class PlayerController : MonoBehaviour
{
    private Movement playerMovement;
    private Player player;

    private PlayerInput playerInput;

    private Vector3 playerDirection = Vector3.zero;

    private void Start()
    {
        playerMovement = GetComponent<Movement>();
        player = GetComponent<Player>();
    }

    void Update()
    {
        UpdateInputs();
        UpdatePlayer();
    }

    private void UpdateInputs()
    {
        // Fetch horizontal and vertical axes input
        playerInput.horizontalInput = Input.GetAxisRaw("Horizontal");
        playerInput.verticalInput = Input.GetAxisRaw("Vertical");

        playerDirection = new Vector2(playerInput.horizontalInput, playerInput.verticalInput).normalized;

        // Fetch mouse position in world coordinates
        playerInput.mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Check if player is pressing mouse buttons
        playerInput.mouseLeftClick = Input.GetMouseButtonDown(0);
        playerInput.mouseLeftHeld = Input.GetMouseButton(0);
        playerInput.mouseRightClick = Input.GetMouseButtonDown(1);
        playerInput.mouseRightHeld = Input.GetMouseButton(0);

        // Get number keys on the alphanumeric keyboard
        playerInput.numberKey1 = Input.GetKeyDown(KeyCode.Alpha1);
        playerInput.numberKey2 = Input.GetKeyDown(KeyCode.Alpha2);
        playerInput.numberKey3 = Input.GetKeyDown(KeyCode.Alpha3);
    }

    private void UpdatePlayer()
    {
        // Update player movement look position and direction
        playerMovement.SetLookPosition(playerInput.mousePosition);
        playerMovement.SetDirectionVector(playerDirection, player.speedModifier);

        // When the player presses left click, fire their active weapon
        if ((player.activeWeapon.isAutomatic && playerInput.mouseLeftHeld) || (!player.activeWeapon.isAutomatic && playerInput.mouseLeftClick)) {
            player.activeWeapon.Fire(
                transform.position,
                playerInput.mousePosition - transform.position,
                player.damageModifier,
                player.knockbackModifier);
        }

        // Swap active weapon
        if (playerInput.numberKey1)
        {
            player.SwapActiveWeapon(1);
        }
        else if (playerInput.numberKey2)
        {
            player.SwapActiveWeapon(2);
        }
        else if (playerInput.numberKey3)
        {
            player.SwapActiveWeapon(3);
        }
    }
}
