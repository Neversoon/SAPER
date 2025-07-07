using UnityEngine;
using UnityEngine.InputSystem;

public class GameController : MonoBehaviour
{
    [SerializeField] InputActionAsset inputActions;
    [SerializeField] CameraController cameraController;
    GameModesController gameModesController = new GameModesController();
    GameStarter gameStarter;

    void Awake()
    {
        gameStarter = new GameStarter(inputActions, cameraController);
        Play();
    }
    public void Play()
    {
        gameStarter.StartGame(gameModesController.GetCurrentGameData());
        cameraController.SetBounds(gameModesController.GetCurrentGameData().boardData);
    }
    public void Restart()
    {
        gameStarter.RestartGame(gameModesController.GetCurrentGameData());
        cameraController.SetBounds(gameModesController.GetCurrentGameData().boardData);
    }
    public void ChangeGameMode(GameModeData gameModeData)
    {
        gameModesController.ChangeCurrentData(gameModeData);
        Restart();
    }
    public void SelectEasyMode()
    {
        ChangeGameMode(GameModes.easyMode);
    }
    public void SelectMediumMode()
    {
        ChangeGameMode(GameModes.mediumMode);
    }
    public void SelectHardMode()
    {
        ChangeGameMode(GameModes.hardMode);
    }
}
