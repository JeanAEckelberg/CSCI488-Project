using System.Collections;

using System.Collections.Generic;

using UnityEngine;

using TMPro;


public class KeyboardButton : MonoBehaviour
{

    [SerializeField] private Canvas canvas;
    
    Keyboard keyboard;

    [SerializeField] TextMeshProUGUI buttonText;


    void Start()
    {
        keyboard = canvas.GetComponent<Keyboard>();

        if (buttonText.text.Length == 1){

            NameToButtonText();

        }

    }

   

    public void NameToButtonText(){

        buttonText.text = gameObject.name;

    }

}
