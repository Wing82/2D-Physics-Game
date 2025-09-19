using UnityEngine;
using UnityEngine.SceneManagement;

public class Box : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (Cannon.Instance.Score < 70)
        {
            if (collision.CompareTag("Ball"))
            {
                Cannon.Instance.Score += 10;
                Destroy(gameObject);
                Destroy(collision.gameObject);
            }
        }
        else if (Cannon.Instance.Score == 70)
        {
            SceneManager.LoadScene("GameOverScreen");
        }
    }
}
