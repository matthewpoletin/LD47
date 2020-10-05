using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClueTab : MonoBehaviour
{
    public string name = default;
    [SerializeField] private int _maxNumberOfClues = default;
    [SerializeField] private GameObject _summaryPrefab;

    public GameObject _policeButton;
    public GameObject _sleepButton;

    public GameObject _endGameButton;

    private List<string> _currentClues = new List<string>();

    public void CheckForSummary(string newClue)
    {
        _currentClues.Add(newClue);

        if (_currentClues.Count >= _maxNumberOfClues)
        {
            Instantiate(_summaryPrefab, gameObject.transform);

            // да, да, я знаю, что это зашквар, но быстро придумать другого решения не смог
            ActivateButton(_policeButton);
            ActivateButton(_sleepButton);
        }
    }

    private void ActivateButton(GameObject buttonToActivate)
    {
        if (buttonToActivate != null)
        {
            buttonToActivate.SetActive(true);
        }
    }
}
