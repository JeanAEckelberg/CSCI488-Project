using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class Debugger : MonoBehaviour
{
    public static Debugger Instance;

    private TMP_Text txt;
    [SerializeField] private bool silent;

    // Start is called before the first frame update
    void Start()
    {
        txt = GetComponentInChildren<TMP_Text>();
        Instance = this;
    }

    // Update is called once per frame
    // Write logs to screen if not silent (to a max of 10)
    public void LogIt(string msg)
    {
        if (silent)
            return;
        txt.text += msg + "\n";
        txt.text = txt.text.Split("\n")
            .Reverse()
            .Take(10)
            .Reverse()
            .Aggregate("", (s, s1) => s + "\n" + s1);
    }
}