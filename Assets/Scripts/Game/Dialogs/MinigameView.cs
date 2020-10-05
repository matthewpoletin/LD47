using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinigameView : MonoBehaviour
{
    [SerializeField] private Sprite idleImg = default;
    [SerializeField] private Sprite pressedImg = default;

    private string seq;

    private GameObject parent;

    private Action<bool> _callBack;

    private bool shown = false;
    private bool integrity = true;
    private int counter = 0;

    private Image a;
    private Dictionary<string, Image> uberDict = new Dictionary<string, Image>();

    private void Awake()
    {
        uberDict.Add("W", GameObject.Find("Up").GetComponent<Image>());
        uberDict.Add("S", GameObject.Find("Down").GetComponent<Image>());
        uberDict.Add("A", GameObject.Find("Left").GetComponent<Image>());
        uberDict.Add("D", GameObject.Find("Right").GetComponent<Image>());
    }

    public void Connect(string sequence, Action<bool> callback)
    {
        seq = sequence;
        _callBack = callback;

        ShowSequence();
    }

    private void Update()
    {
        switch (shown)
        {
            case true:
                foreach (KeyCode vKey in Enum.GetValues(typeof(KeyCode)))
                {
                    if (Input.GetKeyUp(vKey))
                    {
                        if (vKey.ToString() != ("" + seq[counter]))
                        {
                            integrity = false;
                        }

                        counter += 1;
                        if (counter >= seq.Length)
                        {
                            shown = false;
                            _callBack?.Invoke(integrity);
                            Destroy(gameObject);
                        }
                    }
                }

                break;
        }
    }

    private void ShowSequence()
    {
        for (int i = 0; i < seq.Length; i++)
        {
            IEnumerator coroutine = Blink(i, pressedImg, 0.3f * (i + 1));
            StartCoroutine(coroutine);
        }
    }

    private IEnumerator Blink(int i, Sprite img, float ttl)
    {
        yield return new WaitForSeconds(ttl);
        uberDict["" + seq[i]].sprite = img;
        if (i != seq.Length)
        {
            IEnumerator coroutine = Blink(i, idleImg, 0.3f);
            StartCoroutine(coroutine);
        }

        if (i == seq.Length - 1)
        {
            shown = true;
        }
    }
}