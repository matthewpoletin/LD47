using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Report : MonoBehaviour
{
    [SerializeField] private GameObject _cluePrefab;
    [SerializeField] private Transform _clueContainer;
    [SerializeField] private List<ClueTab> _clueTabs;
    [SerializeField] private List<ClueEntryView> _conclusionClues;
    [SerializeField] private ClueTab _activeTab;

    private List<ClueEntryView> _clueEntryList = new List<ClueEntryView>();
    private List<string> _currentClues = new List<string>();

    private GameController _gameController;

    private int _index = 0;

    public void Connect(GameController controller)
    {
        _gameController = controller;
    }

    public void StartAddingClues(List<string> clueList, List<GuestParams> guestParams)
    {
        if (clueList.Count <= 0) { return; }

        for (int i = 0; i < clueList.Count; i++)
        {
            AddClue(clueList[i], guestParams[i]);
        }
    }

    public void AddClue(string clueToAdd, GuestParams guestParams)
    {
        if (CompareStrings(clueToAdd)) { return; }

        foreach (var tab in _clueTabs)
        {
            if (tab.name == "Conclusions" && CheckForConclusion(clueToAdd))
            {
                AddToTab(clueToAdd, tab.transform);
                tab.CheckForSummary(clueToAdd);
            }

            if (guestParams.Name == tab.name)
            {
                AddToTab(clueToAdd, tab.transform);
                tab.CheckForSummary(clueToAdd);
            }
        }
    }

    private void AddToTab(string clueToAdd, Transform tabTransform)
    {
        var go = Instantiate(_cluePrefab.gameObject, tabTransform);
        ClueEntryView clueEntry = go.GetComponent<ClueEntryView>();
        clueEntry.Connect(clueToAdd);
        _currentClues.Add(clueToAdd);
        _clueEntryList.Add(clueEntry);
    }

    private bool CheckForConclusion(string clue)
    {
        foreach (var conclusion in _conclusionClues)
        {
            if (conclusion.Text.text == clue)
            {
                return true;
            }
        }

        return false;
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

    public void OpenNewTab(GameObject newTab)
    {
        foreach (var tab in _clueTabs)
        {
            tab.gameObject.SetActive(false);
            tab.transform.parent.gameObject.SetActive(false);
        }

        newTab.gameObject.SetActive(true);
        _activeTab = newTab.transform.GetChild(0).GetComponent<ClueTab>();
        _activeTab.gameObject.SetActive(true);
    }

    public void CallThePolice(GameObject policeButton)
    {
        policeButton.gameObject.SetActive(false);
        string message = "Police arrest " + _activeTab.name + ". Loop repeats.";
        AddToTab(message, _activeTab.transform);
        gameObject.SetActive(false);
        EventManager.CallOnCallPolice();
    }

    public void Sleep(GameObject soberupButton)
    {
        soberupButton.gameObject.SetActive(false);
        string message = "You mix cocktail, which put to sleep Blonde. You left temporary loop. Until new meeting ;)";
        AddToTab(message, _activeTab.transform);

        if (_activeTab._endGameButton != null)
        {
            _activeTab._endGameButton.SetActive(true);
        }
    }

    public void EndGame()
    {
        Debug.Log("Credits");
    }
}
