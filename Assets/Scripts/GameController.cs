using System;
using Object = UnityEngine.Object;

public class GameController
{
    private readonly ModuleManager _moduleManager;

    private CreditsDialog _creditsDialog;

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

    public void OpenCreditsDialog(Action onCloseCallback = null)
    {
        var creditsDialogGo = Object.Instantiate(GlobalParams.CommonAssets.CreditsDialog, _moduleManager.CurrentModule.DialogsContainer);
        _creditsDialog = creditsDialogGo.GetComponent<CreditsDialog>();
        _creditsDialog.Connect(OnCreditsDialogCloseClick, onCloseCallback);
    }

    private void OnCreditsDialogCloseClick()
    {
        _creditsDialog.Utilize();
        Object.Destroy(_creditsDialog.gameObject);
    }

    public void OpenReportMenu()
    {

    }
}