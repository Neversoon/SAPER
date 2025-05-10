using UnityEngine;
using UnityEngine.InputSystem;

public class UserInput
{
    public Vector2 screenPosition { get; private set; }
    public InputAction openCell { get; private set; }
    public InputAction setFlag { get; private set; }
    public InputAction zoom { get; private set; }
    InputAction screenPositionAction;

    public UserInput(InputActionAsset inputActions)
    {
        openCell = inputActions.FindAction("OpenCell");
        screenPositionAction = inputActions.FindAction("ScreenPosition");
        setFlag = inputActions.FindAction("SetFlag");
        zoom = inputActions.FindAction("Zoom");

        screenPositionAction.performed += (ctx) =>
        {
            screenPosition = ctx.ReadValue<Vector2>();
        };

        screenPositionAction.Enable();
        openCell.Enable();
        setFlag.Enable();
        zoom.Enable();
    }


}
