using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Cannon : MonoBehaviour, PlayerInput.IBirdCannonActions
{
    PlayerInput playerInput; // Reference to the PlayerInput Action component
    Rigidbody2D rb;

    private static Cannon _instance;
    public static Cannon Instance => _instance;

    [Header("Cannon Settings")]
    public float rotationSpeed = 90f;
    public float angle = 45f;
    public Transform rotatePivot;

    public event Action<float> OnAngleChanged;

    public float Angle
    {
        get => angle;
        set
        {
            angle = value;
            OnAngleChanged?.Invoke(angle);
        }
    }
    
    

    [Header("Firing Settings")]
    public Ball ballPrefab;
    public Transform firePoint;
    public float minPower = 10f;
    public float maxPower = 50f;
    public float powerRate = 20f;
    public float currentPower = 0f;

    public event Action<float> OnPowerChanged;

    public float CurrentPower
    { 
        get => currentPower;
        set
        {
            currentPower = value;
            OnPowerChanged?.Invoke(currentPower);
        }
    }


    private bool _isFiring = false;

    private Vector2 rotateInput; // store the input vector


    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
        playerInput = new PlayerInput();
        currentPower = minPower;
    }

    private void OnEnable()
    {
        playerInput.BirdCannon.Enable();
        playerInput.BirdCannon.SetCallbacks(this);
    }

    private void OnDisable()
    {
        playerInput.BirdCannon.Disable();
        playerInput.BirdCannon.RemoveCallbacks(this);
    }

    public void OnRotate(InputAction.CallbackContext context)
    {
        rotateInput = context.ReadValue<Vector2>();
    }

    public void OnFire(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _isFiring = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        CannonRotate();
        AdjustPower();

        if (_isFiring)
        {
            Fire();
            _isFiring = false;
        }
    }

    void CannonRotate()
    {
        float rotateAmount = rotateInput.y; // Get the vertical input for rotation

        if (!_isFiring)
        {
            angle += rotateAmount * rotationSpeed * Time.deltaTime;
            angle = Mathf.Clamp(angle, 20f, 60f); // Clamp the angle

            rotatePivot.rotation = Quaternion.Euler(0, 0, angle);

            Debug.Log("Cannon Angle: " + angle);
        }
    }

    void AdjustPower()
    {
        if (!_isFiring)
        {
            float powerAdjust = rotateInput.x; // Get the horizontal input for power adjustment

            currentPower += powerAdjust * powerRate * Time.deltaTime;
            currentPower = Mathf.Clamp(currentPower, minPower, maxPower);

            Debug.Log("Current Power: " + currentPower);
        }
    }

    void Fire()
    {
        if (ballPrefab && firePoint)
        {
            Ball ball = Instantiate(ballPrefab, firePoint.position, firePoint.rotation);  
            ball.direction = firePoint.right; // Set the ball's direction to the fire point's right vector
            ball.initVelocity = currentPower; // Set the ball's speed to the current power
            Debug.Log("Fired ball with power: " + currentPower);
        }
    } 
}
