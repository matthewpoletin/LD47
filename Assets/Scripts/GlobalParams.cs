using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GlobalParams", menuName = "Params/GlobalParams", order = 0)]
public class GlobalParams : ScriptableObject
{
    public CommonAssets CommonAssets = default;
    public float CycleDuration = 3 * 60f;
    public List<GuestParams> GuestList = default;
    public TextAsset StorylineCsv = default;
    public float PlayerMovementSpeed = default;
    public float OpacityMinDistance = default;
    public float OpacityMaxDistance = default;
}