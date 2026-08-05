using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class UserInput : System.IDisposable
{
    public Vector2 screenPosition { get; private set; }
    public Vector2 firstTouchPosition { get; private set; }
    public Vector2 lastTouchPosition { get; private set; }

    public InputAction openCell { get; private set; }
    public InputAction setFlag { get; private set; }
    public InputAction zoom { get; private set; }
    public InputAction move { get; private set; }
    public InputAction mobileBoardInteraction { get; private set; }
    public InputAction screenPositionAction { get; private set; }

    public UserInput(InputActionAsset inputActions)
    {
        if (inputActions == null)
        {
            Debug.LogError("InputActionAsset is not assigned in UserInput.");
            return;
        }

        openCell = inputActions.FindAction("OpenCellPC");
        screenPositionAction = inputActions.FindAction("ScreenPosition");
        setFlag = inputActions.FindAction("SetFlagPC");
        zoom = inputActions.FindAction("Zoom");
        move = inputActions.FindAction("Move");
        mobileBoardInteraction = inputActions.FindAction("MobileBoardInteraction");

        if (openCell != null)
        {
            openCell.started += SaveFirstActionPosition;
            openCell.canceled += SaveEndActionPosition;
            openCell.Enable();
        }

        if (setFlag != null)
        {
            setFlag.started += SaveFirstActionPosition;
            setFlag.canceled += SaveEndActionPosition;
            setFlag.Enable();
        }

        if (screenPositionAction != null)
        {
            screenPositionAction.performed += SaveTouchPosition;
            screenPositionAction.Enable();
        }

        zoom?.Enable();
        move?.Enable();
        mobileBoardInteraction?.Enable();
    }

    void SaveFirstActionPosition(InputAction.CallbackContext ctx)
    {
        firstTouchPosition = screenPosition;
    }

    void SaveEndActionPosition(InputAction.CallbackContext ctx)
    {
        lastTouchPosition = screenPosition;
    }

    void SaveTouchPosition(InputAction.CallbackContext ctx)
    {
        screenPosition = ctx.ReadValue<Vector2>();
    }

    public bool IsPointerOverUI(Vector2 screenPosition)
    {
        if (EventSystem.current == null)
            return false;

        PointerEventData eventData = new PointerEventData(EventSystem.current)
        {
            position = screenPosition
        };

        var results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);

        foreach (var result in results)
        {
            if (result.gameObject.layer == LayerMask.NameToLayer("UI"))
            {
                return true;
            }
        }

        return false;
    }

    public void Dispose()
    {
        if (openCell != null)
        {
            openCell.started -= SaveFirstActionPosition;
            openCell.canceled -= SaveEndActionPosition;
            openCell.Disable();
        }

        if (setFlag != null)
        {
            setFlag.started -= SaveFirstActionPosition;
            setFlag.canceled -= SaveEndActionPosition;
            setFlag.Disable();
        }

        if (screenPositionAction != null)
        {
            screenPositionAction.performed -= SaveTouchPosition;
            screenPositionAction.Disable();
        }

        zoom?.Disable();
        move?.Disable();

        if (mobileBoardInteraction != null)
        {
            mobileBoardInteraction.Disable();
        }
    }
}
