using System.Collections;

using System.Collections.Generic;

using UnityEngine;

using TMPro;
using UnityEngine.UI;


public class KeyboardButton : MonoBehaviour
{

    [SerializeField] private Canvas canvas;
    [SerializeField] private Sprite defaultImage;
    [SerializeField] private Sprite hoverImage;
    
    Keyboard keyboard;

    [SerializeField] TextMeshProUGUI buttonText;
    private Image image;

    // Gets correct components on start
    void Start()
    {
        image = gameObject.GetComponent<Image>();
        keyboard = canvas.GetComponent<Keyboard>();

        if (buttonText.text.Length == 1){

            NameToButtonText();

        }

    }

    // set hover sprite
    public void Hover() => image.sprite = hoverImage;

    // set normal sprite
    public void Unhover() => image.sprite = defaultImage;

    // use name on botton text
    public void NameToButtonText() => buttonText.text = gameObject.name;

    // sends character to keyboard
    public void SendToKeyboard() => keyboard.InsertChar(buttonText.text);
}
