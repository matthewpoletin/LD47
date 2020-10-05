using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour, ICharacter
{
    [SerializeField] private CharacterController controller = default;
    [SerializeField] private Transform _topPlaceholder = default;
    [SerializeField] private GameObject leftPivo = default;
    [SerializeField] private GameObject rightPivo = default;
    [SerializeField] private Animator _animator = default;

    private Camera _camera;
    private GlobalParams _globalParams;

    public bool MovementEnabled { get; set; } = true;

    public Transform TopPlaceholder => _topPlaceholder;
    public Vector3 Position => transform.position;

    private Coroutine _moving = null;

    private float lastX = 0.0f;

    public void Connect(Camera camera1, GlobalParams globalParams)
    {
        _camera = camera1;
        _globalParams = globalParams;
    }

    public void Tick(float deltaTime)
    {
        if (!MovementEnabled)
        {
            return;
        }

        if (_globalParams == null)
        {
            return;
        }

        float x = Input.GetAxis("Horizontal");

        var xAbs = Mathf.Abs(x);

        if (xAbs > 0.001 && lastX > 0.01)
        {
            _animator.SetFloat("Movement", xAbs);

            if (_moving == null)
            {
                if (x > 0)
                {
                    _moving = StartCoroutine(Rotate(-90));
                }
                else
                {
                    _moving = StartCoroutine(Rotate(90));
                }
            }
        }

        var movement = Vector3.right * x;

        transform.Translate(movement * (_globalParams.PlayerMovementSpeed * Time.deltaTime), Space.World);

        //controller.Move(movement * (_globalParams.PlayerMovementSpeed * Time.deltaTime));

        if (transform.position.x > rightPivo.transform.position.x)
        {
            StartCoroutine(Rotate(90));

            transform.position = rightPivo.transform.position;
            x = 0;
        }
        else if (transform.position.x < leftPivo.transform.position.x)
        {
            StartCoroutine(Rotate(-90));

            transform.position = leftPivo.transform.position;
            x = 0;
        }

        _camera.transform.Rotate(0, x * Time.deltaTime * 6, 0);

        lastX = x;
    }

    private IEnumerator Rotate(float angle)
    {
        float duration = 0.2f;
        float startRotation = transform.eulerAngles.y;
        float endRotation = startRotation + angle;
        float t = 0.0f;
        while (t < duration)
        {
            t += Time.deltaTime;
            float yRotation = Mathf.Lerp(startRotation, endRotation, t / duration) % 360.0f;
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, yRotation,
            transform.eulerAngles.z);
            yield return null;
        }

        _moving = null;
    }
}
