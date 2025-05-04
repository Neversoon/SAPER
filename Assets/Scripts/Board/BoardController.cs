using UnityEngine;

public class BoardController
{
    public CellObject[][] cells { get; private set; }
    GameObject container;

    public BoardController(CellObject[][] cells, GameObject container)
    {
        this.cells = cells;
        this.container = container;
    }

    public void Clear()
    {
        cells = new CellObject[0][];
        Object.Destroy(container);
    }
}
