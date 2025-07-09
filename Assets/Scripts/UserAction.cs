using UnityEngine;
using UnityEngine.InputSystem;

public class UserAction : System.IDisposable
{
    bool userOpenFirstCell = false;
    [SerializeField] float deadZone = 0.6f;
    UserInput userInput;
    BoardScanner boardScanner;
    CellSelection cellSelection;
    CameraController cameraController;

    Camera mainCamera;

    System.Action<InputAction.CallbackContext> openCellHandler;
    System.Action<InputAction.CallbackContext> setFlagHandler;

    System.Action<InputAction.CallbackContext> zoom;
    private int touchCount = 0;
    private float prevMagnitude = 0;
    public UserAction(UserInput userInput, BoardScanner boardScanner, CellSelection cellSelection, CameraController cameraController)
    {
        this.userInput = userInput;
        this.boardScanner = boardScanner;
        this.cellSelection = cellSelection;
        mainCamera = cameraController.getMainCamera;
        this.cameraController = cameraController;
        cameraController.userInput = userInput;

        openCellHandler = (ctx) => OpenCell(userInput);
        setFlagHandler = (ctx) => SetFlag(userInput);
        zoom = (ctx) => Zoom(ctx.ReadValue<Vector2>().y * 100f);

        userInput.openCell.canceled += openCellHandler;
        userInput.setFlag.canceled += setFlagHandler;
        userInput.zoom.performed += zoom;

        userInput.move.started += cameraController.StartDrag;
        userInput.move.canceled += cameraController.StopDrag;

        /// Very big shit code
        var touch0contact = new InputAction
        (
            type: InputActionType.Button,
            binding: "<Touchscreen>/touch0/press"
        );
        touch0contact.Enable();
        var touch1contact = new InputAction
        (
            type: InputActionType.Button,
            binding: "<Touchscreen>/touch1/press"
        );
        touch1contact.Enable();

        touch0contact.performed += _ => touchCount++;
        touch1contact.performed += _ => touchCount++;
        touch0contact.canceled += _ =>
        {
            touchCount--;
            prevMagnitude = 0;
        };
        touch1contact.canceled += _ =>
        {
            touchCount--;
            prevMagnitude = 0;
        };

        var touch0pos = new InputAction
        (
            type: InputActionType.Value,
            binding: "<Touchscreen>/touch0/position"
        );
        touch0pos.Enable();
        var touch1pos = new InputAction
        (
            type: InputActionType.Value,
            binding: "<Touchscreen>/touch1/position"
        );
        touch1pos.Enable();
        touch1pos.performed += _ =>
        {
            if (touchCount < 2)
                return;
            var magnitude = (touch0pos.ReadValue<Vector2>() - touch1pos.ReadValue<Vector2>()).magnitude;
            if (prevMagnitude == 0)
                prevMagnitude = magnitude;
            var difference = magnitude - prevMagnitude;
            prevMagnitude = magnitude;
            Zoom(-difference);
        };
    }

    public void Zoom(float value)
    {
        cameraController.Zoom(value);
    }

    void OpenCell(UserInput userInput)
    {
        if (userInput.IsPointerOverUI(userInput.screenPosition))
        {
            return;
        }

        Vector2 worldPosition = mainCamera.ScreenToWorldPoint(userInput.screenPosition);
        Vector2 firstWorldPosition = mainCamera.ScreenToWorldPoint(userInput.firstTouchPosition);

        if (Vector2.Distance(worldPosition, firstWorldPosition) > deadZone)
        {
            return;
        }

        CellObject cell = cellSelection.FindSelection(worldPosition);

        if (cell == null)
            return;

        if (cell.cellStateChanger.currentState.setFlag)
            return;

        cell.cellStateChanger.currentState.Tap();

        boardScanner.Scan(cell);

        if (!userOpenFirstCell)
        {
            userOpenFirstCell = true;
            EventBus.Publish(new GameEvents.OpenFirstCell());
        }

        if (!boardScanner.HaveClosedEmptyCell())
        {
            EventBus.Publish(new GameEvents.Win());
        }
    }

    void SetFlag(UserInput userInput)
    {
        if (userInput.IsPointerOverUI(userInput.screenPosition))
        {
            return;
        }

        Vector2 worldPosition = mainCamera.ScreenToWorldPoint(userInput.screenPosition);
        Vector2 firstWorldPosition = mainCamera.ScreenToWorldPoint(userInput.firstTouchPosition);

        if (Vector2.Distance(worldPosition, firstWorldPosition) > deadZone)
        {
            return;
        }

        CellObject cell = cellSelection.FindSelection(worldPosition);

        if (cell == null)
            return;

        if (!userOpenFirstCell)
        {
            userOpenFirstCell = true;
            EventBus.Publish(new GameEvents.OpenFirstCell());
        }

        cell.cellStateChanger.currentState.SetFlag();
        SFXAudioPlayer.Instance.PlaySFX(SFXAudioPlayer.Instance.audioClips.flagPlaced);

        EventBus.Publish(new GameEvents.FlagCountChanged(boardScanner.gameData.gameRules.badCellCount - boardScanner.FlagCount()));
    }

    public void Dispose()
    {
        if (userInput != null)
        {
            userInput.openCell.canceled -= openCellHandler;
            userInput.setFlag.canceled -= setFlagHandler;
            userInput.zoom.performed -= zoom;
            userInput.move.started -= cameraController.StartDrag;
            userInput.move.canceled -= cameraController.StopDrag;
        }
    }
}
