using UnityEngine;
using UnityEngine.Events;

public class GameEvents : MonoBehaviour
{
    public UnityAction lostGame;
    public UnityAction winGame;
    public UnityAction startGame;
    public UnityAction restartGame;

    private static GameEvents _instance;
    public static GameEvents Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject gameEvents = new GameObject("GameEvents");
                return gameEvents.AddComponent<GameEvents>();
            }
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

}
