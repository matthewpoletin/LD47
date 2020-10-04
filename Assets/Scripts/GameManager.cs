using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : BaseModule
{
    [SerializeField] private Camera _camera = default;
    [SerializeField] private GuestsManager _guestsManager = default;
    [SerializeField] private PlayerController _playerController = default;
    [SerializeField] private PauseDialog _pauseDialog = default;
    [SerializeField] private ClockView _clockView = default;
    [SerializeField] private Transform _bubbleContainer = default;
    [SerializeField] private GameObject _clueDialog = default;
    [SerializeField] private Report _report = default;
    [SerializeField] private ClueManager _clueManager = default;
    [SerializeField] private Tutorial _tutorial = default;

    private CycleManager _cycleManager;
    private List<string> _cluesList = new List<string>(); // Надо как-то инициализировать...

    public override void Connect(GameController controller)
    {
        _playerController.Connect(_camera, controller.GlobalParams);

        _guestsManager.Connect(controller.GlobalParams.GuestList, controller.Pool, controller.GlobalParams,
            controller.GlobalParams.CommonAssets,
            _camera, _bubbleContainer, _playerController);

        _cycleManager = new CycleManager(controller, controller.Pool, _guestsManager, _bubbleContainer,
            controller.GlobalParams.StorylineCsv, _playerController, _camera);

        _pauseDialog.Connect(controller);
        _pauseDialog.gameObject.SetActive(false);

        // Тестовые значения для подсказок
        _cluesList.Add("Something");
        _cluesList.Add("More");

        _report.Connect(controller, _cluesList);
        _report.gameObject.SetActive(false);

        _tutorial.Connect(_cycleManager);
        _tutorial.gameObject.SetActive(false);

        _clockView.Connect(_cycleManager.Timer);

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
    //    _report.AddClue(clueToAdd);
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
    private void ShowReport()
    {
        _cycleManager.Timer.Pause();
        StartCoroutine(ShowReportCoroutine());
    }

    IEnumerator ShowReportCoroutine()
    {
        _report.gameObject.SetActive(true);
        yield return new WaitForSeconds(3);
        _report.gameObject.SetActive(false);
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
        _report.Utilize();
        //_tutorial.Utilize();
        //EventManager.OnGuestEnterClue -= AddClue;
        //EventManager.OnGuestTalkClue -= AddClue;
    }
}