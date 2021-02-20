using UnityEngine;
using UnityEngine.UI;

public class GameOverLog : MonoBehaviour
{
    [SerializeField] GameObject gameOverLog;
    [SerializeField] Text scoreText;

    [SerializeField] GameObject winLog;

    GameManager _gameManager;

    #region Awake Start
    private void Awake()
    {
        _gameManager = GetComponent<GameManager>();
    }

    private void Start()
    {
        gameOverLog.SetActive(false);
    }
    #endregion

    public void GameOver(bool win)
    {
        gameOverLog.SetActive(true);
        FindBestPlayer();

        if (win)
        {
            winLog.SetActive(true);
        }
        else
        {
            winLog.SetActive(false);
        }
    }

    void FindBestPlayer()
    {
        Player bestPlayer = _gameManager.players[0];

        for (int i = 1; i < _gameManager.players.Count; i++)
        {
            if (bestPlayer.playerSO.score < _gameManager.players[i].playerSO.score)
            {
                bestPlayer = _gameManager.players[i];
            }
        }

        string text = "Лучший счет у игрока: " + bestPlayer.gameObject.name + "!!\n У него очков: " + bestPlayer.playerSO.score;
        scoreText.text = text;
    }
}
