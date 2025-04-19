using UnityEngine;

public abstract class Cell : MonoBehaviour
{
    public CellData cellData { get; set; } = new CellData();
    protected CellView cellView;
    public abstract void Tap();
}
