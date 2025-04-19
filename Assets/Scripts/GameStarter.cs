using UnityEngine;

public class GameStarter : MonoBehaviour
{
    void Awake()
    {
        BoardGenerator boardGenerator = new BoardGenerator();
        Cell[,] cells = boardGenerator.GenerateBoard(new BoardData());
    }
}
