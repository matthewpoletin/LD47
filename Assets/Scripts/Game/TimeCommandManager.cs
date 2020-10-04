using System.Collections.Generic;

public class TimeCommandManager
{
    private readonly Timer _timer;

    private readonly List<TimeCommandExecute> _commands = new List<TimeCommandExecute>();

    public TimeCommandManager(Timer timer)
    {
        _timer = timer;
    }

    public void AddCommand(TimeCommandExecute timeCommand)
    {
        _commands.Add(timeCommand);
    }

    public void Tick(float deltaTime)
    {
        for (var index = _commands.Count - 1; index >= 0; index--)
        {
            var activity = _commands[index];
            if (activity.StartTime < _timer.TimePassed)
            {
                activity.Action?.Invoke();
                _commands.RemoveAt(index);
            }
        }
    }

    public void Utilize()
    {
        _commands.Clear();
    }
}