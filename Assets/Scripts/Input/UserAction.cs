using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class UserAction : System.IDisposable
{
    bool userOpenFirstCell = false;
    bool userFirstInteraction = false;
    [SerializeField] float deadZone = 1.5f;
    UserInput userInput;
    BoardScanner boardScanner;
    CellSelection cellSelection;
    CameraController cameraController;

    Camera mainCamera;
 
    System.Action<InputAction.CallbackContext> openCellHandler;
    System.Action<InputAction.CallbackContext> setFlagHandler;
    System.Action<InputAction.CallbackContext> mobilePressCallback;
    InputAction touch0contact;
    InputAction touch1contact;
    InputAction touch0pos;
    InputAction touch1pos;
    System.Action<InputAction.CallbackContext> touch0ContactPerformed;
    System.Action<InputAction.CallbackContext> touch1ContactPerformed;
    System.Action<InputAction.CallbackContext> touch0ContactCanceled;
    System.Action<InputAction.CallbackContext> touch1ContactCanceled;
    System.Action<InputAction.CallbackContext> touch1PositionPerformed;
 
    System.Action<InputAction.CallbackContext> zoom;
    int touchCount = 0;
    float prevMagnitude = 0;

    float pressStartTime = 0f;
    bool isPressed = false;
    const float holdThreshold = 0.5f;

    float blockInteractionTime = 0f;
    const float blockDurationAfterMultitouch = 0.2f;
    bool blockInteraction = false;

    public UserAction(UserInput userInput, BoardScanner boardScanner, CellSelection cellSelection, CameraController cameraController)
    {
        if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
        {
            deadZone = 20f;
        }
        else if (Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.WindowsPlayer)
        {
            deadZone = 1.5f;
        }

        this.userInput = userInput;
        this.boardScanner = boardScanner;
        this.cellSelection = cellSelection;
        mainCamera = cameraController.getMainCamera;
        this.cameraController = cameraController;
        cameraController.userInput = userInput;

        openCellHandler = (ctx) => OpenCell(userInput);
        setFlagHandler = (ctx) => SetFlag(userInput);
        zoom = (ctx) => Zoom(ctx.ReadValue<Vector2>().y * 10f);

        if (userInput.openCell != null)
            userInput.openCell.canceled += openCellHandler;
        if (userInput.setFlag != null)
            userInput.setFlag.canceled += setFlagHandler;
        if (userInput.zoom != null)
            userInput.zoom.performed += zoom;

        if (userInput.move != null)
        {
            userInput.move.started += cameraController.StartDrag;
            userInput.move.canceled += cameraController.StopDrag;
        }

        mobilePressCallback = (ctx) =>
        {
            if (touchCount > 1)
            {
                return;
            }

            if (Time.time < blockInteractionTime || blockInteraction)
                return;

            if (ctx.started)
            {
                pressStartTime = Time.time;
                isPressed = true;
            }
            else if (ctx.canceled)
            {
                if (!isPressed)
                    return;

                isPressed = false;

                float heldTime = Time.time - pressStartTime;

                if (userInput.IsPointerOverUI(userInput.screenPosition))
                    return;

                if (heldTime >= holdThreshold)
                {
                    Debug.Log("Long press detected");
                    SetFlag(userInput);
                }
                else
                {
                    Debug.Log("Short press detected");
                    OpenCell(userInput);
                }
            }
        };

        if (userInput.mobileBoardInteraction != null)
        {
            userInput.mobileBoardInteraction.started += mobilePressCallback;
            userInput.mobileBoardInteraction.canceled += mobilePressCallback;
        }

        /// Very big shit code
        touch0contact = new InputAction
        (
            type: InputActionType.Button,
            binding: "<Touchscreen>/touch0/press"
        );
        touch0contact.Enable();
        touch1contact = new InputAction
        (
            type: InputActionType.Button,
            binding: "<Touchscreen>/touch1/press"
        );
        touch1contact.Enable();

        touch0ContactPerformed = _ => touchCount++;
        touch1ContactPerformed = _ =>
        {
            blockInteraction = true;
            touchCount++;
        };

        touch0ContactCanceled = _ =>
        {
            if (blockInteraction)
            {
                blockInteraction = false;
                blockInteractionTime = Time.time + blockDurationAfterMultitouch;
            }
            touchCount--;
            prevMagnitude = 0;
        };
        touch1ContactCanceled = _ =>
        {
            blockInteractionTime = Time.time + blockDurationAfterMultitouch;
            touchCount--;
            prevMagnitude = 0;
        };

        touch0contact.performed += touch0ContactPerformed;
        touch1contact.performed += touch1ContactPerformed;
        touch0contact.canceled += touch0ContactCanceled;
        touch1contact.canceled += touch1ContactCanceled;

        touch0pos = new InputAction
        (
            type: InputActionType.Value,
            binding: "<Touchscreen>/touch0/position"
        );
        touch0pos.Enable();
        touch1pos = new InputAction
        (
            type: InputActionType.Value,
            binding: "<Touchscreen>/touch1/position"
        );
        touch1pos.Enable();

        touch1PositionPerformed = _ =>
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

        touch1pos.performed += touch1PositionPerformed;
    }

    public void Zoom(float value)
    {
        cameraController.Zoom(value);
    }

    void OpenCell(UserInput userInput)
    {
        if (userInput.IsPointerOverUI(userInput.screenPosition) || cameraController.lastDistanceMove > 0.1f)
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

        if (!userOpenFirstCell)
        {
            userOpenFirstCell = true;
            if (cell.cellStateChanger.currentState.cellData.id == 2)
            {
                cell.cellStateChanger.ChangeState<EmptyClosedCell>();
                cell.cellStateChanger.currentState.cellView.SetEmptyCell();
            }
        }

        if (!userFirstInteraction)
        {
            userFirstInteraction = true;
            EventBus.Publish(new GameEvents.FirstInteraction());
        }

        cell.cellStateChanger.currentState.Tap();

        boardScanner.Scan(cell);

        if (!boardScanner.HaveClosedEmptyCell())
        {
            EventBus.Publish(new GameEvents.Win());
        }
    }

    void SetFlag(UserInput userInput)
    {
        if (userInput.IsPointerOverUI(userInput.screenPosition) || cameraController.lastDistanceMove > 0.1f)
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

        if (!userFirstInteraction)
        {
            userFirstInteraction = true;
            EventBus.Publish(new GameEvents.FirstInteraction());
        }

        cell.cellStateChanger.currentState.SetFlag();
        SFXAudioPlayer.Instance.PlaySFX(SFXAudioPlayer.Instance.audioClips.flagPlaced);

        EventBus.Publish(new GameEvents.FlagCountChanged(boardScanner.gameData.gameRules.badCellCount - boardScanner.FlagCount()));
    }

    public void Dispose()
    {
        if (userInput == null)
            return;

        if (userInput.openCell != null)
            userInput.openCell.canceled -= openCellHandler;

        if (userInput.setFlag != null)
            userInput.setFlag.canceled -= setFlagHandler;

        if (userInput.zoom != null)
            userInput.zoom.performed -= zoom;

        if (userInput.move != null)
        {
            userInput.move.started -= cameraController.StartDrag;
            userInput.move.canceled -= cameraController.StopDrag;
        }

        if (userInput.mobileBoardInteraction != null)
        {
            userInput.mobileBoardInteraction.started -= mobilePressCallback;
            userInput.mobileBoardInteraction.canceled -= mobilePressCallback;
        }

        if (touch0contact != null)
        {
            touch0contact.performed -= touch0ContactPerformed;
            touch0contact.canceled -= touch0ContactCanceled;
            touch0contact.Disable();
            touch0contact.Dispose();
        }

        if (touch1contact != null)
        {
            touch1contact.performed -= touch1ContactPerformed;
            touch1contact.canceled -= touch1ContactCanceled;
            touch1contact.Disable();
            touch1contact.Dispose();
        }

        if (touch0pos != null)
        {
            touch0pos.Disable();
            touch0pos.Dispose();
        }

        if (touch1pos != null)
        {
            touch1pos.performed -= touch1PositionPerformed;
            touch1pos.Disable();
            touch1pos.Dispose();
        }
    }
}
