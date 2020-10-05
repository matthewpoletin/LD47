using System;
using UnityEngine;
using UnityEngine.UI;

public class CreditsDialog : MonoBehaviour
{
    [SerializeField] private Button _closeButton = default;

    private Action _onCloseCallback;

    public void Connect(Action onCloseCallback)
    {
        _closeButton.onClick.AddListener(OnGameButtonClick);

        _onCloseCallback = onCloseCallback;
    }

    private void OnGameButtonClick()
    {
        _onCloseCallback?.Invoke();
    }

    public void Utilize()
    {
        _closeButton.onClick.RemoveListener(OnGameButtonClick);
    }
}