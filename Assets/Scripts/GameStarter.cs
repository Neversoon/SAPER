using UnityEngine;
using UnityEngine.InputSystem;

public class GameStarter : MonoBehaviour
{
    [SerializeField] InputActionAsset inputActions;
    [SerializeField] Camera mainCamera;
    
    void Awake()
    {
        BoardGenerator boardGenerator = new BoardGenerator();

        CellObject[][] cells = boardGenerator.GenerateBoard(new BoardData());

        new UserInput(inputActions, cells, mainCamera);
    }
}
