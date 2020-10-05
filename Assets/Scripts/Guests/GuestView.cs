using DG.Tweening;
using UnityEngine;
using System.Collections;

public class GuestView : MonoBehaviour
{
    [SerializeField] private Transform _topPlaceholder = default;
    [SerializeField] private GameObject _clueTrigger = default;

    public Transform TopPlaceholder => _topPlaceholder;

    public void PlayMovement(Vector3 fromPosition, Vector3 toPosition, float duration, bool stayActive = true)
    {
        var movementSequence = DOTween.Sequence();
        movementSequence.OnStart(() =>
        {
            gameObject.SetActive(true);
            transform.position = fromPosition;
        });
        movementSequence.Insert(0f, transform.DOMove(toPosition, duration));
        movementSequence.OnComplete(() => gameObject.SetActive(stayActive));
        movementSequence.Play();
    }

    public void TryToGetClue(string clueToAdd, float duration)
    {
        StartCoroutine(ClueTriggerCoroutine(clueToAdd, duration));
    }

    IEnumerator ClueTriggerCoroutine(string clueToAdd, float duration)
    {
        ClueTrigger clueTrigger = _clueTrigger.GetComponent<ClueTrigger>();
        //clueTrigger._lastClue = clueToAdd;
        //clueTrigger.gameObject.SetActive(true);
        clueTrigger.Connect(clueToAdd);
        yield return new WaitForSeconds(duration);
        clueTrigger.Utilize();
    }
}