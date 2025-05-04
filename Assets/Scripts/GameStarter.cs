using UnityEngine;
using UnityEngine.InputSystem;

public class GameStarter
{
    InputActionAsset inputActions;
    Camera mainCamera;
    UserAction userAction;
    BoardController boardController;

    public GameStarter(InputActionAsset inputActions, Camera mainCamera)
    {
        this.inputActions = inputActions;
        this.mainCamera = mainCamera;
    }

    public void StartGame(GameModeData gameData)
    {
        BoardGenerator boardGenerator = new BoardGenerator(gameData.gameRules);

        boardController = boardGenerator.GenerateBoard(gameData.boardData);

        UserInput userInput = new UserInput(inputActions);
        BoardScanner boardScanner = new BoardScanner(boardController.cells, gameData);
        CellSelection cellSelection = new CellSelection(boardController.cells);

        userAction = new UserAction(userInput, boardScanner, cellSelection, mainCamera);

        GameEvents.Instance.startGame?.Invoke();
    }
    public void RestartGame(GameModeData gameData)
    {
        GameEvents.Instance.restartGame?.Invoke();
        
        if (userAction != null)
        {
            userAction.Dispose();
        }

        boardController.Clear();

        StartGame(gameData);
    }

}
