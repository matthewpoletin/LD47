using UnityEngine;

public class MainMenuManager : BaseModule
{
    [SerializeField] private MainMenuView _mainMenuView = default;

    public override void Connect(GameController controller)
    {
        _mainMenuView.Connect(controller);
    }
    
    public override void Tick(float deltaTime)
    {
    }

    public override void Utilize()
    {
        _mainMenuView.Utilize();
    }
}
