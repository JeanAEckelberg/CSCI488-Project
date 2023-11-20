using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.UI;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.OpenXR.Input;
using XRController = UnityEngine.InputSystem.XR.XRController;

public class customController : MonoBehaviour
{
    private Canvas wheel;
    [SerializeField] InputActionReference primaryInputActionReference;
    [SerializeField] InputActionReference secondaryInputActionReference;
    [SerializeField] private Canvas mainCanvas;
    [SerializeField] private TMP_InputField mainText;
    
    // Start is called before the first frame update
    void Start()
    {
        wheel = GetComponentInChildren<Canvas>();
        wheel.enabled = false;
        primaryInputActionReference.action.performed += OnBackspace;
        secondaryInputActionReference.action.performed += OnEnter;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnBackspace(InputAction.CallbackContext c)
    {
        if (!wheel.enabled)
            return;
        
        mainCanvas.GetComponent<Keyboard>().DeleteChar();
    }

    private void OnEnter(InputAction.CallbackContext c)
    {
        if (!wheel.enabled)
            return;
        
        mainText.onSubmit.Invoke("");
    }

    
    public void OnSelect()
    {
        wheel.enabled = !wheel.enabled;
    }
}
