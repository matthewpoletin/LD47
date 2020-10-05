using UnityEngine;

public abstract class BaseModule : MonoBehaviour
{
    [SerializeField] private Transform _dialogsContainer = default;

    public Transform DialogsContainer => _dialogsContainer;

    public abstract void Connect(GameController controller);
    public abstract void Tick(float deltaTime);
    public abstract void Utilize();
}
