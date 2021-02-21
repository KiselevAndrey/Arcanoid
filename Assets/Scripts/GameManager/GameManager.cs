using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Данные по игре")]
    [SerializeField] GameOptionsSO gameOptions;

    [HideInInspector] public List<Player> players;
    [HideInInspector] public List<Ball> balls;

    GameOverLog _gameOverLog;
    Pause _pause;
    bool _isPause;

    #region Awake Start Update
    private void Awake()
    {
        players = FindObjectsOfType<Player>().ToList();
        balls = FindObjectsOfType<Ball>().ToList();

        SetIndex();

        _gameOverLog = GetComponent<GameOverLog>();
        _pause = GetComponent<Pause>();
    }

    private void Start()
    {
        // обнуление игроков
        switch (gameOptions.gameStatus)
        {
            case GameStatus.New:
                for (int i = 0; i < players.Count; i++)
                {
                    players[i].playerSO.Zeroing();
                }
                break;

            case GameStatus.Load:
                break;
        }

        // обнуление мячей
        for (int i = 0; i < balls.Count; i++)
        {
            balls[i].move.Zeroing();
        }

        Time.timeScale = 1;
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
            _pause.Paused(); 
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

    #region GameOver
    /// <summary>
    /// Что происходит при завершении игры
    /// </summary>
    /// <param name="win">выиграл или проиграл</param>
    public void GameOver(bool win)
    {
        Time.timeScale = 0;
        _gameOverLog.GameOver(win);

        for (int i = 0; i < players.Count; i++)
        {
            players[i].GameOver();
        }
    }
    #endregion

    public void StartNewGame()
    {
        gameOptions.gameStatus = GameStatus.New;
        gameOptions.maxLevel = 1;
        ManagerSceneStatic.LoadScene(LVLNames.IntToLVLName(1));
    }
}
