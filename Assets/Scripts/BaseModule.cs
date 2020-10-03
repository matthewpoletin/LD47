using UnityEngine;

public abstract class BaseModule : MonoBehaviour
{
    public abstract void Connect(GameController controller);
    public abstract void Tick(float deltaTime);
    public abstract void Utilize();
}
