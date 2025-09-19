using UnityEngine;
using UnityEngine.SceneManagement;

public class Ball : MonoBehaviour
{
    private int _x = 1;
    private int _y = 1;
    public  int X => _x;
    public int Y => _y;

    [Header("Launch Settings")]
    public float initVelocity = 5f;
    public Vector2 direction = Vector2.right;

    [Header("Gravity Control")]
    public float gravityThresholdY = 3f; // height where gravity starts
    public float gravity = -9.81f;       // gravity force

    private bool gravityEnabled = false;
    private Vector2 velocity;
    private Vector2 position;

    [Header("Lifetime")]
    public float lifeTime = 5f;   // seconds before destruction
    private float timer = 0f;

    [Header("Collision")]
    public float collisionRadius = 0.2f; // how big the "hitbox" is
    public LayerMask collisionLayers;    // choose which layers it collides with

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        position = transform.position;
        velocity = direction.normalized * initVelocity;
    }

    // Update is called once per frame
    void Update()
    {
        float dt = Time.deltaTime;

        // Lifetime
        timer += dt;
        if (timer >= lifeTime)
        {
            Destroy(gameObject);
            return; // stop updating after destroy
        }

        // Gravity enable
        if (!gravityEnabled && position.y <= gravityThresholdY)
        {
            gravityEnabled = true;
        }

        if(gravityEnabled)
        {
            velocity.y += gravity * dt;
        }

        // Movement
        position += velocity * dt;
        transform.position = position;

        CheckCollision();

        if (Cannon.Instance.Score == 70)
        {
            SceneManager.LoadScene("GameOverScreen");
        }
    }

    private void CheckCollision()
    {
        // Cast a small circle around the ball
        Collider2D hit = Physics2D.OverlapCircle(transform.position, collisionRadius, collisionLayers);

        if (hit != null)
        {
            Debug.Log("Ball hit: " + hit.gameObject.name);

            if (hit.CompareTag("Floor"))
            {
                Debug.Log("Ball collided with the Floor");
                Destroy(gameObject);
            }
        }
    }
}
