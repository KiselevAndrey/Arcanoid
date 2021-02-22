using UnityEngine;
using UnityEngine.UI;

public class GameOverLog : MonoBehaviour
{
    [SerializeField] GameObject gameOverLog;
    [SerializeField] Text scoreText;

    [SerializeField] GameObject winLog;

    [Header("Счета")]
    [SerializeField] Text bestScoreText;
    [SerializeField] Text currentScoreText;


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
            _gameManager.gameOptions.UpdateMaxLVL(int.Parse(_gameManager.lvlOptions.lvlStats.name[_gameManager.lvlOptions.lvlStats.name.Length - 1].ToString()) + 1);
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
            if (bestPlayer.playerSO.currentScore < _gameManager.players[i].playerSO.currentScore)
            {
                bestPlayer = _gameManager.players[i];
            }
        }

        string text = "Лучший счет у игрока: " + bestPlayer.gameObject.name + "!!\n У него очков: " + bestPlayer.playerSO.currentScore;
        scoreText.text = text;

        _gameManager.lvlOptions.UpdateBestScore(bestPlayer.playerSO.currentScore);
        bestScoreText.text = _gameManager.lvlOptions.lvlStats.bestScore.ToString();
        currentScoreText.text = bestPlayer.playerSO.currentScore.ToString();
    }
}
