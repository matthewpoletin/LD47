using TMPro;
using UnityEngine;

public enum CycleState
{
    Paused,
    Ticking,
    ShowResults,
}

public class CycleManager
{
    private float _cycleDuration;

    private Timer Timer { get; set; }

    public CycleManager(Timer timer, float duration)
    {
        _cycleDuration = duration;
        Timer = timer;
        Timer.OnTimerElapsed += OnTimerElapsed;

        Restart();
    }

    private void OnTimerElapsed()
    {
        Restart();
    }

    public void Restart()
    {
        Timer.Reset(_cycleDuration);
        Timer.Unpause();
    }
}