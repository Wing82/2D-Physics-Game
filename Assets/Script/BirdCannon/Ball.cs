using UnityEngine;

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

        timer += dt;
        if (timer >= lifeTime)
        {
            Destroy(gameObject);
            return; // stop updating after destroy
        }

        if (!gravityEnabled && position.y <= gravityThresholdY)
        {
            gravityEnabled = true;
        }

        if(gravityEnabled)
        {
            velocity.y += gravity * dt;
        }

        position += velocity * dt;

        transform.position = position;

        if (transform.position.y <= -3.5f)
        {
            //transform.position = new Vector2(transform.position.x, -3.5f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            Destroy(gameObject);
            Debug.Log("Ball hit the floor and will be destroyed.");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            Destroy(gameObject);
            Debug.Log("Ball hit the floor and will be destroyed.");
        }
    }
}
