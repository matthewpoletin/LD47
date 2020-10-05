using System;
using UnityEngine;
using UnityEngine.UI;

public class PauseDialog : MonoBehaviour
{
    [SerializeField] private Button _resumeButton = default;
    [SerializeField] private Button _restartButton = default;
    [SerializeField] private Button _exitButton = default;

    private GameController _controller;
    private Action<bool> _resumeGameCallback;

    public void Connect(GameController controller, Action<bool> resumeGameCallback)
    {
        _controller = controller;
        _resumeGameCallback = resumeGameCallback;

        _resumeButton.onClick.AddListener(OnResumeButtonClick);
        _restartButton.onClick.AddListener(OnRestartButtonClick);
        _exitButton.onClick.AddListener(OnExitButtonClick);
    }

    private void OnExitButtonClick()
    {
        _controller.LoadMainMenu();
    }

    private void OnRestartButtonClick()
    {
        _controller.LoadGame();
    }

    private void OnResumeButtonClick()
    {
        _resumeGameCallback?.Invoke(false);
    }

    public void Utilize()
    {
        _resumeButton.onClick.RemoveListener(OnResumeButtonClick);
        _restartButton.onClick.RemoveListener(OnRestartButtonClick);
        _exitButton.onClick.RemoveListener(OnExitButtonClick);
    }
}
