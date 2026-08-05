using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BoardGenerator
{
    GameRules gameRules;

    public BoardGenerator(GameRules gameRules)
    {
        this.gameRules = gameRules;
    }

    public BoardController GenerateBoard(BoardData boardData)
    {
        var bombFactory = new CellFactory<BombCell>();
        var emptyFactory = new CellFactory<EmptyClosedCell>();

        CellObject[][] cells = new CellObject[boardData.sizeY][];

        for (int y = 0; y < boardData.sizeY; y++)
        {
            cells[y] = new CellObject[boardData.sizeX];
        }

        GameObject container = new GameObject("Board");

        float offsetY = boardData.sizeY / 2f - 0.5f;
        float offsetX = boardData.sizeX / 2f - 0.5f;

        List<Vector2Int> availablePoints = new List<Vector2Int>();

        for (int y = 0; y < boardData.sizeY; y++)
        {
            for (int x = 0; x < boardData.sizeX; x++)
            {
                availablePoints.Add(new Vector2Int(x, y));
            }
        }

        int minesToPlace = Mathf.Clamp(gameRules.badCellCount, 0, boardData.sizeX * boardData.sizeY - 1);
        if (minesToPlace != gameRules.badCellCount)
        {
            Debug.LogWarning($"badCellCount {gameRules.badCellCount} was clamped to {minesToPlace} based on board dimensions.");
        }

        for (int i = 0; i < minesToPlace; i++)
        {
            int index = Random.Range(0, availablePoints.Count);
            Vector2Int point = availablePoints[index];
            availablePoints.RemoveAt(index);

            Vector2 createPosition = new Vector2(point.x - offsetX, point.y - offsetY);
            CellObject cell = bombFactory.Create(createPosition, container, new Vector2Int(point.y, point.x));

            cells[point.y][point.x] = cell;
        }

        foreach (var point in availablePoints)
        {
            Vector2 createPosition = new Vector2(point.x - offsetX, point.y - offsetY);
            CellObject cell = emptyFactory.Create(createPosition, container, new Vector2Int(point.y, point.x));

            cells[point.y][point.x] = cell;
        }

        BoardController boardController = new BoardController(cells, container);

        return boardController;
    }

}
