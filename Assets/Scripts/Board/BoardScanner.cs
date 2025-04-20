using UnityEngine;

public class BoardScanner
{
    public void Scan(CellObject targetCell, ref CellObject[][] cells)
    {
        Vector2Int index = targetCell.cellData.cellIndex;

        BoardData boardData = new BoardData();

        int minY = Mathf.Clamp(index.y - 1, 0, boardData.sizeY - 1);
        int maxY = Mathf.Clamp(index.y + 1, 0, boardData.sizeY - 1);

        int minX = Mathf.Clamp(index.x - 1, 0, boardData.sizeX - 1);
        int maxX = Mathf.Clamp(index.x + 1, 0, boardData.sizeX - 1);

        for (int y = minY; y <= maxY; y++)
        {
            for (int x = minX; x <= maxX; x++)
            {
                if (cells[y][x].cellData.id != 0)
                {
                    FindBombs(cells[y][x], ref cells);
                }
            }
        }
    }

    void FindBombs(CellObject targetCell, ref CellObject[][] cells)
    {
        Vector2Int index = targetCell.cellData.cellIndex;

        BoardData boardData = new BoardData();

        int minY = Mathf.Clamp(index.y - 1, 0, boardData.sizeY - 1);
        int maxY = Mathf.Clamp(index.y + 1, 0, boardData.sizeY - 1);

        int minX = Mathf.Clamp(index.x - 1, 0, boardData.sizeX - 1);
        int maxX = Mathf.Clamp(index.x + 1, 0, boardData.sizeX - 1);

        int bombCount = 0;

        for (int y = minY; y <= maxY; y++)
        {
            for (int x = minX; x <= maxX; x++)
            {
                if (cells[y][x].cellData.id == 2)
                {
                    bombCount++;
                }

            }
        }

        if (bombCount != 0)
        {
            targetCell.cellView.ChangeBombCountText(bombCount);
            return;
        }
        else
        {
            targetCell.cellView.SetEmptyCell();
            targetCell.cellData.id = 0;
        }

        for (int y = minY; y <= maxY; y++)
        {
            for (int x = minX; x <= maxX; x++)
            {
                if (cells[y][x].cellData.id != 0)
                {
                    FindBombs(cells[y][x], ref cells);
                }
            }
        }
    }

}
