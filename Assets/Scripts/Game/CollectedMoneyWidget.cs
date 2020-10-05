using UnityEngine;
using UnityEngine.UI;

public class CollectedMoneyWidget : MonoBehaviour
{
    [SerializeField] private Text _text = default;

    private GameModel _gameModel;

    public void Connect(GameModel gameModel)
    {
        _gameModel = gameModel;

        _gameModel.VisibleValues.OnCollectedMoneyChanged += OnCollectedMoneyChanged;
        OnCollectedMoneyChanged();
    }

    private void OnCollectedMoneyChanged()
    {
        _text.text = $"${_gameModel.VisibleValues.CollectedMoney.ToString()}";
    }

    public void Utilize()
    {
        _gameModel.VisibleValues.OnCollectedMoneyChanged -= OnCollectedMoneyChanged;
    }
}