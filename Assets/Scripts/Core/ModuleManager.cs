using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ModuleManager
{
    private const string SCENE_MAIN_MENU_PATH = "Scenes/MainMenu";
    private const string SCENE_GAME_PATH = "Scenes/Game";

    private ApplicationManager _applicationManager;

    private BaseModule _currentModule;
    private Scene _currentModuleScene;

    public void Initialize(ApplicationManager applicationManager)
    {
        _applicationManager = applicationManager;
    }

    private IEnumerator UnloadCurrentModule()
    {
        if (_currentModule == null)
        {
            yield break;
        }

        _currentModule.Utilize();

        if (_currentModuleScene.isLoaded)
        {
            yield return SceneManager.UnloadSceneAsync(_currentModuleScene);
        }

        _currentModule = null;
    }

    public void LoadMainMenu()
    {
        _applicationManager.StartCoroutine(ChangeModule<MainMenuManager>(SCENE_MAIN_MENU_PATH));
    }

    public void LoadGame()
    {
        _applicationManager.StartCoroutine(ChangeModule<GameManager>(SCENE_GAME_PATH));
    }

    private IEnumerator ChangeModule<T>(string sceneName) where T : BaseModule
    {
        yield return UnloadCurrentModule();

        yield return SceneManager.LoadSceneAsync(sceneName);

        _currentModuleScene = SceneManager.GetSceneByName(sceneName);
        // TODO: Активировать сцену при смене уровня
        // SceneManager.SetActiveScene(_currentModuleScene);

        _currentModule = GameObject.FindObjectOfType<T>();
        _currentModule.Connect(_applicationManager.Controller);
    }
}