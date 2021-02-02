using UnityEngine;

public class Block : MonoBehaviour
{
    [Header("Характеристики блока")]
    [SerializeField, Min(1)] public int lifes;
    [SerializeField] public int score;

    PlayerManager _player;
    BlockInstantiater _instantiater;

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag(BDNames.Player).GetComponent<PlayerManager>();
        _instantiater = GameObject.FindGameObjectWithTag(BDNames.BlockInstantiater).GetComponent<BlockInstantiater>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
    }

    bool Hit(int damage)
    {
        
        if(damage > lifes)
        {
            return false;
        }
        else
        {
            return true;
        }
        lifes -= damage;
        _player.AddScore(damage);

        if (lifes <= 0)
        {
            _player.AddScore(score);
            _instantiater.Hit();
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == BDNames.Ball)
        {
            Hit(1);
        }
    }
}
