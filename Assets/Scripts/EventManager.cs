﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EventManager
{
    public static Action<string> OnGuestEnterClue = delegate { };
    public static Action<string> OnGuestTalkClue = delegate { };

    public static void CallOnGuestEnterClue(string clueToAdd)
    {
        OnGuestEnterClue?.Invoke(clueToAdd);
    }

    public static void CallOnGuestTalkClue(string clueToAdd)
    {
        OnGuestTalkClue?.Invoke(clueToAdd);
    }
}