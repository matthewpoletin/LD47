public class GameController
{
    private readonly ModuleManager _moduleManager;

    public GlobalParams GlobalParams { get; }
    public GameObjectPool Pool { get; }

    public GameController(ModuleManager moduleManager, GlobalParams globalParams, GameObjectPool pool)
    {
        _moduleManager = moduleManager;
        GlobalParams = globalParams;
        Pool = pool;
    }

    public void LoadMainMenu()
    {
        _moduleManager.LoadMainMenu();
    }

    public void LoadGame()
    {
        _moduleManager.LoadGame();
    }

    public void OpenCreditsDialog()
    {
    }
}