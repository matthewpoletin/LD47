using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GlobalParams", menuName = "Params/GlobalParams", order = 0)]
public class GlobalParams : ScriptableObject
{
    public List<GuestParams> GuestList = default;
}