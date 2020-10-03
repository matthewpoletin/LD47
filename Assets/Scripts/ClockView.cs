using UnityEngine;

public class ClockView : MonoBehaviour
{
    [SerializeField] private Transform _hourHand = default;
    [SerializeField] private Transform _minuteHand = default;

    private Timer _timer;

    private const float ROTATION_SPEED = 100;

    public void Connect(Timer cycleManagerTimer)
    {
        _timer = cycleManagerTimer;
    }

    private void Update()
    {
        _hourHand.Rotate(0, 0, -1 * ROTATION_SPEED * Time.deltaTime);
        _minuteHand.Rotate(0, 0, -1 * ROTATION_SPEED * 60 * Time.deltaTime);

        // TODO: Обновлять положение время в соответсвии с _timer.TimePassed/_timer.Duration
    }
}