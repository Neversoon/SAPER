using UnityEngine;
using UnityEngine.InputSystem;

public class UserAction : System.IDisposable
{
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
        this.mainCamera = cameraController.getMainCamera;
        this.cameraController = cameraController;

        openCellHandler = (ctx) => OpenCell(userInput.screenPosition);
        setFlagHandler = (ctx) => SetFlag(userInput.screenPosition);
        zoom = (ctx) => Zoom(ctx.ReadValue<Vector2>().y * 100f);

        userInput.openCell.performed += openCellHandler;
        userInput.setFlag.performed += setFlagHandler;
        userInput.zoom.performed += zoom;
        
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
			if(touchCount < 2)
				return;
			var magnitude = (touch0pos.ReadValue<Vector2>() - touch1pos.ReadValue<Vector2>()).magnitude;
			if(prevMagnitude == 0)
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
            userInput.zoom.performed -= zoom;
        }
    }
}
