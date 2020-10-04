using UnityEngine;

public class CycleManager
{
    private readonly GuestsManager _guestsManager;
    private readonly float _cycleDuration;

    private readonly CommandsFactory _commandsFactory;

    private readonly ClueManager _clueManager;

    private TimeCommandManager _timeline;

    private readonly Timer _timer;
    private TextAsset _storylineData;

    public Timer Timer => _timer;

    public CycleManager(GameController controller, GameObjectPool pool, GuestsManager guestsManager,
        Transform bubbleContainer, TextAsset storylineData, PlayerController playerController, Camera camera)
    {
        _guestsManager = guestsManager;
        _cycleDuration = controller.GlobalParams.CycleDuration;
        _storylineData = storylineData;

        _timer = new Timer(_cycleDuration);
        _timer.OnTimerElapsed += OnTimerElapsed;

        _timeline = new TimeCommandManager(_timer);
        _commandsFactory = new CommandsFactory(_guestsManager, controller.GlobalParams.CommonAssets.DialogPrefab,
            bubbleContainer, playerController, camera);

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
        _guestsManager.Reset();

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
                        _timeline.AddCommand(_commandsFactory.CreateTimeCommand(new GuestEnterCommand
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
                        _timeline.AddCommand(_commandsFactory.CreateTimeCommand(new GuestLeaveCommand()
                        {
                            GuestParams = guestParams,
                            StartTime = (int) storylineEntry["StartTime"],
                            Duration = (int) storylineEntry["Duration"],
                        }));
                        break;
                    }
                    case "Talk":
                    {
                        _timeline.AddCommand(_commandsFactory.CreateTimeCommand(new GuestTalkCommand()
                        {
                            GuestParams = guestParams,
                            StartTime = (int) storylineEntry["StartTime"],
                            Duration = (int) storylineEntry["Duration"],
                            TextEng = (string) storylineEntry["TextEng"],
                            TextRus = (string) storylineEntry["TextRus"],
                        }));
                        break;
                    }
                    case "Clue":
                    {
                        _timeline.AddCommand(_commandsFactory.CreateTimeCommand(new GuestClueCommand()
                        {
                            GuestParams = guestParams,
                            StartTime = (int) storylineEntry["StartTime"],
                            Duration = (int) storylineEntry["Duration"],
                            ReportEng = (string) storylineEntry["TextEng"],
                            ReportRus = (string) storylineEntry["TextRus"],
                        }));
                        break;
                    }
                    case "Order":
                    {
                        // TODO:
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