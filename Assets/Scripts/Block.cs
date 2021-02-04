using UnityEngine;

public class Block : MonoBehaviour
{
    [Header("Характеристики блока")]
    [SerializeField, Min(1)] public int lifes;
    [SerializeField] public int score;

    [SerializeField] Collider2D coll2D;

   // PlayerManager _player;
    BlockInstantiater _instantiater;
    string playerName;

    private void Start()
    {
        _instantiater = GameObject.FindGameObjectWithTag(BDNames.BlockInstantiater).GetComponent<BlockInstantiater>();
        coll2D = GetComponent<Collider2D>();
    }
    
    bool HitPunch(int damage, PlayerManager player)
    {
        int currentLife = lifes - damage;

        // пробития нет
        if(currentLife > 0)
        {
            lifes = currentLife;
            _instantiater.GiveScoreToPlayer(player, damage);
            return false;
        }
        // пробило
        else
        {
            _instantiater.GiveScoreToPlayer(player, damage - currentLife + score);
            _instantiater.BlockDied();
            Destroy(gameObject);
            return true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == BDNames.Ball)
        {
            Ball ball = collision.GetComponent<Ball>();
            ball.ZeroingTouches();

            if(!HitPunch(ball.damage, ball.GetPlayer()))
                coll2D.isTrigger = false;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == BDNames.Ball)
        {
            coll2D.isTrigger = true;
        }
    }
}
