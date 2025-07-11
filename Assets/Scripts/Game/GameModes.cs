using UnityEngine;

public static class GameModes
{
    public readonly static GameModeData easyMode = new GameModeData
    {
        boardData = new BoardData
        {
            sizeX = 10,
            sizeY = 10
        },
        gameRules = new GameRules
        {
            badCellCount = 10
        }
    };
    public readonly static GameModeData mediumMode = new GameModeData
    {
        boardData = new BoardData
        {
            sizeX = 17,
            sizeY = 17
        },
        gameRules = new GameRules
        {
            badCellCount = 40
        }
    };
    public readonly static GameModeData hardMode = new GameModeData
    {
        boardData = new BoardData
        {
            sizeX = 31,
            sizeY = 17
        },
        gameRules = new GameRules
        {
            badCellCount = 99
        }
    };
}
