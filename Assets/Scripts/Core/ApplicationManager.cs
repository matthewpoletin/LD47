using UnityEngine;
using UnityEngine.SceneManagement;

public class ApplicationManager : MonoBehaviour
{
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
        _moduleManager = new ModuleManager();
        Controller = new GameController(_moduleManager);
        _moduleManager.Initialize(this);

        if (SceneManager.GetActiveScene().name == "ApplicationManager")
        {
            _moduleManager.LoadMainMenu();
        }
    }
}