using DG.Tweening;
using UnityEngine;
using System.Collections;

public class GuestView : MonoBehaviour, ICharacter
{
    [SerializeField] private Transform _topPlaceholder = default;
    [SerializeField] private GameObject _clueTrigger = default;
    [SerializeField] private Animator _animator = default;
    [SerializeField] private Transform _rootView = default;

    public Transform TopPlaceholder => _topPlaceholder;
    public Vector3 Position => transform.position;

    public void PlayMovement(Vector3 fromPosition, Vector3 toPosition, float duration, bool stayActive = true)
    {
        var movementSequence = DOTween.Sequence();
        movementSequence.OnStart(() =>
        {
            gameObject.SetActive(true);
            transform.position = fromPosition;

            var xTo = toPosition.x;

            var xFrom = fromPosition.x;

            if (xFrom < xTo)
            {
                StartCoroutine(Rotate(90, 1));
//                _rootView.Rotate(0, 90, 0);
            }
            else
            {
                StartCoroutine(Rotate(-90, 1));
//                _rootView.Rotate(0, -90, 0);
            }

            _animator.SetTrigger("Walk");
        });
        movementSequence.Insert(0f, transform.DOMove(toPosition, duration));
        movementSequence.OnComplete(() =>
        {
            gameObject.SetActive(stayActive);

            _animator.SetTrigger("Sitting");

            var xTo = toPosition.x;

            var xFrom = fromPosition.x;

            StartCoroutine(Rotate(-90, 1));
//            _rootView.Rotate(0, -90, 0);
        });
        movementSequence.Play();
    }

    private IEnumerator Rotate(float angle, float duration)
    {
        float startRotation = _rootView.eulerAngles.y;
        float endRotation = startRotation + angle;
        float t = 0.0f;
        while (t < duration)
        {
            t += Time.deltaTime;
            float yRotation = Mathf.Lerp(startRotation, endRotation, t / duration) % 360.0f;
            _rootView.eulerAngles = new Vector3(_rootView.eulerAngles.x, yRotation,
            _rootView.eulerAngles.z);
            yield return null;
        }
    }

    public void TryToGetClue(string clueToAdd, float duration, GuestParams guestParams)
    {
        StartCoroutine(ClueTriggerCoroutine(clueToAdd, duration, guestParams));
    }

    IEnumerator ClueTriggerCoroutine(string clueToAdd, float duration, GuestParams guestParams)
    {
        ClueTrigger clueTrigger = _clueTrigger.GetComponent<ClueTrigger>();
        clueTrigger.Connect(clueToAdd, guestParams);
        yield return new WaitForSeconds(duration);
        clueTrigger.Utilize();
    }
}