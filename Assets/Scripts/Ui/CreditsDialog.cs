using System;
using UnityEngine;
using UnityEngine.UI;

public class CreditsDialog : MonoBehaviour
{
    [SerializeField] private Button _closeButton = default;

    private Action[] _onCloseCallbacks;

    public void Connect(params Action[] onCloseCallback)
    {
        _closeButton.onClick.AddListener(OnGameButtonClick);

        _onCloseCallbacks = onCloseCallback;
    }

    private void OnGameButtonClick()
    {
        foreach (var callback in _onCloseCallbacks)
        {
            callback?.Invoke();
        }
    }

    public void Utilize()
    {
        _closeButton.onClick.RemoveListener(OnGameButtonClick);
    }
}