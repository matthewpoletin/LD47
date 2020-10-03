using UnityEngine;

public class GameManager : BaseModule
{
    [SerializeField] private GlobalParams _globalParams = default;
    [SerializeField] private Camera _camera = default;
    [SerializeField] private GuestsManager _guestsManager = default;
    [SerializeField] private PlayerController _playerController = default;
    [SerializeField] private PauseDialog _pauseDialog = default;
    // TODO: Установить ссылку на объект часов
    // [SerializeField] private ClockView _clockView = default;

    private Timer _timer;
    private CycleManager _cycleManager;

    public override void Connect(GameController controller)
    {
        _playerController.Connect(_camera);
        _guestsManager.AddGuest(_globalParams.GuestList[0], _guestsManager.transform);

        _timer = new Timer(_globalParams.CycleDuration);
        _cycleManager = new CycleManager(_timer, _globalParams.CycleDuration);
    
        _pauseDialog.Connect(controller);
        _pauseDialog.gameObject.SetActive(false);
    }

    private void Update()
    {
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