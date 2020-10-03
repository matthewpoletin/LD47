using DG.Tweening;
using UnityEngine;

public class GuestView : MonoBehaviour
{
    [SerializeField] private Transform _topPlaceholder = default;

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

    public void ShowDialog(string commandText)
    {
        
        throw new System.NotImplementedException();
    }
}