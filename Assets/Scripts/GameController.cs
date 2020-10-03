public class GameController
{
    private readonly ModuleManager _moduleManager;

    public GameController(ModuleManager moduleManager)
    {
        _moduleManager = moduleManager;
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