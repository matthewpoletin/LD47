using UnityEngine;
using UnityEngine.UI;

public class OrderBox : DialogBoxBase
{
    [SerializeField] private Image _drinkImage = default;

    public void Connect(GuestView guestView, Camera camera1, Sprite drinkSpite)
    {
        base.Connect(camera1, guestView);
        _drinkImage.sprite = drinkSpite;
    }
}