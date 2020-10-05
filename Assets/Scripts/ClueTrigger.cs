using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClueTrigger : MonoBehaviour
{
    public string _lastClue = default;

    private void Start()
    {
        
    }

    public void Connect(string clueToAdd)
    {
        _lastClue = clueToAdd;
        gameObject.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other != null && other.CompareTag("Player"))
        {
            if (_lastClue != null)
            {
                EventManager.CallOnClueGet(_lastClue);
                Utilize(); 
            }
        }
    }

    public void Utilize()
    {
        _lastClue = null;
        gameObject.SetActive(false);
    }
}
