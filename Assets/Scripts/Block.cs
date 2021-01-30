using UnityEngine;

public class Block : MonoBehaviour
{
    [Header("Характеристики блока")]
    [SerializeField, Min(1)] int lifes;
    [SerializeField] int score;

    PlayerManager _player;

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag(BDNames.Player).GetComponent<PlayerManager>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == BDNames.Ball)
        {
            Hit();
        }
    }

    void Hit()
    {
        lifes--;
        _player.AddScore(1);

        if (lifes <= 0)
        {
            _player.AddScore(score);
            Destroy(gameObject);
        }
    }
}
