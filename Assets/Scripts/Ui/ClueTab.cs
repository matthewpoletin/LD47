﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClueTab : MonoBehaviour
{
    public string name = default;
    [SerializeField] private int _maxNumberOfClues = default;
    [SerializeField] private GameObject _summaryPrefab;
    [SerializeField] private GameObject _policeButton;
    private List<string> _currentClues = new List<string>();

    public void CheckForSummary(string newClue)
    {
        _currentClues.Add(newClue);

        if (_currentClues.Count >= _maxNumberOfClues)
        {
            Instantiate(_summaryPrefab, gameObject.transform);
            if (_policeButton != null)
            {
                _policeButton.SetActive(true);
            }
        }
    }
}
