using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EventManager
{
    public static Action<string> OnGuestEnterClue = delegate { };
    public static Action<string> OnGuestTalkClue = delegate { };
    public static Action<string, GuestParams> OnClueGet = delegate { };
    public static Action OnCallPolice = delegate { };

    public static void CallOnGuestEnterClue(string clueToAdd)
    {
        OnGuestEnterClue?.Invoke(clueToAdd);
    }

    public static void CallOnGuestTalkClue(string clueToAdd)
    {
        OnGuestTalkClue?.Invoke(clueToAdd);
    }

    public static void CallOnClueGet(string clueToAdd, GuestParams guestParams)
    {
        OnClueGet?.Invoke(clueToAdd, guestParams);
    }

    public static void CallOnCallPolice()
    {
        OnCallPolice?.Invoke();
    }
}
