using UnityEngine;

struct PlayerInput
{
    public float horizontalInput;
    public float verticalInput;

    public Vector3 mousePosition;

    public bool mouseLeftClick;
    public bool mouseLeftHeld;
    public bool mouseRightClick;
    public bool mouseRightHeld;
}

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Movement playerMovement;
    [SerializeField] private Player player;

    private PlayerInput playerInput;

    private Vector3 playerDirection = Vector3.zero;
    
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
    }

    private void UpdatePlayer()
    {
        // Update player movement look position and direction
        playerMovement.SetLookPosition(playerInput.mousePosition);
        playerMovement.SetDirectionVector(playerDirection);

        // When the player presses left click, fire their active weapon
        if (playerInput.mouseLeftClick)
        {
            player.meleeWeapon.Fire(
                player.transform.position,
                playerInput.mousePosition - transform.position);
        }
    }
}
