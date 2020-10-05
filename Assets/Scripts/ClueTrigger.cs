using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClueTrigger : MonoBehaviour
{
    public string _lastClue = default;
    private GuestParams _guestParams;

    private void Start()
    {
        
    }

    public void Connect(string clueToAdd, GuestParams guestParams)
    {
        _lastClue = clueToAdd;
        _guestParams = guestParams;
        gameObject.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other != null && other.CompareTag("Player"))
        {
            if (_lastClue != null)
            {
                EventManager.CallOnClueGet(_lastClue, _guestParams);
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
