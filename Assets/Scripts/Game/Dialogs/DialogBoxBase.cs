using UnityEngine;

public abstract class DialogBoxBase : MonoBehaviour
{
    // private readonly Vector3 _offset = new Vector3(0.8f, 1.8f, 0f);
    private readonly Vector3 _offset = Vector3.zero;

    [SerializeField] private CanvasGroup _canvasGroup = default;

    private GuestView _guestView;
    private Camera _camera;

    public GuestView GuestView => _guestView;

    protected void Connect(Camera camera1, GuestView guestView)
    {
        _camera = camera1;
        _guestView = guestView;
    }

    public void Tick(float deltaTime)
    {
        transform.position = _camera.WorldToScreenPoint(_guestView.TopPlaceholder.position) + _offset;
    }

    public void SetOpacity(float alpha)
    {
        _canvasGroup.alpha = alpha;
    }

    public void Utilize()
    {
        _canvasGroup.alpha = 1f;
    }
}