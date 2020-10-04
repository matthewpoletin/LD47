using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GuestsManager : MonoBehaviour
{
    [SerializeField] private List<ChairView> _chairList = default;
    [SerializeField] private Transform _guestContainer = default;
    [SerializeField] private Transform _leftPivot = default;
    [SerializeField] private Transform _rightPivot = default;

    private readonly Dictionary<GuestParams, GuestView> _guests = new Dictionary<GuestParams, GuestView>();

    public Transform LeftPivot => _leftPivot;
    public Transform RightPivot => _rightPivot;

    public void Connect(List<GuestParams> guestList)
    {
        foreach (var guestParams in guestList)
        {
            CreateGuest(guestParams);
        }
    }

    public void CreateGuest(GuestParams guestParams)
    {
        var go = Instantiate(guestParams.Prefab, _guestContainer);
        var guestView = go.GetComponent<GuestView>();
        go.SetActive(false);

        _guests.Add(guestParams, guestView);
    }


    public GuestView GetGuestView(GuestParams commandGuestParams)
    {
        return _guests[commandGuestParams];
    }

    public GuestParams GetGuestByCharacter(string character)
    {
        return _guests.Keys.FirstOrDefault(GuestParams => GuestParams.Character == character);
    }

    public ChairView GetChair(int commandChairIndex)
    {
        return _chairList[commandChairIndex];
    }

    public void Reset()
    {
        foreach (var guests in _guests.Values)
        {
            guests.transform.position = _leftPivot.transform.position;
            guests.gameObject.SetActive(false);
        }
    }

    public void Utilize()
    {
        foreach (var guestView in _guests.Values)
        {
            Destroy(guestView.gameObject);
        }
        _guests.Clear();
    }
}