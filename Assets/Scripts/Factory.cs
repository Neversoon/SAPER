using UnityEngine;

public abstract class Factory
{
    protected GameObject cellView;
    public Factory()
    {
        cellView = Resources.Load("Prefabs/Cell", typeof(GameObject)) as GameObject;
    }
    public abstract Cell Create(Vector2 position, GameObject container, Vector2Int indexPosition);
}