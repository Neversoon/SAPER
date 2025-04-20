using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CellStateChanger : MonoBehaviour, ICellStateChanger
{
    [SerializeField] CellView cellView;
    List<Cell> cellStates = new List<Cell>();
    public Cell currentState;

    void Awake()
    {
        cellStates = new List<Cell>(){
            new BombCell(cellView, this),
            new EmptyCell(cellView, this),
            new EmptyClosedCell(cellView, this),
            new NumericCell(cellView, this)
        };
    }

    public void ChangeState<T>() where T : Cell
    {
        currentState = cellStates.FirstOrDefault(stateFromList => stateFromList is T);
    }
}