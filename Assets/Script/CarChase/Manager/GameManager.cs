using System;
using UnityEngine;

[DefaultExecutionOrder(-1)]
public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance => _instance;
    public event Action<PlayerController> OnPlayerSpawned;
    public event Action<int> OnLifeValueChanged;
    public event Action<int> OnScoreValueChanged;

    #region PLAYER CONTROLLER INFO
    [SerializeField] private PlayerController playerPrefab;
    private PlayerController _playerInstance;
    public PlayerController PlayerInstance => _playerInstance;
    #endregion

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
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
