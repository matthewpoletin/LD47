using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GuestEnterTimelineCommand
{
    public float Time = default;
    public GuestParams GuestParams = default;
    public int ChairIndex = 0;
    public float Duration = 1f;
}

[Serializable]
public class GuestLeaveTimelineCommand
{
    public float Time = default;
    public GuestParams GuestParams = default;
    public float Duration = 1f;
}

[Serializable]
public class GuestDialogCommand
{
    public float Time = default;
    public GuestParams GuestParams = default;
    public string Text = default;
    public float Duration = 5f;
}

[CreateAssetMenu(fileName = "GuestTimelineParams", menuName = "Params/GuestTimelineParams", order = 10)]
public class GuestTimelineParams : ScriptableObject
{
    public List<GuestEnterTimelineCommand> GuestAppear = default;
    public List<GuestLeaveTimelineCommand> GuestLeave = default;
    public List<GuestDialogCommand> GuestDialog = default;
}