using UnityEngine;

public class GameManager : BaseModule
{
    [SerializeField] private GlobalParams _globalParams = default;
    [SerializeField] private Camera _camera = default;
    [SerializeField] private GuestsManager _guestsManager = default;
    [SerializeField] private PlayerController _playerController = default;
    [SerializeField] private PauseDialog _pauseDialog = default;
    [SerializeField] private ClockView _clockView = default;

    private CycleManager _cycleManager;

    public override void Connect(GameController controller)
    {
        _playerController.Connect(_camera);
        _guestsManager.AddGuest(_globalParams.GuestList[0], _guestsManager.transform);

        _cycleManager = new CycleManager(_globalParams.CycleDuration, _globalParams.GuestTimelineParams);

        _pauseDialog.Connect(controller);
        _pauseDialog.gameObject.SetActive(false);

        _clockView.Connect(_cycleManager.Timer);
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

    public override void Utilize()
    {
        _pauseDialog.Utilize();
    }
}