using UnityEngine;
using UnityEngine.InputSystem;

public class UserAction : System.IDisposable
{
    UserInput userInput;
    BoardScanner boardScanner;
    CellSelection cellSelection;

    Camera mainCamera;

    System.Action<InputAction.CallbackContext> openCellHandler;
    System.Action<InputAction.CallbackContext> setFlagHandler;

    public UserAction(UserInput userInput, BoardScanner boardScanner, CellSelection cellSelection, Camera mainCamera)
    {
        this.userInput = userInput;
        this.boardScanner = boardScanner;
        this.cellSelection = cellSelection;
        this.mainCamera = mainCamera;

        openCellHandler = (ctx) => OpenCell(userInput.screenPosition);
        setFlagHandler = (ctx) => SetFlag(userInput.screenPosition);

        userInput.openCell.performed += openCellHandler;
        userInput.setFlag.performed += setFlagHandler;
    }

    void OpenCell(Vector2 screenPosition)
    {
        Vector2 worldPosition = mainCamera.ScreenToWorldPoint(screenPosition);
        CellObject cell = cellSelection.FindSelection(worldPosition);

        if (cell == null)
            return;

        if (cell.cellStateChanger.currentState.setFlag)
            return;

        cell.cellStateChanger.currentState.Tap();
        
        boardScanner.Scan(cell);

        if (!boardScanner.HaveClosedEmptyCell())
        {
            GameEvents.Instance.winGame?.Invoke();
        }
    }

    void SetFlag(Vector2 screenPosition)
    {
        Vector2 worldPosition = mainCamera.ScreenToWorldPoint(screenPosition);
        CellObject cell = cellSelection.FindSelection(worldPosition);

        if (cell == null)
            return;

        cell.cellStateChanger.currentState.SetFlag();
    }

    public void Dispose()
    {
        if (userInput != null)
        {
            userInput.openCell.performed -= openCellHandler;
            userInput.setFlag.performed -= setFlagHandler;
        }
    }
}
