using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GuestTimelinePoint
{
    public float Time = default;
    public GuestParams GuestParams = default;
}

[CreateAssetMenu(fileName = "GuestTimelineParams", menuName = "Params/GuestTimelineParams", order = 10)]
public class GuestTimelineParams : ScriptableObject
{
    public List<GuestTimelinePoint> TimelinePoints = default;
}