using UnityEngine;
using UnityEngine.SceneManagement;

public class Exit : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Wheel"))
        {
            // Load the next scene or perform any action when the player collides with the exit
            Debug.Log("Player has exited the level!");
            // Example: Load next scene
            SceneManager.LoadScene("New");
        }
    }
}
