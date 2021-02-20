using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Данные по игре")]
    [SerializeField] GameOptionsSO gameOptions;

    [Header("Данные по уровню")]
    [SerializeField] GameObject pauseMenu;

    [HideInInspector] public List<Player> players;
    [HideInInspector] public List<Ball> balls;

    GameOverLog _gameOverLog;
    bool _isPause;

    #region Awake Start Update
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
            Paused();
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

    // Отработка действий при паузе
    void Paused()
    {
        _isPause = !_isPause;
        pauseMenu.SetActive(_isPause);

        Time.timeScale = _isPause ? 0 : 1;
    }

    public void StartNewGame()
    {
        gameOptions.gameStatus = GameStatus.New;
        ManagerSceneStatic.LoadScene(LVLNames.IntToLVLName(1));
    }
}
