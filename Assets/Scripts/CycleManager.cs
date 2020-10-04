using UnityEngine;

public class CycleManager
{
    private readonly GuestsManager _guestsManager;
    private readonly float _cycleDuration;
    private readonly GuestTimelineParams _guestTimeline;

    private readonly CommandsFactory _commandsFactory;

    private readonly ClueManager _clueManager;

    private TimeCommandManager _timeline;

    private readonly Timer _timer;

    public Timer Timer => _timer;

    public CycleManager(GameController controller, GuestsManager guestsManager, Transform bubbleContainer)
    {
        _guestsManager = guestsManager;
        _cycleDuration = controller.GlobalParams.CycleDuration;
        _guestTimeline = controller.GlobalParams.GuestTimelineParams;

        _timer = new Timer(_cycleDuration);
        _timer.OnTimerElapsed += OnTimerElapsed;

        _timeline = new TimeCommandManager(_timer);
        _commandsFactory = new CommandsFactory(_guestsManager, controller.GlobalParams.CommonAssets.DialogPrefab, bubbleContainer);

        _clueManager = new ClueManager(controller.GlobalParams.CommonAssets.DialogPrefab, bubbleContainer);

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

        foreach (var command in _guestTimeline.GuestAppear)
        {
            _timeline.AddCommand(_commandsFactory.CreateTimeCommand(command));
        }

        foreach (var command in _guestTimeline.GuestLeave)
        {
            _timeline.AddCommand(_commandsFactory.CreateTimeCommand(command));
        }

        foreach (var command in _guestTimeline.GuestDialog)
        {
            _timeline.AddCommand(_commandsFactory.CreateTimeCommand(command));
        }
    }

    public void Utilize()
    {
        _timeline.Utilize();
    }
}