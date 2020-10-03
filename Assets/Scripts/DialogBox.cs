using UnityEngine;
using UnityEngine.UI;

public class DialogBox : MonoBehaviour
{
    [SerializeField] private Text _text = default;

    private Transform _followPivot;
    private Camera _camera;
    private readonly Vector3 _offset = new Vector3(0.8f, 1.8f, 0f);

    private void Start()
    {
        _camera = Camera.main;
    }

    public void Connect(string value, Transform followPivot, float duration)
    {
        _text.text = value;
        _followPivot = followPivot;

        GameObject.Destroy(gameObject, duration);
    }

    private void Update()
    {
        transform.position = _camera.WorldToScreenPoint(_followPivot.transform.position) + _offset;
    }
}