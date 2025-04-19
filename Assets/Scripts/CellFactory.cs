using UnityEngine;

public class CellFactory<T> : Factory where T : Cell
{
    public override Cell Create(Vector2 position, GameObject container, Vector2Int cellIndex)
    {
        GameObject cellDefault = Object.Instantiate(cellView.gameObject, position, Quaternion.identity, container.transform);

        cellDefault.name = typeof(T).Name;

        Cell cell = cellDefault.AddComponent<T>();

        cell.cellData.cellIndex = cellIndex;

        return cell;
    }
}