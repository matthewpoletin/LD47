using UnityEngine;

[CreateAssetMenu(fileName = "GuestParams", menuName = "Params/GuestParams", order = 5)]
public class GuestParams : ScriptableObject
{
    public string Name = default;
    public Sprite Avatar = default;
    public GameObject Prefab = default;
}