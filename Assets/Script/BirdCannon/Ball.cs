using UnityEngine;

public class Ball : MonoBehaviour
{
    private int _x = 1;
    private int _y = 1;
    public  int X => _x;
    public int Y => _y;

    public float speed = 10f;
    public Vector3 direction = Vector3.forward;
    public float lifeTime = 5f;

    public float gravity = -9.81f;
    public float velocity = 0f;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
