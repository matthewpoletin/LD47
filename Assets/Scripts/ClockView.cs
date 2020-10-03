using UnityEngine;

public class ClockView : MonoBehaviour
{
    [SerializeField] private Transform _hourHand = default;
    [SerializeField] private Transform _minuteHand = default;

    private Timer _timer;

    private const float ROTATION_SPEED = 25;

    public void Connect(Timer cycleManagerTimer)
    {
        _timer = cycleManagerTimer;
    }

    private void Update()
    {
        _hourHand.localEulerAngles = new Vector3(-90f, 0f, -360f * _timer.TimePassed / _timer.Duration);
        _minuteHand.localEulerAngles = new Vector3(-90f, 0f, 60 * -360f * _timer.TimePassed / _timer.Duration);
    }
}