using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftWheelController : MonoBehaviour
{
    private List<KeyboardButton> selectedRow;
    [SerializeField] private List<KeyboardButton> topRow;
    [SerializeField] private List<KeyboardButton> shiftedTopRow;
    [SerializeField] private List<KeyboardButton> outerRow;
    [SerializeField] private List<KeyboardButton> shiftedOuterRow;
    [SerializeField] private List<KeyboardButton> innerRow;
    [SerializeField] private List<KeyboardButton> shiftedInnerRow;
    [SerializeField] private List<KeyboardButton> bottomRow;
    [SerializeField] private List<KeyboardButton> shiftedBottomRow;


    public void OnSelectRow(Vector2 decider, bool onShift)
    {
        Debugger.Instance.LogIt($"Selecting Row on left with: {decider}; Shifted: {onShift}");
        if (decider.y > 0.75 && decider.x <= 0.25)
        {
            selectedRow = onShift ? shiftedTopRow : topRow;
        }
        else if (decider.y < -0.75 && Mathf.Abs(decider.x) <= 0.25)
        {
            selectedRow = onShift ? shiftedBottomRow : bottomRow;
        }
        else if (decider.x > 0.75 && Mathf.Abs(decider.y) <= 0.25)
        {
            selectedRow = onShift ? shiftedInnerRow : innerRow;
        }
        else if (decider.x < -0.75 && Mathf.Abs(decider.y) <= 0.25)
        {
            selectedRow = onShift ? shiftedOuterRow : outerRow;
        }

        selectedRow?.ForEach(x => x.gameObject.SetActive(true));
    }
    
    public void OnSelect(float diff)
    {
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
