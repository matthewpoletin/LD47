using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClueManager
{
    private GameObject _clueBubblePrefab;
    private Transform _cluePositionContainer;

    public ClueManager(GameObject dialogPrefab, Transform bubbleContainer)
    {
        _clueBubblePrefab = dialogPrefab;
        _cluePositionContainer = bubbleContainer;
    }
}
