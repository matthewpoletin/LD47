using UnityEngine;

public class ClockView : MonoBehaviour
{
    [SerializeField] private Transform _hourHand = default;
    [SerializeField] private Transform _minuteHand = default;

    private const float ROTATION_SPEED = 100;

    private void Update()
    {
        _hourHand.Rotate(0, 0, -1 * ROTATION_SPEED * Time.deltaTime);
        _minuteHand.Rotate(0, 0, -1 * ROTATION_SPEED * 60 * Time.deltaTime);
    }
}