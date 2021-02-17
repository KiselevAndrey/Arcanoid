using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject winLog;
    
    [HideInInspector] public Player[] players;
    [HideInInspector] public Ball[] balls;

    private void Awake()
    {
        players = FindObjectsOfType<Player>();
        balls = FindObjectsOfType<Ball>();

        SetIndex();
    }

    /// <summary>
    /// Установка индексов у объектов
    /// </summary>
    void SetIndex()
    {
        for (int i = 0; i < players.Length; i++)
            players[i].indexInGame = i;

        for (int i = 0; i < balls.Length; i++)
            balls[i].indexInGame = i;
    }

    public void Win()
    {
        winLog.SetActive(true);
        for (int i = 0; i < players.Length; i++)
        {
            balls[i]?.move.Zeroing();
        }
        for (int i = players.Length-1; i < balls.Length; i++)
        {
            balls[i].gameObject();
        }
    }
}
