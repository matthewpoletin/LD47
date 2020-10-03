using UnityEngine;

public class CommandsFactory
{
    private readonly GuestsManager _guestsManager;
    private readonly GameObject _dialogPrefab;

    public CommandsFactory(GuestsManager guestsManager, GameObject dialogPrefab)
    {
        _guestsManager = guestsManager;
        _dialogPrefab = dialogPrefab;
    }

    public TimeCommandExecute CreateTimeCommand(GuestEnterTimelineCommand command)
    {
        return new TimeCommandExecute
        {
            Time = command.Time,
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
    
    public TimeCommandExecute CreateTimeCommand(GuestLeaveTimelineCommand command)
    {
        return new TimeCommandExecute
        {
            Time = command.Time,
            Action = delegate
            {
                var guestView = _guestsManager.GetGuestView(command.GuestParams);
                var fromPosition = guestView.transform.position;
                var toPosition = _guestsManager.RightPivot.transform.position;
                guestView.PlayMovement(fromPosition, toPosition, command.Duration);
            }
        };
    }

    public TimeCommandExecute CreateTimeCommand(GuestDialogCommand command)
    {
        return new TimeCommandExecute
        {
            Time = command.Time,
            Action = delegate
            {
                var guestView = _guestsManager.GetGuestView(command.GuestParams);
                var fromPosition = guestView.transform.position;
                var toPosition = _guestsManager.RightPivot.transform.position;
                guestView.ShowDialog(command.Text);
            }
        };
    }
}