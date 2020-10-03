public class CycleManager
{
    private readonly GuestsManager _guestsManager;
    private readonly float _cycleDuration;
    private readonly GuestTimelineParams _guestTimeline;

    private readonly CommandsFactory _commandsFactory;

    private TimeCommandManager _timeline;

    private readonly Timer _timer;

    public Timer Timer => _timer;

    public CycleManager(GameController controller, GuestsManager guestsManager)
    {
        _guestsManager = guestsManager;
        _cycleDuration = controller.GlobalParams.CycleDuration;
        _guestTimeline = controller.GlobalParams.GuestTimelineParams;

        _timer = new Timer(_cycleDuration);
        _timer.OnTimerElapsed += OnTimerElapsed;

        _timeline = new TimeCommandManager(_timer);
        _commandsFactory = new CommandsFactory(_guestsManager, controller.GlobalParams.CommonAssets.DialogPrefab);

        Restart();
    }

    public void Tick(float deltaTime)
    {
        _timer.Tick(deltaTime);
        _timeline.Tick(deltaTime);
    }

    private void OnTimerElapsed()
    {
        // TODO: Конец цикла
        Restart();
    }

    public void Restart()
    {
        _timer.Reset(_cycleDuration);
        _timer.Unpause();

        // TODO: Initialize
        foreach (var command in _guestTimeline.GuestAppear)
        {
            var timeCommand = _commandsFactory.CreateTimeCommand(command);
            _timeline.AddCommand(timeCommand);
        }

        foreach (var command in _guestTimeline.GuestLeave)
        {
            var timeCommand = _commandsFactory.CreateTimeCommand(command);
            _timeline.AddCommand(timeCommand);
        }
    }

    public void Utilize()
    {
    }
}