using UnityEngine;

public class CellFactory<T> where T : Cell
{
    protected GameObject cellView;
    public CellFactory()
    {
        cellView = Resources.Load("Prefabs/Cell", typeof(GameObject)) as GameObject;
    }
    public Cell Create(Vector2 position, GameObject container, Vector2Int cellIndex)
    {
        GameObject cellDefault = Object.Instantiate(cellView.gameObject, position, Quaternion.identity, container.transform);

        cellDefault.name = typeof(T).Name;

        Cell cell = cellDefault.AddComponent<T>();

        cell.cellData.cellIndex = cellIndex;

        return cell;
    }
}