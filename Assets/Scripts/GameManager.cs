using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Player[] players;
    public Ball[] balls;

    private void Start()
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
}
