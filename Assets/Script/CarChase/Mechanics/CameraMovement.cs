using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private float minXPos;
    [SerializeField] private float maxXPos;

    PlayerController playerController; // Reference to the PlayerController

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnEnable()
    {
        if(!GameManager.Instance) return; // Ensure GameManager is initialized
        GameManager.Instance.OnPlayerSpawned += OnPlayerSpawnedCallback;
    }

    private void OnDisable()
    {
        GameManager.Instance.OnPlayerSpawned -= OnPlayerSpawnedCallback;
    }

    void OnPlayerSpawnedCallback(PlayerController playerInstance) => playerController = playerInstance; // Assign the player instance when the event is triggered

    // Update is called once per frame
    void Update()
    {
        if (!playerController) return;

        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(playerController.transform.position.x, minXPos, maxXPos);
        transform.position = pos;
    }
}
