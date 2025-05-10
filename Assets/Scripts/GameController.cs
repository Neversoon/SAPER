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
    }
    public void Restart()
    {
        gameStarter.RestartGame(gameModesController.GetCurrentGameData());
    }
}
