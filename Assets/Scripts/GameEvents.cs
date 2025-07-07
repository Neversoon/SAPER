using UnityEngine;
using UnityEngine.Events;

public class GameEvents : MonoBehaviour
{
    public UnityAction lostGame;
    public UnityAction winGame;
    public UnityAction startGame;
    public UnityAction restartGame;
    public UnityAction userOpenFirstCell;
    public UnityAction<int> userFlagCountChanged;

    private static GameEvents _instance;
    public static GameEvents Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindAnyObjectByType<GameEvents>();

                if (_instance == null)
                {
                    GameObject obj = new GameObject("GameEvents");
                    _instance = obj.AddComponent<GameEvents>();
                    DontDestroyOnLoad(obj);
                }
            }

            return _instance;
        }
    }
}
