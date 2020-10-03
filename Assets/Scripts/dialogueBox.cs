using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class dialogueBox : MonoBehaviour
{
    public GameObject parent;
    public string txt;

    Vector3 offset = new Vector3(0.8f, 1.8f, 0f);

    void Awake()
    {
        gameObject.GetComponent<UnityEngine.UI.Text>().text = txt.ToString();
    }

    void Update()
    {
        Vector3 pos = Camera.main.WorldToScreenPoint(parent.transform.position + offset);
        transform.position = pos;
    }

    void Fade()
    {

    }
}
