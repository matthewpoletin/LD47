using UnityEngine;
using UnityEngine.SceneManagement;

public class ApplicationManager : MonoBehaviour
{
    [SerializeField] private GlobalParams _globalParams = default;
    [SerializeField] private GameObjectPool _gameObjectPool = default;

    private ModuleManager _moduleManager;

    private static ApplicationManager _instance = null;
    public GameController Controller { get; private set; }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        InitializeManager();
    }

    private void InitializeManager()
    {
        _gameObjectPool.Init();

        _moduleManager = new ModuleManager();
        Controller = new GameController(_moduleManager, _globalParams, _gameObjectPool);
        _moduleManager.Initialize(this);

        if (SceneManager.GetActiveScene().name == "ApplicationManager")
        {
            _moduleManager.LoadMainMenu();
        }
    }

    public void Update()
    {
        var deltaTime = Time.deltaTime;

        _moduleManager.Tick(deltaTime);
    }
}