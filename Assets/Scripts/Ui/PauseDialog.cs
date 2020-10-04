using UnityEngine;
using UnityEngine.UI;

public class PauseDialog : MonoBehaviour
{
    [SerializeField] private Button _resumeButton = default;
    [SerializeField] private Button _restartButton = default;
    [SerializeField] private Button _exitButton = default;

    private GameController _controller;

    public void Connect(GameController controller)
    {
        _controller = controller;
        
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
        // TODO:
    }

    public void Utilize()
    {
        _resumeButton.onClick.RemoveListener(OnResumeButtonClick);
        _restartButton.onClick.RemoveListener(OnRestartButtonClick);
        _exitButton.onClick.RemoveListener(OnExitButtonClick);
    }
}
