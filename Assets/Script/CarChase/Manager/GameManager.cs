using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

[DefaultExecutionOrder(-1)]
public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance => _instance;
    public event Action<PlayerController> OnPlayerSpawned;
    public event Action<int> OnLifeValueChanged;
    public event Action<int> OnTimerValueChanged;

    public event Action OnTimerPrepare; // Show "Get Ready!"
    public event Action<bool> OnPlayerMovementToggle; // Enable/Disable player movement

    #region PLAYER CONTROLLER INFO
    [SerializeField] private PlayerController playerPrefab;
    private PlayerController _playerInstance;
    public PlayerController PlayerInstance => _playerInstance;
    #endregion

    #region MENU CONTROLLER INFO
    private MenuController currentMenuController;

    public void SetMenuController(MenuController newMenuController) => currentMenuController = newMenuController;
    #endregion

    #region GAME PROPERTIES
    #region LIVES
    [SerializeField] private int maxLives = 5;
    private int _lives = 3;

    public int lives
    {
        get => _lives;
        set
        {
            if (value <= 0)
            {
                GameOver();
                return;
            }

            //if (_lives > value) Respawn();

            _lives = value;

            if (_lives > maxLives) _lives = maxLives;

            OnLifeValueChanged?.Invoke(_lives);

            Debug.Log($"{gameObject.name} lives has changed to {_lives}");
        }
    }
    #endregion

    #region TIMER
    private float _timer = 35f; // Default timer value: Seconds
    public float timer
    {
        get => _timer;
        set
        {
            _timer = value;
            OnTimerValueChanged?.Invoke((int)_timer);
            if (_timer <= 0)
            {
                GameOver();
                return;
            }
            Debug.Log($"{gameObject.name} timer has changed to {_timer}");
        }
    }
    #endregion
    #endregion

    private bool timeIsRunning = false;

    void Awake()
    {
        if (!_instance)
        {
            _instance = this;
            DontDestroyOnLoad(this);
            return;
        }

        Destroy(gameObject);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _timer = 30f; // Reset to default at start
        timeIsRunning = false; // Timer is not running at the start

        _playerInstance = Instantiate(playerPrefab, new Vector2(-10, -1), Quaternion.identity);
        _playerInstance.gameObject.name = "Player";
        OnPlayerSpawned?.Invoke(_playerInstance); // Notify that the player has been spawned

        OnPlayerMovementToggle?.Invoke(false); // Disable player movement initially

        StartCoroutine(StartTimerAfterDelay()); // Start the timer after a delay
    }

    IEnumerator StartTimerAfterDelay()
    {
        yield return null; // wait a frame so UI can subscribe
        
        OnTimerPrepare?.Invoke(); // Show "Get Ready"
        yield return new WaitForSeconds(1.0f); // Wait before starting

        for(int count = 3; count > 0; count--)
        {
            OnTimerValueChanged?.Invoke(count);
            yield return new WaitForSeconds(1.0f);
        }

        // Start main timer
        timeIsRunning = true;
        OnPlayerMovementToggle?.Invoke(true); // Allow player movement
        OnTimerValueChanged?.Invoke((int)_timer); // Show full timer value
    }

    // Update is called once per frame
    void Update()
    {
        if (timeIsRunning && timer > 0)
        {
            timer -= Time.deltaTime;
            if (timer <= 0) timer = 0;
        }
    }

    void GameOver()
    {
        Debug.Log("Game Over goes here");
        SceneManager.LoadScene("GameOver");
        _lives = maxLives;
        Destroy(this);
    }
}
