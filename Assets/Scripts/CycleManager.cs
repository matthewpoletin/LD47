using System.Linq;
using UnityEngine;

public class CycleManager
{
    private GameController _controller;
    private readonly GuestsManager _guestsManager;
    private readonly float _cycleDuration;

    private readonly CommandsFactory _commandsFactory;

    private readonly ClueManager _clueManager;

    private TimeCommandManager _timeline;

    private readonly Timer _timer;
    private TextAsset _storylineData;
    private Transform _minigameContainer;

    public Timer Timer => _timer;

    public CycleManager(GameController controller, GameObjectPool pool, GuestsManager guestsManager,
        Transform bubbleContainer,
        Transform minigameContainer, TextAsset storylineData, PlayerController playerController, Camera camera)
    {
        _controller = controller;
        _guestsManager = guestsManager;
        _cycleDuration = controller.GlobalParams.CycleDuration;
        _storylineData = storylineData;
        _minigameContainer = minigameContainer;

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

        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    var minigameGo =
        //        GameObject.Instantiate(_controller.GlobalParams.CommonAssets.MinigamePrefab, _minigameContainer);
        //    var minigameView = minigameGo.GetComponent<MinigameView>();
        //    minigameView.Connect("WASD", OnMinigameComplete);
        //}
    }

    // TODO: Перенести внутрь GuestManager и при вызове включать перемещение игрока
  

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
                            ReportEng = (string) storylineEntry["ReportEng"],
                            ReportRus = (string) storylineEntry["ReportRus"],
                        }));
                        break;
                    }
                    case "Order":
                    {
                        var drinkName = (string) storylineEntry["Drink"];
                        var drinkParams = _controller.GlobalParams.DrinkList.FirstOrDefault(d => d.Name == drinkName);
                        if (drinkParams == null)
                        {
                            Debug.LogError($"Drink not found: {drinkName}");
                            continue;
                        }

                        _timeline.AddCommand(_commandsFactory.CreateTimeCommand(new GuestOrderCommand()
                        {
                            StartTime = (int) storylineEntry["StartTime"],
                            GuestParams = guestParams,
                            DrinkParams = drinkParams,
                            Duration = (int) storylineEntry["Duration"],
                        }));
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