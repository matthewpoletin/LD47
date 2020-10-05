using System;

public class GameModel
{
    public class Values
    {
        private int _collectedMoney = 0;

        public int CollectedMoney
        {
            get => _collectedMoney;
            set
            {
                _collectedMoney = value;
                OnCollectedMoneyChanged?.Invoke();
            }
        }

        public event Action OnCollectedMoneyChanged;
    }

    public Values VisibleValues { get; }

    public GameModel()
    {
        VisibleValues = new Values();
    }
}