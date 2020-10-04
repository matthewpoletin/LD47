using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClueManager : MonoBehaviour
{
    public List<string> clueList;

    public void Connect()
    {
        clueList = new List<string>();
    }

    public void Utilize()
    {
        clueList.Clear();
    }
}
