using System.Collections;

using System.Collections.Generic;

using UnityEngine;

using TMPro;


public class Keyboard : MonoBehaviour{

   [SerializeField] TMP_InputField inputField;


   // public void SetInputField(TMP_InputField field){
   //
   //     inputField = field;
   //
   //     Debug.Log(inputField.text);
   //
   // }


   public void InsertChar(string c){

       inputField.text += c;

   }


   public void DeleteChar(){

       if (inputField.text.Length > 0){

           inputField.text = inputField.text.Substring(0, inputField.text.Length - 1);

       }

   }

}
