using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    [SerializeField] private List<GameObject> _tutorialScreenPrefabs;

    private CycleManager _cycleManager;

    private int _index = 0;

    public void Connect(CycleManager cycleManager)
    {
        _cycleManager = cycleManager;
    }

    public void OpenTutorialScreen()
    {
        if (_index < _tutorialScreenPrefabs.Count)
        {
            _cycleManager.Timer.Pause();
            gameObject.SetActive(true);
            StartCoroutine(ShowTutorialCoroutine());
        }
    }

    IEnumerator ShowTutorialCoroutine()
    {
        _tutorialScreenPrefabs[_index].SetActive(true);
        yield return new WaitForSeconds(3);
        _tutorialScreenPrefabs[_index].SetActive(false);
        _index++;
        gameObject.SetActive(false);
        _cycleManager.Timer.Unpause();
    }

    public void Utilize()
    {
        
    }
}
