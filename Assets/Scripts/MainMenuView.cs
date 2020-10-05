using UnityEngine;
using UnityEngine.UI;

public class MainMenuView : MonoBehaviour
{
    [SerializeField] private Button _playButton = default;
    [SerializeField] private Button _creditsButton = default;
    [SerializeField] private Button _exitButton = default;

    private GameController _controller;

    public void Connect(GameController controller)
    {
        _controller = controller;

        _playButton.onClick.AddListener(OnPlayButtonClick);
        _creditsButton.onClick.AddListener(OnCreditsButtonClick);
        _exitButton.onClick.AddListener(OnExitButtonClick);
    }

    private void OnPlayButtonClick()
    {
        _controller.LoadGame();
    }

    private void OnCreditsButtonClick()
    {
        _controller.OpenCreditsDialog();
    }

    private void OnExitButtonClick()
    {
        Application.Quit();
    }

    public void Utilize()
    {
        _playButton.onClick.RemoveListener(OnPlayButtonClick);
        _creditsButton.onClick.RemoveListener(OnCreditsButtonClick);
        _exitButton.onClick.RemoveListener(OnExitButtonClick);
    }
}