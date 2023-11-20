using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class CustomCharacterController : MonoBehaviour
{
    private ActionBasedContinuousMoveProvider moveProvider;
    [SerializeField] private TMP_InputField mainTextInput;
    
    // Start is called before the first frame update
    void Start()
    {
        moveProvider = GetComponent<ActionBasedContinuousMoveProvider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnSelect()
    {
        moveProvider.enabled = !moveProvider.enabled;
    }
}
