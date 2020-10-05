using System.Collections.Generic;
using UnityEngine;

public class CommandsFactory
{
    private readonly GuestsManager _guestsManager;
    private readonly GameObject _dialogPrefab;
    private readonly Transform _bubbleContainer;
    private readonly PlayerController _playerController;
    private readonly Camera _camera;

    private List<string> _currentClues;

    public CommandsFactory(GuestsManager guestsManager, GameObject dialogPrefab, Transform bubbleContainer,
        PlayerController playerController, Camera camera)
    {
        _guestsManager = guestsManager;
        _dialogPrefab = dialogPrefab;
        _bubbleContainer = bubbleContainer;
        _playerController = playerController;
        _camera = camera;

        _currentClues = new List<string>();
    }

    public TimeCommandExecute CreateTimeCommand(GuestEnterCommand command)
    {
        return new TimeCommandExecute
        {
            StartTime = command.StartTime,
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
            StartTime = command.StartTime,
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
            StartTime = command.StartTime,
            Action = delegate
            {
                var guestView = _guestsManager.GetGuestView(command.GuestParams);
                _guestsManager.CreateDialogBox(guestView, command.TextEng, command.Duration);
            }
        };
    }

    public TimeCommandExecute CreateTimeCommand(GuestClueCommand command)
    {
        return new TimeCommandExecute
        {
            StartTime = command.StartTime,
            Action = delegate
            {
                var guestView = _guestsManager.GetGuestView(command.GuestParams);
                guestView.TryToGetClue(command.ReportEng, command.Duration, command.GuestParams);
            }
        };
    }

    public TimeCommandExecute CreateTimeCommand(GuestOrderCommand command)
    {
        return new TimeCommandExecute()
        {
            StartTime = command.StartTime,
            Action = delegate
            {
                var guestView = _guestsManager.GetGuestView(command.GuestParams);
                _guestsManager.CreateOrderView(guestView, command.DrinkParams, command.Duration);
            }
        };
    }
}