using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class Keyboard : MonoBehaviour
{
    [SerializeField] TMP_InputField inputField;

    // Insert character to main textfield
    public void InsertChar(string c)
    {
        inputField.text += c;
    }

    // Deletes character in main textfield
    public void DeleteChar()
    {
        if (inputField.text.Length > 0)
        {
            inputField.text = inputField.text.Substring(0, inputField.text.Length - 1);
        }
    }
}