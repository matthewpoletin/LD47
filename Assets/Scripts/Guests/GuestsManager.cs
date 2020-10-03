using UnityEngine;

public class GuestsManager : MonoBehaviour
{
    public GuestView CreateGuest(GuestParams guestParams, Transform container)
    {
        var go = GameObject.Instantiate(guestParams.Prefab, container);
        var guestView = go.GetComponent<GuestView>();
        return guestView;
    }
}