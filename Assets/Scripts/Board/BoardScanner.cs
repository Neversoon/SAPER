using UnityEngine;

public class BoardScanner
{
    CellObject[][] cells;
    GameModeData gameData;
    public BoardScanner(CellObject[][] cells, GameModeData gameData)
    {
        this.cells = cells;
        this.gameData = gameData;
    }

    public void Scan(CellObject targetCell)
    {
        if (targetCell.cellStateChanger.currentState.cellData.id == 2)
        {
            return;
        }

        FindBombs(targetCell);
    }

    void FindBombs(CellObject targetCell)
    {
        CellData cellData = targetCell.cellStateChanger.currentState.cellData;
        CellView cellView = targetCell.cellStateChanger.currentState.cellView;

        Vector2Int index = targetCell.cellIndex;


        int minY = Mathf.Clamp(index.x - 1, 0, gameData.boardData.sizeY - 1);
        int maxY = Mathf.Clamp(index.x + 1, 0, gameData.boardData.sizeY - 1);

        int minX = Mathf.Clamp(index.y - 1, 0, gameData.boardData.sizeX - 1);
        int maxX = Mathf.Clamp(index.y + 1, 0, gameData.boardData.sizeX - 1);
        
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
            targetCell.cellStateChanger.currentState.SetFlag(false);
            targetCell.cellStateChanger.ChangeState<NumericCell>();
            cellView.ChangeFlagView(false);
            return;
        }
        else
        {
            if (cellData.id == 3)
            {
                return;
            }
            cellView.SetEmptyCell();
            cellView.Disableflag();
            targetCell.cellStateChanger.currentState.SetFlag(false);
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

                if (cellDataTar.id == 1)
                {
                    FindBombs(cells[y][x]);
                }
            }
        }
    }

    public bool HaveClosedEmptyCell()
    {
        for (int y = 0; y < cells.Length; y++)
        {
            for (int x = 0; x < cells[y].Length; x++)
            {
                if (cells[y][x].cellStateChanger.currentState.cellData.id == 1)
                {
                    return true;
                }
            }
        }

        return false;
    }
}
