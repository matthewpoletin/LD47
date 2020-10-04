using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Report : MonoBehaviour
{
    [SerializeField] private List<Text> _clueContainers = default;
    private GameController _controller;

    private int _index = 0;

    public void Connect(GameController controller)
    {
        _controller = controller;
    }

    public void AddClue(string clueToAdd)
    {
        if (_index <= _clueContainers.Count)
        {
            _clueContainers[_index].text = clueToAdd;
            _index++;
        }
    }
}
