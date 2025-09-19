using UnityEngine;

public class Box : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ball"))
        {
           Cannon.Instance.Score += 10;
            Destroy(gameObject);
            Destroy(collision.gameObject);
        }
    }
}
