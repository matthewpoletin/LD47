using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Report : MonoBehaviour
{
    [SerializeField] private GameObject _cluePrefab;

    private List<ClueEntryView> _clueEntryList = new List<ClueEntryView>();
    private List<string> _currentClues = new List<string>();

    private int _index = 0;

    public void Connect(List<string> clueList)
    {
        foreach (var clue in clueList)
        {
            AddClue(clue);
        }
    }

    public void AddClue(string clueToAdd)
    {
        if (!CompareStrings(clueToAdd))
        {
            var go = Instantiate(_cluePrefab.gameObject, gameObject.transform);
            ClueEntryView clueEntry = go.GetComponent<ClueEntryView>();
            clueEntry.Connect(clueToAdd);
            _currentClues.Add(clueToAdd);
            _clueEntryList.Add(clueEntry);
        }
    }

    public void Utilize()
    {
        foreach (var entry in _clueEntryList)
        {
            Destroy(entry.gameObject);
        }
        _clueEntryList.Clear();
    }

    private bool CompareStrings(string newString)
    {
        foreach (var str in _currentClues)
        {
            if (string.Compare(newString, str) == 0)
            {
                return true;
            }
        }

        return false;
    }
}
