using UnityEngine;
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
    public InputAction screenPositionAction;

    public UserInput(InputActionAsset inputActions)
    {
        openCell = inputActions.FindAction("OpenCell");
        screenPositionAction = inputActions.FindAction("ScreenPosition");
        setFlag = inputActions.FindAction("SetFlag");
        zoom = inputActions.FindAction("Zoom");
        move = inputActions.FindAction("Move");

        openCell.started += SaveFirstActionPosition;
        openCell.canceled += SaveEndActionPosition;
        setFlag.started += SaveFirstActionPosition;
        setFlag.canceled += SaveEndActionPosition;
        screenPositionAction.performed += SaveTouchPosition;

        screenPositionAction.Enable();
        openCell.Enable();
        setFlag.Enable();
        zoom.Enable();
        move.Enable();
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

    public void Dispose()
    {
        openCell.started -= SaveFirstActionPosition;
        openCell.canceled -= SaveEndActionPosition;
        setFlag.started -= SaveFirstActionPosition;
        setFlag.canceled -= SaveEndActionPosition;
        screenPositionAction.performed -= SaveTouchPosition;
    }
}
