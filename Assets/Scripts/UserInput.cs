using UnityEngine;
using UnityEngine.InputSystem;

public class UserInput
{
    Vector2 screenPosition;
    public UserInput(InputActionAsset inputActions, CellObject[][] cells, Camera mainCamera)
    {
        CellSelection cellSelection = new CellSelection(cells);

        InputAction openCell = inputActions.FindAction("OpenCell");

        InputAction screenPositionAction = inputActions.FindAction("ScreenPosition");

        InputAction setFlag = inputActions.FindAction("SetFlag");


        screenPositionAction.performed += (cfx) =>
        {
            screenPosition = cfx.ReadValue<Vector2>();
        };

        openCell.performed += (cfx) =>
        {
            Vector2 worldPosition = mainCamera.ScreenToWorldPoint(screenPosition);

            CellObject cell = cellSelection.FindSelection(worldPosition);

            if (cell != null)
            {
                cell.cellStateChanger.currentState.Tap();

                BoardScanner boardScanner = new BoardScanner();

                boardScanner.Scan(cell, ref cells);
            }
        };


        setFlag.performed += (cfx) =>
        {
            Vector2 worldPosition = mainCamera.ScreenToWorldPoint(screenPosition);

            CellObject cell = cellSelection.FindSelection(worldPosition);

            if (cell != null)
            {
                cell.cellStateChanger.currentState.SetFlag();
            }
        };

        screenPositionAction.Enable();
        openCell.Enable();
        setFlag.Enable();
    }
}
