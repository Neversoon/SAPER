using Unity.VisualScripting;
using UnityEngine;

public class CellFactory<T> where T : ClosedCell
{
    protected CellObject cellObject;
    public CellFactory()
    {
        cellObject = Resources.Load("Prefabs/Cell", typeof(CellObject)) as CellObject;
    }
    public CellObject Create(Vector2 position, GameObject container, Vector2Int cellIndex)
    {
        CellObject cellDefault = Object.Instantiate(cellObject, position, Quaternion.identity, container.transform);

        cellDefault.name = typeof(T).Name;
        
        cellDefault.cellStateChanger.ChangeState<T>();

        cellDefault.cellStateChanger.currentState.cellData.cellIndex = cellIndex;
        return cellDefault;
    }
}