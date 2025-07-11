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
        EventBus.Subscribe<GameEvents.Pause>(PauseGame);
        EventBus.Subscribe<GameEvents.Resume>(ResumeGame);
        EventBus.Subscribe<GameEvents.Lost>(StopGame);
        EventBus.Subscribe<GameEvents.Win>(StopGame);
    }

    void Start()
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
    public void PauseGame(GameEvents.Pause pauseGameEvent)
    {
        inputActions.Disable();
    }
    public void ResumeGame(GameEvents.Resume resumeGameEvent)
    {
        inputActions.Enable();
    }
    public void StopGame(GameEvents.Lost lostEvent)
    {
        inputActions.Disable();
    }
    public void StopGame(GameEvents.Win winEvent)
    {
        inputActions.Disable();
    }
    void OnDestroy()
    {
        EventBus.Unsubscribe<GameEvents.Pause>(PauseGame);
        EventBus.Unsubscribe<GameEvents.Resume>(ResumeGame);
        EventBus.Unsubscribe<GameEvents.Lost>(StopGame);
        EventBus.Unsubscribe<GameEvents.Win>(StopGame);
    }
}
