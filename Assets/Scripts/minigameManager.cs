using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class minigameManager : MonoBehaviour
{

    public string seq;

    public Sprite idleImg;
    public Sprite pressedImg;

    public Hashtable buttons;

    public GameObject parent;

    private bool shown = false;
    private bool integrity = true;
    private int counter = 0;

    private Image a;
    private Dictionary<string, Image> uberDict = new Dictionary<string, Image>();

    void Awake()
    {
        uberDict.Add("W", GameObject.Find("Up").GetComponent<Image>());
        uberDict.Add("S", GameObject.Find("Down").GetComponent<Image>());
        uberDict.Add("A", GameObject.Find("Left").GetComponent<Image>());
        uberDict.Add("D", GameObject.Find("Right").GetComponent<Image>());

        ShowSequence();
    }

    // Update is called once per frame
    void Update()
    {
        switch(shown)
        {
            case true:
                foreach (KeyCode vKey in System.Enum.GetValues(typeof(KeyCode)))
                {
                    if (Input.GetKey(vKey))
                    {
                        if (vKey.ToString() == ("" + seq[counter]))
                        {
                            integrity = false;
                        }
                        counter += 1;
                        if (counter >= seq.Length)
                        {
                            shown = false;
                            //send result "integrity" to parent and destroy self
                        }
                    }
                }
                break;
 
        }
    }

    void ShowSequence()
    {
        for (int i = 0; i < seq.Length; i++)
        {
            IEnumerator coroutine = Blink(i, pressedImg, 0.5f * (i + 1));
            StartCoroutine(coroutine);
        }
    }

    IEnumerator Blink(int i, Sprite img, float ttl)
    {
        yield return new WaitForSeconds(ttl);
        uberDict["" + seq[i]].sprite = img;
        if (i != seq.Length)
        {
            IEnumerator coroutine = Blink(i, idleImg, 0.5f);
            StartCoroutine(coroutine);
        }
    }
}
