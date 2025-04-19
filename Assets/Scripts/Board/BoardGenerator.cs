using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BoardGenerator
{
    GameRules gameRules = new GameRules();

    public Cell[,] GenerateBoard(BoardData boardData)
    {
        Cell[,] cells = new Cell[boardData.sizeY, boardData.sizeX];

        GameObject container = new GameObject("Board");

        float offsetY = boardData.sizeY / 2;
        float offsetX = boardData.sizeX / 2;

        Vector2Int[][] points = new Vector2Int[boardData.sizeY][];



        for (int y = 0; y < boardData.sizeY; y++)
        {
            points[y] = new Vector2Int[boardData.sizeX];

            for (int x = 0; x < boardData.sizeX; x++)
            {
                points[y][x] = new Vector2Int(x, y);
            }
        }

        for (int i = 0; i < gameRules.badCellCount; i++)
        {
            int randomY = Random.Range(0, points.Length);

            int randomX = Random.Range(0, points[randomY].Length);

            Vector2Int randomPoint = points[randomY][randomX];

            CellFactory<BombCell> factory = new CellFactory<BombCell>();

            Vector2 createPosition = new Vector2(randomPoint.x - offsetX, randomPoint.y - offsetY);

            Cell cell = factory.Create(createPosition, container, new Vector2Int(randomPoint.y, randomPoint.x));

            cells[randomY, randomX] = cell;

            RemovePoint(ref points, randomY, randomX);
        }

        for (int y = 0; y < points.Length; y++)
        {
            for (int x = 0; x < points[y].Length; x++)
            {
                CellFactory<EmptyCell> factory = new CellFactory<EmptyCell>();

                Vector2 createPosition = new Vector2(points[y][x].x - offsetX, points[y][x].y - offsetY);

                Cell cell = factory.Create(createPosition, container, new Vector2Int(y, x));

                cells[points[y][x].y, points[y][x].x] = cell;
            }
        }

        return cells;
    }

    void RemovePoint(ref Vector2Int[][] points, int indexY, int indexX)
    {
        int len = points[indexY].Length;

        if (len <= 1)
        {
            int yLen = points.Length;
            Vector2Int[][] newPoints = new Vector2Int[yLen - 1][];

            int newIndexY = 0;
            for (int y = 0; y < yLen; y++)
            {
                if (y != indexY)
                {
                    newPoints[newIndexY] = points[y];
                    newIndexY++;
                }
            }

            points = newPoints;
            return;
        }

        Vector2Int[] newRow = new Vector2Int[len - 1];
        int newIndex = 0;

        for (int i = 0; i < len; i++)
        {
            if (i != indexX)
            {
                newRow[newIndex] = points[indexY][i];
                newIndex++;
            }
        }

        points[indexY] = newRow;
    }

}
