using UnityEngine;

public class Ball : MonoBehaviour
{
    public Rigidbody2D rb;

    private int _x = 1;
    private int _y = 1;
    public  int X => _x;
    public int Y => _y;

    public float speed = 5f;
    public Vector2 direction = Vector2.right;
    public float lifeTime = 5f;

    public float gravity = 9.81f;
    public float velocity = 0f;
    public float initVelocity = 5f;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb.linearVelocity = Vector2.zero;
    }

    // Update is called once per frame
    void Update()
    {
        direction = direction.normalized;
        rb.linearVelocity = direction * speed * gravity;
        rb.linearVelocity = rb.linearVelocity + Vector2.right;
        rb.AddForce(rb.linearVelocity, ForceMode2D.Impulse);

        Debug.Log("Ball Direction: " + direction);
    }
}
