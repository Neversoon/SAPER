using UnityEngine;
using UnityEngine.InputSystem;

public class GameStarter
{
    InputActionAsset inputActions;
    CameraController cameraController;
    UserAction userAction;
    BoardController boardController;

    public GameStarter(InputActionAsset inputActions, CameraController cameraController)
    {
        this.inputActions = inputActions;
        this.cameraController = cameraController;
    }

    public void StartGame(GameModeData gameData)
    {
        EventBus.Publish(new GameEvents.FlagCountChanged(gameData.gameRules.badCellCount));

        BoardGenerator boardGenerator = new BoardGenerator(gameData.gameRules);

        boardController = boardGenerator.GenerateBoard(gameData.boardData);

        UserInput userInput = new UserInput(inputActions);
        BoardScanner boardScanner = new BoardScanner(boardController.cells, gameData);
        CellSelection cellSelection = new CellSelection(boardController.cells);

        userAction = new UserAction(userInput, boardScanner, cellSelection, cameraController);

        EventBus.Publish(new GameEvents.Started());
    }
    public void RestartGame(GameModeData gameData)
    {
        EventBus.Publish(new GameEvents.Restart());

        if (userAction != null)
        {
            userAction.Dispose();
        }

        boardController.Clear();

        StartGame(gameData);
    }

    public void PauseGame()
    {
        EventBus.Publish(new GameEvents.Pause());
    }
    public void ResumeGame()
    {
        EventBus.Publish(new GameEvents.Resume());
    }
}
