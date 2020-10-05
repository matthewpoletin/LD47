using UnityEngine;
using UnityEngine.UI;

public class TextBox : DialogBoxBase
{
    [SerializeField] private Text _text = default;

    public void Connect(string value, Camera camera1, ICharacter guestView)
    {
        base.Connect(camera1, guestView);

        _text.text = value;
    }

    public new void Utilize()
    {
        base.Utilize();

        _text.text = "";
    }
}