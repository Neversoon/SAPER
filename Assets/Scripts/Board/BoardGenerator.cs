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
        CellObject[][] cells = new CellObject[boardData.sizeY][];

        for (int y = 0; y < boardData.sizeY; y++)
        {
            cells[y] = new CellObject[boardData.sizeX];
        }

        GameObject container = new GameObject("Board");

        float offsetY = boardData.sizeY / 2 + 0.5f;
        float offsetX = boardData.sizeX / 2 - 0.5f;

        List<Vector2Int> availablePoints = new List<Vector2Int>();

        for (int y = 0; y < boardData.sizeY; y++)
        {
            for (int x = 0; x < boardData.sizeX; x++)
            {
                availablePoints.Add(new Vector2Int(y, x));
            }
        }

        for (int i = 0; i < gameRules.badCellCount; i++)
        {
            int index = Random.Range(0, availablePoints.Count);
            Vector2Int point = availablePoints[index];
            availablePoints.RemoveAt(index);

            var factory = new CellFactory<BombCell>();

            Vector2 createPosition = new Vector2(point.x - offsetX, point.y - offsetY);
            CellObject cell = factory.Create(createPosition, container, new Vector2Int(point.y, point.x));

            cells[point.y][point.x] = cell;
        }

        foreach (var point in availablePoints)
        {
            var factory = new CellFactory<EmptyClosedCell>();

            Vector2 createPosition = new Vector2(point.x - offsetX, point.y - offsetY);
            CellObject cell = factory.Create(createPosition, container, new Vector2Int(point.y, point.x));

            cells[point.y][point.x] = cell;
        }

        BoardController boardController = new BoardController(cells, container);

        return boardController;
    }

}
