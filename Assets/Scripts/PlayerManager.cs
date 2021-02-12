using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    [Header("Данные игрока")]
    [SerializeField] PlayerSO player;

    [Header("Картинки цифр")]
    [SerializeField] Number life;
    [SerializeField] Number damage;

    [SerializeField] Text scoreText;
    [SerializeField] Text bestScoreText;
    [SerializeField] Text currentScoreText;
    
    int _life;
    int _damage;

    public int GetDamage() => _damage;
    public void AddDamage(int value)
    {
        _damage += value;
        if (_damage < 1) _damage = 1;
        UpdateDamage();
    }

    // Start is called before the first frame update
    void Awake()
    {
        player.Zeroing();

        _life = player.startLifes;
        _damage = player.startDamage;

        UpdateLife();
        UpdateDamage();
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
            Destroy(gameObject);
        }
    }

    void UpdateLife()
    {
        life.SetNumber(_life);
    }

    void UpdateDamage() => damage.SetNumber(_damage);

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
