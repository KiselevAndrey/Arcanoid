using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] PlayerSO player;
    [SerializeField] Text lifeText;
    [SerializeField] Text scoreText;
    [SerializeField] Text bestScoreText;
    [SerializeField] Text currentScoreText;

    int _life;

    // Start is called before the first frame update
    void Start()
    {
        player.Zeroing();

        _life = player.startLifes;
        UpdateLife();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Hit()
    {
        _life--;
        UpdateLife();

        player.UpdateBestScore();
        bestScoreText.text = player.bestScore.ToString();
        UpdateScore();

        if (_life <= 0)
        {
            print("Game Over");
        }
    }

    void UpdateLife()
    {
        lifeText.text = _life.ToString();
    }

    public void UpdateScore()
    {
        scoreText.text = player.score.ToString();
        currentScoreText.text = player.currentScore.ToString();
    }

    public void AddScore(int value)
    {
        player.AddScore(value);
        UpdateScore();
    }

}
