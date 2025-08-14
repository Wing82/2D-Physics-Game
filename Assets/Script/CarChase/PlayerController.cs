using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour, PlayerInput.ICarChaseActions
{
    PlayerInput playerInput; // Reference to the PlayerInput Action component
    Rigidbody2D rb;
    Animator anim; 
    private GroundCheck gndChk;

    [Header("Movement Variables")]
    [SerializeField] private float initSpeed = 5.0f; // Initial speed of the player
    [SerializeField] private float maxSpeed = 10.0f; // Maximum speed the player can reach

    [SerializeField] private float acceleration = 0.1f; // Acceleration rate of the player

    private float curSpeed; // Current speed of the player
    Vector2 direction; // The direction of the player's movement, initialized to zero

    [Header("Tilting Variables")]
    [SerializeField] private Transform head; // Reference to the head transform for tilting
    [SerializeField] private Transform tail;
    [SerializeField] private float bikeTiltSpeed = 100f; // Speed for main bike tilt
    [SerializeField] private float maxBikeTiltAngle = 15f; // Max rotation for the bike body
    [SerializeField] private float partTiltAmount = 5f; // Extra tilt for head/tail

    private float bikeTilt;       // Input tilt
    private float currentBikeTilt; // Current rotation of bike

    [Header("Ground Check")]
    public bool isGrounded = false;

    private bool canMove = false;

    private void Awake()
    {
        playerInput = new PlayerInput(); // Create a new instance of PlayerInput
    }

    private void OnEnable()
    {
        playerInput.CarChase.Enable(); // Enable the CarChase actions
        playerInput.CarChase.SetCallbacks(this); // Set this script as the callback for CarChase actions

        if (GameManager.Instance != null)
            GameManager.Instance.OnPlayerMovementToggle += ToggleMovement;
    }

    private void OnDisable()
    {
        playerInput.CarChase.Disable(); // Disable the CarChase actions when this script is disabled
        playerInput.CarChase.RemoveCallbacks(this); // Remove this script as the callback for CarChase actions

        if (GameManager.Instance != null)
            GameManager.Instance.OnPlayerMovementToggle -= ToggleMovement;
    }

    private void ToggleMovement(bool allow)
    {
        canMove = allow;
        if (allow) rb.linearVelocity = Vector2.zero; // stop immediately
    }


    #region Input Functions
    // Implement the interface methods for PlayerInput.ICarChaseActions
    public void OnBreak(InputAction.CallbackContext context)
    {
        if (context.performed)
            PressedBreak();
        if (context.canceled)
            curSpeed = initSpeed; // Reset the current speed to the initial speed when the break is released
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            direction = context.ReadValue<Vector2>(); // Read the movement direction from the input action
        }
        if (context.canceled)
        {
            direction = new Vector2(acceleration * Time.deltaTime, 0); 
        }
    }

    public void OnTiled(InputAction.CallbackContext context)
    {
        bikeTilt = context.ReadValue<float>(); // Read the tilt input from the input action
    }
    #endregion

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
        gndChk = GetComponent<GroundCheck>();
        curSpeed = initSpeed; // Set the current speed to the initial speed
    }

    void Update()
    {
        if (!canMove || Time.timeScale <= 0) // Check if the player can move and if the game is not paused
            return;

        AnimatorClipInfo[] curPlayingClips = anim.GetCurrentAnimatorClipInfo(0);

        CheckIsGround();

        // Calculate the new velocity based on the direction and current speed
        Vector2 velocity = direction * curSpeed;

        // Clamp the speed to the maximum speed
        if (velocity.magnitude > maxSpeed)
            velocity = velocity.normalized * maxSpeed;

        rb.linearVelocity = velocity;

        HandleBikeTilt();

        if (anim != null)
        {
            anim.SetFloat("Move", Mathf.Abs(rb.linearVelocity.x));

            if (velocity.magnitude > 0.1f)
                anim.Play("BikeRun"); // Play the running animation if the player is moving
            else 
                anim.Play("BikeIdle"); // Play the idle animation if the player is not moving
        }
    }

    private void HandleBikeTilt()
    {
        // Main bike tilt
        if (Mathf.Abs(bikeTilt) > 0.01f)
        {
            currentBikeTilt += bikeTilt * bikeTiltSpeed * Time.deltaTime;
        }
        else
        {
            currentBikeTilt = Mathf.MoveTowards(currentBikeTilt, 0, bikeTiltSpeed * Time.deltaTime);
        }

        currentBikeTilt = Mathf.Clamp(currentBikeTilt, -maxBikeTiltAngle, maxBikeTiltAngle);

        // Apply rotation to whole bike
        //transform.rotation = Quaternion.Euler(0, 0, currentBikeTilt);
        rb.MoveRotation(Mathf.LerpAngle(rb.rotation, currentBikeTilt, Time.deltaTime * bikeTiltSpeed));

        // Apply rotation to head and tail
        if (head != null) head.localRotation = Quaternion.Euler(0, 0, -currentBikeTilt * partTiltAmount / maxBikeTiltAngle);
        if (tail != null) tail.localRotation = Quaternion.Euler(0, 0, currentBikeTilt * partTiltAmount / maxBikeTiltAngle);
    }

    void PressedBreak()
    {
        curSpeed = 0; // Set the current speed to zero when the break is pressed
        Vector2 velocity = direction * curSpeed;
        rb.linearVelocity = velocity;

        if (anim != null)
            anim.Play("BikeIdle"); // Play the stop animation if the Animator component is available
    }

    void CheckIsGround()
    {
        isGrounded = gndChk.isGrounded();
        Debug.Log($"Grounded: {isGrounded}");
    }
}
