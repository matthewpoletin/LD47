using UnityEngine;

public class GameManager : BaseModule
{
    [SerializeField] private Camera _camera = default;
    [SerializeField] private GuestsManager _guestsManager = default;
    [SerializeField] private PlayerController _playerController = default;
    [SerializeField] private PauseDialog _pauseDialog = default;
    [SerializeField] private ClockView _clockView = default;
    [SerializeField] private Transform _bubbleContainer = default;
    [SerializeField] private Transform _minigameContainer = default;
    [SerializeField] private GameObject _clueDialog = default;
    [SerializeField] private Report _report = default;
    [SerializeField] private Tutorial _tutorial = default;
    [SerializeField] private CollectedMoneyWidget _collectedMoneyWidget = default;

    private GameModel _gameModel;
    private CycleManager _cycleManager;

    private bool _isPaused = false;

    public override void Connect(GameController controller)
    {
        _gameModel = new GameModel();

        _playerController.Connect(_camera, controller.GlobalParams);

        _guestsManager.Connect(_gameModel, controller.GlobalParams.GuestList, controller.Pool, controller.GlobalParams,
            controller.GlobalParams.CommonAssets,
            _camera, _bubbleContainer, _minigameContainer, _playerController);

        _cycleManager = new CycleManager(controller, controller.Pool, _guestsManager, _bubbleContainer,
            _minigameContainer,
            controller.GlobalParams.StorylineCsv, _playerController, _camera, _gameModel);

        _pauseDialog.Connect(controller, TogglePause);
        _pauseDialog.gameObject.SetActive(false);

        _report.Connect(controller);
        _report.gameObject.SetActive(false);

        _tutorial.Connect(_cycleManager);
        OpenTutorialScreen();

        _clockView.Connect(_cycleManager.Timer);
        _collectedMoneyWidget.Connect(_gameModel);

        _cycleManager.Timer.OnTimerElapsed += ShowReport;

        EventManager.OnCallPolice += ContinueGame;
    }

    public override void Tick(float deltaTime)
    {
        if (!_isPaused)
        {
            _playerController.Tick(deltaTime);
            _cycleManager.Tick(deltaTime);
            _guestsManager.Tick(deltaTime);
        }

        if (Input.GetKey(KeyCode.R))
        {
            _cycleManager.Restart();
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause(!_isPaused);
        }
    }

    private void TogglePause(bool isPaused)
    {
        _pauseDialog.gameObject.SetActive(isPaused);

        if (isPaused)
        {
            _cycleManager.Timer.Pause();
        }
        else
        {
            _cycleManager.Timer.Unpause();
        }

        _isPaused = isPaused;
    }

    /// <summary>
    /// Вызывается в момент рестарта
    /// </summary>
    public void ShowReport()
    {
        _cycleManager.Timer.Pause();
        _report.StartAddingClues(_guestsManager.ClueList, _guestsManager.GuestParamsList);
        _report.gameObject.SetActive(true);
    }

    public void CloseReport()
    {
        ContinueGame();
        OpenTutorialScreen();
    }

    private void ContinueGame()
    {
        _report.gameObject.SetActive(false);
        _cycleManager.Timer.Unpause();
    }

    private void OpenTutorialScreen()
    {
        _tutorial.OpenTutorialScreen();
    }

    public override void Utilize()
    {
        _pauseDialog.Utilize();
        _guestsManager.Utilize();
        _cycleManager.Utilize();
        EventManager.OnCallPolice += ContinueGame;
    }
}