using System.Collections.Generic;
using System.Collections;
using System.Linq;
using UnityEngine;

public class GuestsManager : MonoBehaviour
{
    [SerializeField] private List<ChairView> _chairList = default;
    [SerializeField] private Transform _guestContainer = default;
    [SerializeField] private Transform _leftPivot = default;
    [SerializeField] private Transform _rightPivot = default;
    [SerializeField] private GameObject _clueNotificationPrefab = default;

    private readonly Dictionary<GuestParams, GuestView> _guests = new Dictionary<GuestParams, GuestView>();
    private readonly Dictionary<TextBox, float> _dialogBoxes = new Dictionary<TextBox, float>();
    private readonly Dictionary<OrderBox, float> _orderViews = new Dictionary<OrderBox, float>();

    private readonly Dictionary<OrderBox, DrinkParams> _orderDrinks = new Dictionary<OrderBox, DrinkParams>();

    // TODO: Хранить созданные диалоги миниигр
    private readonly List<MinigameView> _minigameViews = new List<MinigameView>();

    private GameModel _gameModel;
    private GameObjectPool _pool;
    private GlobalParams _globalParams;
    private CommonAssets _commonAssets;
    private Camera _camera;
    private Transform _bubbleContainer;
    private PlayerController _playerController;

    private readonly List<string> _clueList = new List<string>();
    private readonly List<GuestParams> _guestParams = new List<GuestParams>();

    private readonly List<TextBox> _textBoxesToRemove = new List<TextBox>();
    private readonly List<OrderBox> _orderBoxesToRemove = new List<OrderBox>();
    private readonly List<MinigameView> _minigamesToRemove = new List<MinigameView>();

    private float _elapsedTime;

    public Transform LeftPivot => _leftPivot;
    public Transform RightPivot => _rightPivot;
    public List<string> ClueList => _clueList;
    public List<GuestParams> GuestParamsList => _guestParams;

    public void Connect(GameModel gameModel, List<GuestParams> guestList, GameObjectPool pool,
        GlobalParams globalParams,
        CommonAssets commonAssets, Camera camera1,
        Transform bubbleContainer, PlayerController playerController)
    {
        _gameModel = gameModel;
        _pool = pool;
        _globalParams = globalParams;
        _commonAssets = commonAssets;
        _camera = camera1;
        _bubbleContainer = bubbleContainer;
        _playerController = playerController;

        foreach (var guestParams in guestList)
        {
            CreateGuest(guestParams);
        }

        EventManager.OnClueGet += AddClue;
    }

    public void Tick(float deltaTime)
    {
        foreach (var textBox in _textBoxesToRemove)
        {
            textBox.Utilize();
            _pool.UtilizeObject(textBox.gameObject);
            _dialogBoxes.Remove(textBox);
        }
        _textBoxesToRemove.Clear();

        foreach (var orderBox in _orderBoxesToRemove)
        {
            orderBox.Utilize();
            _pool.UtilizeObject(orderBox.gameObject);
            _orderViews.Remove(orderBox);
            _orderDrinks.Remove(orderBox);
        }
        _orderBoxesToRemove.Clear();

        foreach (var minigameView in _minigamesToRemove)
        {
            minigameView.Utilize();
            _minigameViews.Remove(minigameView);
            _pool.UtilizeObject(minigameView.gameObject);
        }
        _minigamesToRemove.Clear();

        _elapsedTime += deltaTime;

        foreach (var keyValuePair in _dialogBoxes)
        {
            var dialogBox = keyValuePair.Key;
            var destroyTime = keyValuePair.Value;
            if (destroyTime < _elapsedTime)
            {
                _textBoxesToRemove.Add(dialogBox);
            }
        }

        foreach (var dialogBox in _dialogBoxes.Keys)
        {
            var distance = Mathf.Abs(dialogBox.GuestView.transform.position.x - _playerController.transform.position.x);
            var alpha = Mathf.Lerp(1f, 0f,
                Mathf.InverseLerp(_globalParams.OpacityMinDistance, _globalParams.OpacityMaxDistance, distance));
            dialogBox.SetOpacity(alpha);
            dialogBox.Tick(deltaTime);
        }

        foreach (var keyValuePair in _orderViews)
        {
            var orderView = keyValuePair.Key;
            var destroyTime = keyValuePair.Value;
            if (destroyTime < _elapsedTime)
            {
                _orderBoxesToRemove.Add(orderView);
            }
        }

        foreach (var orderView in _orderViews.Keys)
        {
            var distance = Mathf.Abs(orderView.GuestView.transform.position.x - _playerController.transform.position.x);
            var alpha = Mathf.Lerp(1f, 0f,
                Mathf.InverseLerp(_globalParams.OpacityMinDistance, _globalParams.OpacityMaxDistance, distance));
            orderView.SetOpacity(alpha);
            orderView.Tick(deltaTime);

            if (distance < 1)
            {
                if (Input.GetKeyDown(KeyCode.Space) && _playerController.MovementEnabled)
                {
                    _orderBoxesToRemove.Add(orderView);

                    CreateMinigameDialogBox(orderView);

                    _playerController.MovementEnabled = false;
                }
            }

        }

        foreach (var minigameView in _minigameViews)
        {
            minigameView.Tick(deltaTime);
        }
    }

