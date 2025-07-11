using UnityEngine;

public class CellSelection
{
    CellObject[][] cells;
    public CellSelection(CellObject[][] cells) => this.cells = cells;

    public CellObject FindSelection(Vector2 inputPosition)
    {
        for (int y = 0; y < cells.Length; y++)
        {
            for (int x = 0; x < cells[y].Length; x++)
            {
                if (cells[y][x] == null)
                {
                    Debug.Log($"Cell {y} {x} == null");
                }

                float distance = Vector2.Distance(cells[y][x].transform.position, inputPosition);

                if (distance < 0.5f)
                {
                    return cells[y][x];
                }
            }
        }
        return null;
    }

    public void MakeFirstClosedEmptyCellBomb()
    {
        for (int y = 0; y < cells.Length; y++)
        {
            for (int x = 0; x < cells[y].Length; x++)
            {
                if (cells[y][x] != null && cells[y][x].cellStateChanger.currentState.cellData.id == 1)
                {
                    cells[y][x].cellStateChanger.ChangeState<BombCell>();
                    return;
                }
            }
        }
    }
}
