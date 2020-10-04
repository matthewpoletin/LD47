using UnityEngine;
using UnityEngine.UI;

public class DialogBox : MonoBehaviour
{
    private readonly Vector3 _offset = new Vector3(0.8f, 1.8f, 0f);

    [SerializeField] private Text _text = default;
    [SerializeField] private CanvasGroup _canvasGroup = default;

    private GuestView _guestView;
    private Camera _camera;

    public GuestView GuestView => _guestView;

    public void Connect(string value, Camera camera1, GuestView guestView)
    {
        _text.text = value;
        _camera = camera1;
        _guestView = guestView;
    }

    public void Tick(float deltaTime)
    {
        transform.position = _camera.WorldToScreenPoint(_guestView.transform.position) + _offset;
    }

    public void SetOpacity(float alpha)
    {
        _canvasGroup.alpha = alpha;
    }

    public void Utilize()
    {
        _canvasGroup.alpha = 1f;
        _text.text = "";
    }
}