    private void CreateMinigameDialogBox(OrderBox orderView)
    {
        var minigameGo = GameObject.Instantiate(_commonAssets.MinigamePrefab, _bubbleContainer);
        var minigameView = minigameGo.GetComponent<MinigameView>();
        minigameView.Connect(_orderDrinks[orderView].sequence, OnMinigameComplete, _camera, orderView.GuestView);
        _minigameViews.Add(minigameView);
    }

    private void OnMinigameComplete(bool completionResult, MinigameView minigameView)
    {
        if (completionResult)
        {
            _gameModel.VisibleValues.CollectedMoney += 5;
        }
        else
        {
            _gameModel.VisibleValues.CollectedMoney -= 5;
        }

        _playerController.MovementEnabled = true;

        _minigamesToRemove.Add(minigameView);
    }

    private void CreateGuest(GuestParams guestParams)
    {
        var go = Instantiate(guestParams.Prefab, _guestContainer);
        var guestView = go.GetComponent<GuestView>();
        go.SetActive(false);

        _guests.Add(guestParams, guestView);
    }

    public GuestView GetGuestView(GuestParams commandGuestParams)
    {
        return _guests[commandGuestParams];
    }

    public GuestParams GetGuestByCharacter(string character)
    {
        return _guests.Keys.FirstOrDefault(guestParams => guestParams.Character == character);
    }

    public ChairView GetChair(int commandChairIndex)
    {
        return _chairList[commandChairIndex];
    }

    public void CreateDialogBox(GuestView guestView, string value, float duration)
    {
        var dialogBoxGo = _pool.GetObject(_commonAssets.DialogPrefab, _bubbleContainer);
        var dialogBox = dialogBoxGo.GetComponent<TextBox>();
        dialogBox.Connect(value, _camera, guestView);

        _dialogBoxes.Add(dialogBox, _elapsedTime + duration);
    }

    public void CreateOrderView(GuestView guestView, DrinkParams drinkParams, float duration)
    {
        var orderViewGo = _pool.GetObject(_commonAssets.OrderPrefab, _bubbleContainer);
        var orderView = orderViewGo.GetComponent<OrderBox>();
        // TODO: Иконка напитка
        orderView.Connect(guestView, _camera, drinkParams.Name, null);

        _orderViews.Add(orderView, _elapsedTime + duration);
        _orderDrinks.Add(orderView, drinkParams);
    }

    public void AddClue(string clueToAdd, GuestParams guestParams)
    {
        if (!CompareStrings(clueToAdd, _clueList))
        {
            _clueList.Add(clueToAdd);
            _guestParams.Add(guestParams);
            StartCoroutine(ClueNotificationCoroutine());
        }
    }

    IEnumerator ClueNotificationCoroutine()
    {
        _clueNotificationPrefab.gameObject.SetActive(true);
        yield return new WaitForSeconds(3);
        _clueNotificationPrefab.gameObject.SetActive(false);
    }

    public void Reset()
    {
        foreach (var guests in _guests.Values)
        {
            guests.transform.position = _leftPivot.transform.position;
            guests.gameObject.SetActive(false);
        }
    }

    public void Utilize()
    {
        _elapsedTime = default;

        foreach (var dialogBox in _dialogBoxes.Keys)
        {
            dialogBox.Utilize();
            _pool.UtilizeObject(dialogBox.gameObject);
        }

        _dialogBoxes.Clear();

        foreach (var guestView in _guests.Values)
        {
            Destroy(guestView.gameObject);
        }

        _guests.Clear();
        EventManager.OnClueGet -= AddClue;
    }

    private bool CompareStrings(string newString, List<string> clues)
    {
        foreach (var str in clues)
        {
            if (string.Compare(newString, str) == 0)
            {
                return true;
            }
        }

        return false;
    }
}