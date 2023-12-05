using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.UI;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.OpenXR.Input;
using XRController = UnityEngine.InputSystem.XR.XRController;

public class customLeftController : MonoBehaviour
{
    [SerializeField] InputActionReference primaryInputActionReference;
    [SerializeField] InputActionReference specialInputActionReference;
    [SerializeField] InputActionProperty shift;
    [SerializeField] InputActionProperty space;
    [SerializeField] private InputActionProperty open;
    [SerializeField] private Canvas mainCanvas;
    [SerializeField] private TMP_InputField mainText;
    [SerializeField] private Canvas wheel;

    private bool selected = false;
    private bool tracking = false;
    private bool isShifted = false;
    private bool isTrackingSpecial = false;
    private Vector2 axis;

    // Start is called before the first frame update
    // Binds actions to additional functions
    void Start()
    {
        wheel.enabled = false;
        primaryInputActionReference.action.performed += OnTab;
        specialInputActionReference.action.performed += OnSpecial;
        shift.action.performed += OnShift;
        space.action.performed += OnSpace;
    }

    // Update is called once per frame
    // Handles selecting keydial and hovering
    void Update()
    {
        if (!selected)
            return;
        axis = open.action?.ReadValue<Vector2>() ?? Vector2.zero;
        if ((Mathf.Abs(axis.x) > 0.7 && Mathf.Abs(axis.y) < 0.7) || (Mathf.Abs(axis.y) > 0.7 && Mathf.Abs(axis.x) < 0.7) || isTrackingSpecial)
        {
            if (!tracking)
            {
                OnOpen();
                tracking = true;
                return;
            }

            var transform1 = wheel.transform;
            var diff = Vector3.SignedAngle(transform.up, transform1.up, transform1.forward);
            if (wheel.TryGetComponent<LeftWheelController>(out var left))
                left.OnHover(diff);
            if (wheel.TryGetComponent<RightWheelController>(out var right))
                right.OnHover(diff);
            wheel.transform.position = transform.position;
            return;
        }
        if (tracking)
        {
            tracking = false;
            OnClose();
        }
        var transform2 = transform;
        wheel.transform.SetPositionAndRotation(transform2.position, transform2.rotation);
    }
    
    // Handle Tab Action
    private void OnTab(InputAction.CallbackContext c)
    {
        if (!selected)
            return;

        mainText.text += "\t";
    }

    // Handle special key when pressed and released
    private void OnSpecial(InputAction.CallbackContext c)
    {
        if (!selected)
            return;
        if (!c.ReadValueAsButton())
        {
            OnClose();
            isTrackingSpecial = false;
            return;
        }

        if (wheel.TryGetComponent<LeftWheelController>(out var left))
            left.OnSelectRow(axis, isShifted, true);
        if (wheel.TryGetComponent<RightWheelController>(out var right))
            right.OnSelectRow(axis, isShifted, true);
        OnOpen();
        isTrackingSpecial = true;
    }

    // Handle shift togglw
    public void OnShift(InputAction.CallbackContext c)
    {
        if (!selected)
            return;
        isShifted = !isShifted;
    }

    // Handle space entry
    public void OnSpace(InputAction.CallbackContext c)
    {
        if (!selected)
            return;
        mainText.text += " ";
    }

    // enable keydial functions
    public void OnSelect()
    {
        Debugger.Instance.LogIt($"Left Selected: {selected}");
        selected = !selected;
        wheel.enabled = selected;
        var temp = mainText.colors;
        temp.normalColor = selected ? Color.white : Color.grey;
        temp.highlightedColor = selected ? Color.white : Color.grey;
        temp.selectedColor = selected ? Color.white : Color.grey;
        mainText.colors = temp;
    }

    // Handle setting the position of the keydial
    public void OnOpen()
    {
        if (!selected)
            return;
        var transform1 = transform;
        wheel.transform.SetPositionAndRotation(transform1.position, transform1.rotation);
        if (wheel.TryGetComponent<LeftWheelController>(out var left))
            left.OnSelectRow(axis, isShifted);
        if (wheel.TryGetComponent<RightWheelController>(out var right))
            right.OnSelectRow(axis, isShifted);
    }

    // Handle sending selection and closing keydial
    private void OnClose()
    {
        var transform1 = wheel.transform;
        var diff = Vector3.SignedAngle(transform.up, transform1.up, transform1.forward);
        if (wheel.TryGetComponent<LeftWheelController>(out var left))
            left.OnSelect(diff);
        if (wheel.TryGetComponent<RightWheelController>(out var right))
            right.OnSelect(diff);
    }
}