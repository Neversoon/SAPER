using UnityEngine;

public class GameModesController
{
    public GameModeData currentData { get; private set; } = GameModes.easyMode;

    public void ChangeCurrentData(GameModeData currentData)
    {
        this.currentData = currentData;
    }
    public GameModeData GetCurrentGameData()
    {
        return new GameModeData
        {
            boardData = currentData.boardData,
            gameRules = currentData.gameRules
        };
    }
}
