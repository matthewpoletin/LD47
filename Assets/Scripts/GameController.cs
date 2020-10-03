public class GameController
{
    private readonly ModuleManager _moduleManager;

    public GlobalParams GlobalParams { get; }

    public GameController(ModuleManager moduleManager, GlobalParams globalParams)
    {
        _moduleManager = moduleManager;
        GlobalParams = globalParams;
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