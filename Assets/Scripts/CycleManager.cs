using UnityEngine;

public class CycleManager
{
    private readonly GuestsManager _guestsManager;
    private readonly float _cycleDuration;
    private readonly GuestTimelineParams _guestTimeline;

    private readonly CommandsFactory _commandsFactory;

    private TimeCommandManager _timeline;

    private readonly Timer _timer;
    private TextAsset _storylineData;

    public Timer Timer => _timer;

    public CycleManager(GameController controller, GuestsManager guestsManager, Transform bubbleContainer,
        TextAsset storylineData)
    {
        _guestsManager = guestsManager;
        _cycleDuration = controller.GlobalParams.CycleDuration;
        _guestTimeline = controller.GlobalParams.GuestTimelineParams;
        _storylineData = storylineData;

        _timer = new Timer(_cycleDuration);
        _timer.OnTimerElapsed += OnTimerElapsed;

        _timeline = new TimeCommandManager(_timer);
        _commandsFactory = new CommandsFactory(_guestsManager, controller.GlobalParams.CommonAssets.DialogPrefab,
            bubbleContainer);

        Restart();
    }

    public void Tick(float deltaTime)
    {
        _timer.Tick(deltaTime);
        _timeline.Tick(deltaTime);
    }

    private void OnTimerElapsed()
    {
        // TODO: Конец цикла
        Restart();
    }

    public void Restart()
    {
        _timer.Reset(_cycleDuration);
        _timer.Unpause();

        var storylineDict = CsvReader.Read(_storylineData);
        foreach (var storylineEntry in storylineDict)
        {
            if (storylineEntry.TryGetValue("EventType", out var eventType))
            {
                var eventTypeStr = (string) eventType;
                if (string.IsNullOrEmpty(eventTypeStr))
                {
                    continue;
                }

                var characterStr = (string) storylineEntry["Character"];
                var guestParams = _guestsManager.GetGuestByCharacter(characterStr);
                if (guestParams == null)
                {
                    Debug.LogError($"Guest not found by character {characterStr}");
                    continue;
                }

                switch (eventTypeStr)
                {
                    case "Enter":
                    {
                        _timeline.AddCommand(_commandsFactory.CreateTimeCommand(new GuestEnterTimelineCommand
                        {
                            GuestParams = guestParams,
                            StartTime = (int) storylineEntry["StartTime"],
                            Duration = (int) storylineEntry["Duration"],
                            ChairIndex = (int) storylineEntry["ChairIndex"],
                        }));

                        break;
                    }
                    case "Leave":
                    {
                        break;
                    }
                    case "Talk":
                    {
                        break;
                    }
                    case "Clue":
                    {
                        break;
                    }
                    default:
                    {
                        Debug.LogError($"Can't handle storyline entry of type {eventType}");
                        break;
                    }
                }
            }
        }
    }

    public void Utilize()
    {
        _timeline.Utilize();
    }
}