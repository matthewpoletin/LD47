using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Report : MonoBehaviour
{
    [SerializeField] private GameObject _cluePrefab;

    private GameController _controller;
    private List<ClueEntryView> _clueEntryList = new List<ClueEntryView>();

    private int _index = 0;

    public void Connect(GameController controller, List<string> clueList)
    {
        _controller = controller;

        foreach (var clue in clueList)
        {
            AddClue(clue);
        }
    }

    public void AddClue(string clueToAdd)
    {
        var go = Instantiate(_cluePrefab.gameObject, gameObject.transform);
        ClueEntryView clueEntry = go.GetComponent<ClueEntryView>();
        clueEntry.Connect(clueToAdd);
        _clueEntryList.Add(clueEntry);
    }

    public void Utilize()
    {
        foreach (var entry in _clueEntryList)
        {
            Destroy(entry.gameObject);
        }
        _clueEntryList.Clear();
    }
}
