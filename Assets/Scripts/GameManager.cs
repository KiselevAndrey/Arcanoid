using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{    
    [HideInInspector] public List<Player> players;
    [HideInInspector] public List<Ball> balls;

    GameOverLog _gameOverLog;

    #region Awake Start
    private void Awake()
    {
        players = FindObjectsOfType<Player>().ToList();
        balls = FindObjectsOfType<Ball>().ToList();

        SetIndex();

        _gameOverLog = GetComponent<GameOverLog>();
    }

    private void Start()
    {
        // обнуление игроков

        // обнуление мячей
        for (int i = 0; i < balls.Count; i++)
        {
            balls[i].move.Zeroing();
        }
    }
    #endregion

    /// <summary>
    /// Установка индексов у объектов
    /// </summary>
    void SetIndex()
    {
        for (int i = 0; i < players.Count; i++)
            players[i].indexInGame = i;

        for (int i = 0; i < balls.Count; i++)
            balls[i].indexInGame = i;
    }

    public void GameOver(bool win)
    {
        Time.timeScale = 0;
        _gameOverLog.GameOver(win);
    }

}
