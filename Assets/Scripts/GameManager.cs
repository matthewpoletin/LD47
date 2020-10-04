using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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

    private CycleManager _cycleManager;

    public override void Connect(GameController controller)
    {
        _playerController.Connect(_camera);

        _guestsManager.Connect(controller.GlobalParams.GuestList);

        _cycleManager = new CycleManager(controller, _guestsManager, _bubbleContainer, controller.GlobalParams.StorylineCsv);

        _pauseDialog.Connect(controller);
        _pauseDialog.gameObject.SetActive(false);

        _report.Connect(controller);
        _report.gameObject.SetActive(false);

        _clockView.Connect(_cycleManager.Timer);

        EventManager.OnGuestEnterClue += AddClue;
        EventManager.OnGuestTalkClue += AddClue;

        _cycleManager.Timer.OnTimerElapsed += ShowReport;
    }

    public override void Tick(float deltaTime)
    {
        _cycleManager.Tick(deltaTime);

        if (Input.GetKey(KeyCode.R))
        {
            _cycleManager.Restart();
        }
        else if (Input.GetKey(KeyCode.Escape))
        {
            _pauseDialog.gameObject.SetActive(true);
        }
    }

    private void AddClue(string clueToAdd)
    {
        _report.AddClue(clueToAdd);
        StartCoroutine(ClueNotificationCoroutine());
    }

    IEnumerator ClueNotificationCoroutine()
    {
        _clueDialog.gameObject.SetActive(true);
        yield return new WaitForSeconds(3);
        _clueDialog.gameObject.SetActive(false);
    }

    private void ShowReport()
    {
        StartCoroutine(ShowReportCoroutine());
    }

    IEnumerator ShowReportCoroutine()
    {
        _report.gameObject.SetActive(true);
        yield return new WaitForSeconds(3);
        _report.gameObject.SetActive(false);
    }

    public override void Utilize()
    {
        _pauseDialog.Utilize();
        _guestsManager.Utilize();
        _cycleManager.Utilize();
        EventManager.OnGuestEnterClue -= AddClue;
        EventManager.OnGuestTalkClue -= AddClue;
    }
}