using UnityEngine;

public class Managers : MonoBehaviour
{
    private static Managers _instance;
    public static Managers Instance { get { Init(); return _instance; } }

    private readonly ResourceManager _resourceManager = new();
    private readonly PoolManager _poolManager = new();
    private readonly DataManager _dataManager = new();
    private readonly InputManager _inputManager = new();
    private readonly SceneManagerEx _sceneManager = new();
    private readonly SoundManager _soundManager = new();
    private readonly GameStateManager _gameStateManager = new();
    private readonly SpawnManager _spawnManager = new();

    public static ResourceManager Resource => Instance._resourceManager;
    public static PoolManager Pool => Instance._poolManager;
    public static DataManager Data => Instance._dataManager;
    public static InputManager Input => Instance._inputManager;
    public static SceneManagerEx Scene => Instance._sceneManager;
    public static SoundManager Sound => Instance._soundManager;
    public static GameStateManager GameState => Instance._gameStateManager;
    public static SpawnManager Spawn => Instance._spawnManager;

    private void Start()
    {
        Init();
    }

    private void OnDestroy()
    {
        if (_instance != this)
            return;

        _inputManager.Clear();
        _instance = null;
    }

    public static void Init()
    {
        if (_instance != null)
            return;

        GameObject managerObject = GameObject.Find("@Manager");
        if (managerObject == null)
        {
            managerObject = new GameObject("@Manager");
            managerObject.AddComponent<Managers>();
        }

        DontDestroyOnLoad(managerObject);
        _instance = managerObject.GetComponent<Managers>();
        _instance._poolManager.Init(_instance.transform);
        _instance._inputManager.Init();
        _instance._sceneManager.Init();
        _instance._soundManager.Init();
        _instance._gameStateManager.Init();
        _instance._spawnManager.Init();
    }

    public static void Clear()
    {
        Pool.Clear();
        GameState.Clear();
        Spawn.Clear();
    }
}
