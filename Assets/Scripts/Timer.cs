using System;

public class Timer
{
    private float _duration;
    private float _timeLeft;

    public event Action<float> OnTimerTick;
    public event Action OnTimerElapsed;

    private bool _elapsed;

    private bool _paused;

    public float Duration => _duration; 
    public float TimeLeft => _timeLeft; 
    public float TimePassed => _duration - _timeLeft;

    public Timer(float duration)
    {
        Reset(duration);
    }

    public void Reset(float duration)
    {
        _duration = duration;
        _timeLeft = duration;
        _elapsed = false;
    }

    public void Tick(float deltaTime)
    {
        if (_paused || _elapsed)
        {
            return;
        }

        _timeLeft -= deltaTime;
        OnTimerTick?.Invoke(_timeLeft);

        if (!_elapsed && _timeLeft < 0)
        {
            _elapsed = true;
            OnTimerElapsed?.Invoke();
        }
    }

    public void Pause()
    {
        _paused = true;
    }

    public void Unpause()
    {
        _paused = false;
    }
}