using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class Debugger : MonoBehaviour
{
    public static Debugger Instance;

    // Start is called before the first frame update
    private TMP_Text txt;
    private int counter = 0;

    void Start()
    {
        txt = GetComponentInChildren<TMP_Text>();
        Instance = this;
    }

    // Update is called once per frame
    public void LogIt(string msg)
    {
        counter++;
        txt.text += msg + "\n";
        if (counter <= 10)
            return;
        txt.text = txt.text.Split("\n")
            .Reverse()
            .Take(9)
            .Reverse()
            .Aggregate("", (s, s1) => s + "\n" + s1);
    }
}