using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClueEntryView : MonoBehaviour
{
    [SerializeField] private Text _text;

    public Text Text => _text;

    public void Connect(string newString)
    {
        _text.text = newString;
    }
}
