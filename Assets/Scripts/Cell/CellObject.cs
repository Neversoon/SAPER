using UnityEngine;

public class CellObject : MonoBehaviour
{
    public CellStateChanger cellStateChanger;
    public CellData cellData { get; set; } = new CellData();
    public CellView cellView { get; protected set; }
}
