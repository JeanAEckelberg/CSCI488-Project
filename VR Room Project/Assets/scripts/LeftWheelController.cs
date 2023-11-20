using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftWheelController : MonoBehaviour
{
    private List<KeyboardButton> selectedRow;
    [SerializeField] private List<KeyboardButton> topRow;
    [SerializeField] private List<KeyboardButton> outerRow;
    [SerializeField] private List<KeyboardButton> innerRow;
    [SerializeField] private List<KeyboardButton> bottomRow;
    

    public void OnSelectRow(Vector2 decider)
    {
        Debugger.Instance.LogIt($"Selecting Row with: {decider}");
        if (decider.y > 0.75 && decider.x <= 0.25)
        {
            Debugger.Instance.LogIt("Selected Top Row");
            selectedRow = topRow;
        }
        else if (decider.y < -0.75 && Mathf.Abs(decider.x) <= 0.25)
        {
            selectedRow = bottomRow;
        }
        else if (decider.x > 0.75 && Mathf.Abs(decider.y) <= 0.25)
        {
            selectedRow = innerRow;
        }
        else if (decider.x < -0.75 && Mathf.Abs(decider.y) <= 0.25)
        {
            selectedRow = outerRow;
        }
        selectedRow?.ForEach(x => x.gameObject.SetActive(true));
    }
    
    public void OnSelect(float diff)
    {
        Debugger.Instance.LogIt("Here");
        if (selectedRow is null)
        {
            Debugger.Instance.LogIt("True");
        }
        switch (diff)
        {
            case < -60:
            {
                break;
            }
            case < -30:
            {
                selectedRow?[0].SendToKeyboard();
                break;
            }
            case > 150:
            {
                break;
            }
            case > 120:
            {
                selectedRow?[4].SendToKeyboard();
                break;
            }
            case > 90:
            {
                selectedRow?[3].SendToKeyboard();
                break;
            }
            case > 60:
            {
                selectedRow?[2].SendToKeyboard();
                break;
            }
            case > 30:
            {
                selectedRow?[1].SendToKeyboard();
                break;
            }
        }
        selectedRow?.ForEach(x => x.gameObject.SetActive(false));
        selectedRow = null;
    }
}
