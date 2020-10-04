using UnityEngine;
using UnityEngine.UI;

public class DialogBox : MonoBehaviour
{
    [SerializeField] private Text _text = default;
    [SerializeField] private CanvasGroup _canvasGroup = default;

    private Transform _followPivot;
    private Camera _camera;
    // private readonly Vector3 _offset = new Vector3(0.8f, 1.8f, 0f);
    private readonly Vector3 _offset = Vector3.zero;

    private void Start()
    {
        _camera = Camera.main;
    }

    public void Connect(string value, Camera camera, Transform followPivot, float duration)
    {
        _text.text = value;
        _followPivot = followPivot;

        GameObject.Destroy(gameObject, duration);
    }

    private void Tick()
    {
        transform.position = _camera.WorldToScreenPoint(_followPivot.transform.position) + _offset;
    }

    public void SetOpacity(float alpha)
    {
        
    }

    public void Utilize()
    {
    }
}