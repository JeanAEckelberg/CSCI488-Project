using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class CustomCharacterController : MonoBehaviour
{
    private ActionBasedContinuousMoveProvider moveProvider;
    
    // Start is called before the first frame update
    void Start() => moveProvider = GetComponent<ActionBasedContinuousMoveProvider>();

    // Disables movement on selection
    public void OnSelect() => moveProvider.enabled = !moveProvider.enabled;
}
