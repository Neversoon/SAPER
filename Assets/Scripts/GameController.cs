using UnityEngine;
using UnityEngine.InputSystem;

public class GameController : MonoBehaviour
{
    [SerializeField] InputActionAsset inputActions;
    Camera cameraMain;
    GameModesController gameModesController = new GameModesController();
    GameStarter gameStarter;

    void Awake()
    {
        cameraMain = Camera.main;
        gameStarter = new GameStarter(inputActions, cameraMain);
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
