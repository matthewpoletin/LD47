using UnityEngine;
using UnityEngine.UI;

public class OrderBox : DialogBoxBase
{
    [SerializeField] private Text _orderText = default;
    [SerializeField] private Image _drinkImage = default;

    public void Connect(GuestView guestView, Camera camera1, string drinkName, Sprite drinkSpite)
    {
        base.Connect(camera1, guestView);
        _orderText.text = $"Order: {drinkName}";
        _drinkImage.sprite = drinkSpite;
    }
}