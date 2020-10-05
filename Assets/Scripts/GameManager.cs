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
    [SerializeField] private ClueManager _clueManager = default;
    [SerializeField] private Tutorial _tutorial = default;
    [SerializeField] private CollectedMoneyWidget _collectedMoneyWidget = default;

    private GameModel _gameModel;
    private CycleManager _cycleManager;

    public override void Connect(GameController controller)
    {
        _gameModel = new GameModel();

        _playerController.Connect(_camera, controller.GlobalParams);

        _guestsManager.Connect(_gameModel, controller.GlobalParams.GuestList, controller.Pool, controller.GlobalParams,
            controller.GlobalParams.CommonAssets,
            _camera, _bubbleContainer, _playerController);

        _cycleManager = new CycleManager(controller, controller.Pool, _guestsManager, _bubbleContainer, _minigameContainer,
            controller.GlobalParams.StorylineCsv, _playerController, _camera);

        _pauseDialog.Connect(controller);
        _pauseDialog.gameObject.SetActive(false);

        _report.Connect(controller);
        _report.gameObject.SetActive(false);

        _tutorial.Connect(_cycleManager);
        OpenTutorialScreen();

        _clockView.Connect(_cycleManager.Timer);
        _collectedMoneyWidget.Connect(_gameModel);

        //EventManager.OnGuestEnterClue += AddClue;
        //EventManager.OnGuestTalkClue += AddClue;

        _cycleManager.Timer.OnTimerElapsed += ShowReport;
    }

    public override void Tick(float deltaTime)
    {
        _cycleManager.Tick(deltaTime);
        _guestsManager.Tick(deltaTime);

        if (Input.GetKey(KeyCode.R))
        {
            _cycleManager.Restart();
        }
        else if (Input.GetKey(KeyCode.Escape))
        {
            _pauseDialog.gameObject.SetActive(true);
        }
    }

    //private void AddClue(string clueToAdd)
    //{
    //    if (CompareStrings(clueToAdd, _report._currentClues)) { return; }

    //    _report._currentClues.Add(clueToAdd);
    //    StartCoroutine(ClueNotificationCoroutine());
    //}

    //IEnumerator ClueNotificationCoroutine()
    //{
    //    _clueDialog.gameObject.SetActive(true);
    //    yield return new WaitForSeconds(3);
    //    _clueDialog.gameObject.SetActive(false);
    //}

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
        _report.gameObject.SetActive(false);

        // Снимаем с паузы, так как после того, как туториал закончится таймер должен начинаться после репорта
        _cycleManager.Timer.Unpause();
        OpenTutorialScreen();
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
        _clueManager.Utilize();
        //EventManager.OnGuestEnterClue -= AddClue;
        //EventManager.OnGuestTalkClue -= AddClue;
    }
}