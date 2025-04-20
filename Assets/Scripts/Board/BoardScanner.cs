using UnityEngine;

public class BoardScanner
{
    public void Scan(CellObject targetCell, ref CellObject[][] cells)
    {
        if (targetCell.cellStateChanger.currentState.cellData.id == 2)
        {
            return;
        }
        FindBombs(targetCell, ref cells);
    }

    void FindBombs(CellObject targetCell, ref CellObject[][] cells)
    {

        CellData cellData = targetCell.cellStateChanger.currentState.cellData;
        CellView cellView = targetCell.cellStateChanger.currentState.cellView;

        Vector2Int index = cellData.cellIndex;

        Debug.Log($"{index.x} {index.y}");

        BoardData boardData = new BoardData();

        int minY = Mathf.Clamp(index.x - 1, 0, boardData.sizeY - 1);
        int maxY = Mathf.Clamp(index.x + 1, 0, boardData.sizeY - 1);

        int minX = Mathf.Clamp(index.y - 1, 0, boardData.sizeX - 1);
        int maxX = Mathf.Clamp(index.y + 1, 0, boardData.sizeX - 1);

        int bombCount = 0;

        for (int y = minY; y <= maxY; y++)
        {
            for (int x = minX; x <= maxX; x++)
            {

                CellData cellDataTar = cells[y][x].cellStateChanger.currentState.cellData;

                if (cellDataTar.id == 2)
                {
                    bombCount++;
                }

            }
        }

        if (bombCount != 0)
        {
            cellView.ChangeBombCountText(bombCount);
            targetCell.cellStateChanger.ChangeState<NumericCell>();
            return;
        }
        else
        {
            if (cellData.id == 3)
            {
                return;
            }
            cellView.SetEmptyCell();
            targetCell.cellStateChanger.ChangeState<EmptyCell>();
        }

        for (int y = minY; y <= maxY; y++)
        {
            for (int x = minX; x <= maxX; x++)
            {
                if (x == index.y && y == index.x)
                {
                    continue;
                }

                CellData cellDataTar = cells[y][x].cellStateChanger.currentState.cellData;

                if (cellDataTar.id != 0)
                {
                    FindBombs(cells[y][x], ref cells);
                }
            }
        }
    }

}
