using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour, PlayerInput.ICarChaseActions
{
    PlayerInput playerInput; // Reference to the PlayerInput component
    Rigidbody2D rb;
    Animator anim; // Reference to the Animation component for player animations
    private GroundCheck gndChk;

    [Header("Movement Variables")]
    [SerializeField] private float initSpeed = 5.0f; // Initial speed of the player
    [SerializeField] private float maxSpeed = 10.0f; // Maximum speed the player can reach

    private float curSpeed; // Current speed of the player
    Vector2 direction; // The direction of the player's movement, initialized to zero

    private void Awake()
    {
        playerInput = new PlayerInput(); // Create a new instance of PlayerInput
    }

    private void OnEnable()
    {
        playerInput.CarChase.Enable(); // Enable the CarChase actions
        playerInput.CarChase.SetCallbacks(this); // Set this script as the callback for CarChase actions
    }

    private void OnDisable()
    {
        playerInput.CarChase.Disable(); // Disable the CarChase actions when this script is disabled
        playerInput.CarChase.RemoveCallbacks(this); // Remove this script as the callback for CarChase actions
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Get the Rigidbody2D component attached to this GameObject
        anim = GetComponent<Animator>(); // Get the Animator component attached to this GameObject
        curSpeed = initSpeed; // Set the current speed to the initial speed
    }

    #region Input Functions
    // Implement the interface methods for PlayerInput.ICarChaseActions
    public void OnBreak(InputAction.CallbackContext context)
    {
        if (context.performed)
            PressedBreak();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        direction = context.ReadValue<Vector2>(); // Read the movement direction from the input action
    }

    public void OnTiled(InputAction.CallbackContext context)
    {
        direction = context.ReadValue<Vector2>(); // Read the tilting direction from the input action
    }
    #endregion

    void Update()
    {
        if (Time.timeScale <= 0) return;

        AnimatorClipInfo[] curPlayingClips = anim.GetCurrentAnimatorClipInfo(0);

        // Calculate the new velocity based on the direction and current speed
        Vector2 velocity = direction * curSpeed;

        // Clamp the speed to the maximum speed
        if (velocity.magnitude > maxSpeed)
            velocity = velocity.normalized * maxSpeed;

        rb.linearVelocity = velocity;

        if (anim != null)
        {
            anim.SetFloat("Move", Mathf.Abs(rb.linearVelocity.x));

            if (velocity.magnitude > 0.1f)
                anim.Play("BikeRun"); // Play the running animation if the player is moving
            else 
                anim.Play("BikeIdle"); // Play the idle animation if the player is not moving
        }
    }

    void PressedBreak()
    {
        curSpeed = 0; // Set the current speed to zero when the break is pressed
        rb.linearVelocity = Vector2.zero; // Stop the Rigidbody2D by setting its velocity to zero

        if (anim != null)
            anim.Play("BikeIdle"); // Play the stop animation if the Animator component is available
    }

}
