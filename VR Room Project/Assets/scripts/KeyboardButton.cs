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


    void Start()
    {
        image = this.gameObject.GetComponent<Image>();
        keyboard = canvas.GetComponent<Keyboard>();

        if (buttonText.text.Length == 1){

            NameToButtonText();

        }

    }

    public void Hover() => image.sprite = hoverImage;

    public void Unhover() => image.sprite = defaultImage;

    public void NameToButtonText() => buttonText.text = gameObject.name;

    public void SendToKeyboard() => keyboard.InsertChar(buttonText.text);
}
