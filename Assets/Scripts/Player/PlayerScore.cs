using UnityEngine;
using UnityEngine.UI;

public class PlayerScore : MonoBehaviour
{
    [Header("Связывающие данные")]
    [SerializeField] Player player;

    [Header("Текстовые объекты, связанные со счетом")]
    [SerializeField] Text scoreText;
    [SerializeField] Text bestScoreText;
    [SerializeField] Text currentScoreText;

    void Awake()
    {
        player.playerSO.Zeroing();
    }

    public void Hit()
    {
        player.playerSO.UpdateBestScore();
        bestScoreText.text = player.playerSO.bestScore.ToString();
        UpdateScore();
    }

    public void UpdateScore()
    {
        scoreText.text = player.playerSO.score.ToString(); ;
        currentScoreText.text = player.playerSO.currentScore.ToString();
    }

    public void AddScore(int value)
    {
        player.playerSO.AddScore(value);
        UpdateScore();
    }
}
