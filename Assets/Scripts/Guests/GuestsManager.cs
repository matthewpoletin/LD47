using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GuestsManager : MonoBehaviour
{
    [SerializeField] private List<ChairView> _chairList = default;
    [SerializeField] private Transform _guestContainer = default;
    [SerializeField] private Transform _leftPivot = default;
    [SerializeField] private Transform _rightPivot = default;
    [SerializeField] private Transform _clueNotificationPivot = default;

    private readonly Dictionary<GuestParams, GuestView> _guests = new Dictionary<GuestParams, GuestView>();
    private readonly Dictionary<DialogBox, float> _dialogBoxes = new Dictionary<DialogBox, float>();

    private GameObjectPool _pool;
    private GlobalParams _globalParams;
    private CommonAssets _commonAssets;
    private Camera _camera;
    private Transform _bubbleContainer;
    private PlayerController _playerController;

    private float _elapsedTime;

    public Transform LeftPivot => _leftPivot;
    public Transform RightPivot => _rightPivot;
    public Transform ClueNotificationPivot => _clueNotificationPivot;

    public void Connect(List<GuestParams> guestList, GameObjectPool pool, GlobalParams globalParams,
        CommonAssets commonAssets, Camera camera1,
        Transform bubbleContainer, PlayerController playerController)
    {
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
    }

    public void Tick(float deltaTime)
    {
        _elapsedTime += deltaTime;

        var dialogsToRemove = new List<DialogBox>();
        foreach (var keyValuePair in _dialogBoxes)
        {
            var dialogBox = keyValuePair.Key;
            var destroyTime = keyValuePair.Value;
            if (destroyTime < _elapsedTime)
            {
                dialogsToRemove.Add(dialogBox);
            }
        }

        foreach (var dialogBox in dialogsToRemove)
        {
            _pool.UtilizeObject(dialogBox.gameObject);
            _dialogBoxes.Remove(dialogBox);
        }

        foreach (var dialogBox in _dialogBoxes.Keys)
        {
            var distance = Mathf.Abs(dialogBox.GuestView.transform.position.x - _playerController.transform.position.x);
            var alpha = Mathf.Lerp(1f, 0f, Mathf.InverseLerp(_globalParams.OpacityMinDistance, _globalParams.OpacityMaxDistance, distance));
            dialogBox.SetOpacity(alpha);
            dialogBox.Tick(deltaTime);
        }
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
        var dialogBox = dialogBoxGo.GetComponent<DialogBox>();
        dialogBox.Connect(value, _camera, guestView);

        _dialogBoxes.Add(dialogBox, _elapsedTime + duration);
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
    }
}