using System.Collections.Generic;
using UnityEngine;

public class GuestsManager : MonoBehaviour
{
    private List<GuestView> _activeGuestsList = new List<GuestView>();

    public void AddGuest(GuestParams guestParams, Transform container)
    {
        var go = GameObject.Instantiate(guestParams.Prefab, container);
        var guestView = go.GetComponent<GuestView>();

        _activeGuestsList.Add(guestView);
    }

    public void RemoveGuest(GuestView guestView)
    {
        // _activeGuestsList.Remove()
    }
}