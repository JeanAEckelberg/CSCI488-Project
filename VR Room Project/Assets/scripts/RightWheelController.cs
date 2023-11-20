using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RightWheelController : MonoBehaviour
{
    private List<KeyboardButton> selectedRow;
    [SerializeField] private List<KeyboardButton> topRow;
    [SerializeField] private List<KeyboardButton> outerRow;
    [SerializeField] private List<KeyboardButton> innerRow;
    [SerializeField] private List<KeyboardButton> bottomRow;
    

    public void OnSelectRow(Vector2 decider)
    {
        selectedRow?.ForEach(x => x.enabled = false);
        if (decider.y > 0.95 && decider.x <= 0.05)
        {
            selectedRow = topRow;
        }
        else if (decider.y < -0.95 && Mathf.Abs(decider.x) <= 0.05)
        {
            selectedRow = bottomRow;
        }
        else if (decider.x > 0.95 && Mathf.Abs(decider.y) <= 0.05)
        {
            selectedRow = outerRow;
        }
        else if (decider.x < -0.95 && Mathf.Abs(decider.y) <= 0.05)
        {
            selectedRow = innerRow;
        }
        else
        {
            selectedRow = null;
        }
        selectedRow?.ForEach(x => x.enabled = true);
    }
    
    public void OnSelect(float diff)
    {
        
        switch (diff)
        {
            case > 60:
            {
                break;
            }
            case > 30:
            {
                selectedRow[0].SendToKeyboard();
                break;
            }
            case < -150:
            {
                break;
            }
            case < -120:
            {
                selectedRow[4].SendToKeyboard();
                break;
            }
            case < -90:
            {
                selectedRow[3].SendToKeyboard();
                break;
            }
            case < -60:
            {
                selectedRow[2].SendToKeyboard();
                break;
            }
            case < -30:
            {
                selectedRow[1].SendToKeyboard();
                break;
            }
        }
    }
}
