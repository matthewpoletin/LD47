using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinigameBox : DialogBoxBase
{
    [SerializeField] private Sprite idleImg = default;
    [SerializeField] private Sprite pressedImg = default;

    [Space]
    [SerializeField] private Image _upImage = default;
    [SerializeField] private Image _downImage = default;
    [SerializeField] private Image _leftImage = default;
    [SerializeField] private Image _rightImage = default;

    private string seq;

    private GameObject parent;

    private Action<bool, MinigameBox> _callBack;

    private bool shown = false;
    private bool integrity = true;
    private int counter = 0;

    private Dictionary<string, Image> uberDict = new Dictionary<string, Image>();

    private void Awake()
    {
        uberDict.Add("W", _upImage);
        uberDict.Add("S", _downImage);
        uberDict.Add("A", _leftImage);
        uberDict.Add("D", _rightImage);
    }

    public void Connect(string sequence, Action<bool, MinigameBox> callback, Camera camera1, GuestView guestView)
    {
        base.Connect(camera1, guestView);

        seq = sequence;
        _callBack = callback;

        ShowSequence();
    }

    public override void Tick(float deltaTime)
    {
        base.Tick(deltaTime);

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
                            _callBack?.Invoke(integrity, this);
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
            IEnumerator coroutine = Blink(i, pressedImg, 0.4f * (i + 1));
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