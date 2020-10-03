using UnityEngine;

public class ApplicationManager : MonoBehaviour
{
    [SerializeField] private GlobalParams _globalParams = default;
    [SerializeField] private GuestsManager _guestsManager = default;

    private void Start()
    {
        var guestView = _guestsManager.CreateGuest(_globalParams.GuestList[0], _guestsManager.transform);
    }
}