public class CycleManager
{
    private float _cycleDuration;

    private readonly Timer _timer;

    public Timer Timer => _timer;

    public CycleManager(float cycleDuration, GuestTimelineParams guestTimelineParams)
    {
        _cycleDuration = cycleDuration;

        _timer = new Timer(_cycleDuration);
        _timer.OnTimerElapsed += OnTimerElapsed;

        Restart();
    }

    public void Tick(float deltaTime)
    {
        _timer.Tick(deltaTime);

        // TODO: 
        // _timer.TimePassed
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
    }

    public void Utilize()
    {
    }
}