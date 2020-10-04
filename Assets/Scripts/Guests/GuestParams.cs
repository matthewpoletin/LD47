using UnityEngine;

[CreateAssetMenu(fileName = "GuestParams", menuName = "Params/GuestParams", order = 5)]
public class GuestParams : ScriptableObject
{
    public string Character = default;
    public string Name = default;
    public GameObject Prefab = default;
}