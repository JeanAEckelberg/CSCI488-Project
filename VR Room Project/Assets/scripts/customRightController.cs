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

public class customRightController : MonoBehaviour
{
    [SerializeField] InputActionReference primaryInputActionReference;
    [SerializeField] InputActionReference secondaryInputActionReference;
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
    private Vector2 lastAxis = Vector2.zero;
    private Vector2 axis;

    // Start is called before the first frame update
    void Start()
    {
        wheel.enabled = false;
        primaryInputActionReference.action.performed += OnBackspace;
        secondaryInputActionReference.action.performed += OnEnter;
        specialInputActionReference.action.performed += OnSpecial;
        shift.action.performed += OnShift;
        space.action.performed += OnSpace;
    }

    // Update is called once per frame
    void Update()
    {
        if (!selected)
            return;
        axis = open.action?.ReadValue<Vector2>() ?? Vector2.zero;
        if (axis == Vector2.zero && lastAxis != Vector2.zero)
        {
            OnClose();
            tracking = false;
        }
        else if (axis != Vector2.zero && lastAxis == Vector2.zero)
        {
            tracking = true;
        }
        else if (tracking && ((Mathf.Abs(axis.x) > 0.75 && Mathf.Abs(axis.y) < 0.25) || (Mathf.Abs(axis.y) > 0.75 && Mathf.Abs(axis.x) < 0.25)))
        {
            tracking = false;
            OnOpen();
        }
        else if (axis != Vector2.zero && lastAxis != Vector2.zero)
        {
            var transform1 = wheel.transform;
            var diff = Vector3.SignedAngle(transform.up, transform1.up, transform1.forward);
            if (wheel.TryGetComponent<LeftWheelController>(out var left))
                left.OnHover(diff);
            if (wheel.TryGetComponent<RightWheelController>(out var right))
                right.OnHover(diff);
            wheel.transform.position = transform.position;
        }

        lastAxis = axis;
    }
    
    private void OnSpecial(InputAction.CallbackContext c)
    {
        if (!selected)
            return;

        if (wheel.TryGetComponent<LeftWheelController>(out var left))
            left.OnSelectRow(axis, isShifted, true);
        if (wheel.TryGetComponent<RightWheelController>(out var right))
            right.OnSelectRow(axis, isShifted, true);
    }

    private void OnBackspace(InputAction.CallbackContext c)
    {
        if (!selected)
            return;

        mainCanvas.GetComponent<Keyboard>().DeleteChar();
    }

    private void OnEnter(InputAction.CallbackContext c)
    {
        if (!selected)
            return;

        mainText.onSubmit.Invoke("");
    }

    public void OnShift(InputAction.CallbackContext c)
    {
        if (!selected)
            return;
        isShifted = !isShifted;
    }

    public void OnSpace(InputAction.CallbackContext c)
    {
        if (!selected)
            return;
        mainText.text += " ";
    }

    public void OnSelect()
    {
        Debugger.Instance.LogIt($"Right selected: {selected}");
        selected = !selected;
    }
    
    public void OnDeselect()
    {
        Debugger.Instance.LogIt($"Right deselected: {selected}");
        selected = false;
    }

    public void OnOpen()
    {
        if (!selected)
            return;
        var transform1 = transform;
        wheel.transform.SetPositionAndRotation(transform1.position, transform1.rotation);
        wheel.enabled = true;
        if (wheel.TryGetComponent<LeftWheelController>(out var left))
            left.OnSelectRow(axis, isShifted);
        if (wheel.TryGetComponent<RightWheelController>(out var right))
            right.OnSelectRow(axis, isShifted);
    }

    private void OnClose()
    {
        var transform1 = wheel.transform;
        var diff = Vector3.SignedAngle(transform.up, transform1.up, transform1.forward);
        if (wheel.TryGetComponent<LeftWheelController>(out var left))
            left.OnSelect(diff);
        if (wheel.TryGetComponent<RightWheelController>(out var right))
            right.OnSelect(diff);
        wheel.enabled = false;
    }
}