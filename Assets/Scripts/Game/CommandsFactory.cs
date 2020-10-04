using UnityEngine;
using System.Collections.Generic;

public class CommandsFactory
{
    private readonly GuestsManager _guestsManager;
    private readonly GameObject _dialogPrefab;
    private readonly Transform _bubbleContainer;
    private List<string> _currentClues;

    public CommandsFactory(GuestsManager guestsManager, GameObject dialogPrefab, Transform bubbleContainer)
    {
        _guestsManager = guestsManager;
        _dialogPrefab = dialogPrefab;
        _bubbleContainer = bubbleContainer;

        _currentClues = new List<string>();
    }

    public TimeCommandExecute CreateTimeCommand(GuestEnterCommand command)
    {
        return new TimeCommandExecute
        {
            Time = command.StartTime,
            Action = delegate
            {
                var guestView = _guestsManager.GetGuestView(command.GuestParams);
                var chair = _guestsManager.GetChair(command.ChairIndex);
                var toPosition = chair.transform.position;
                var fromPosition = _guestsManager.RightPivot.transform.position;
                guestView.PlayMovement(fromPosition, toPosition, command.Duration);
            }
        };
    }
    
    public TimeCommandExecute CreateTimeCommand(GuestLeaveCommand command)
    {
        return new TimeCommandExecute
        {
            Time = command.StartTime,
            Action = delegate
            {
                var guestView = _guestsManager.GetGuestView(command.GuestParams);
                var fromPosition = guestView.transform.position;
                var toPosition = _guestsManager.RightPivot.transform.position;
                guestView.PlayMovement(fromPosition, toPosition, command.Duration);
            }
        };
    }

    public TimeCommandExecute CreateTimeCommand(GuestTalkCommand command)
    {
        return new TimeCommandExecute
        {
            Time = command.StartTime,
            Action = delegate
            {
                var guestView = _guestsManager.GetGuestView(command.GuestParams);
                var go = GameObject.Instantiate(_dialogPrefab, _bubbleContainer);
                var dialogBox =  go.GetComponent<DialogBox>();
                dialogBox.Connect(command.TextEng, guestView.TopPlaceholder, command.Duration);
            }
        };
    }

    private bool CompareStrings(string newString)
    {
        foreach (var str in _currentClues)
        {
            if (string.Compare(newString, str) == 0)
            {
                return true;
            }
        }

        return false;
    }
}
