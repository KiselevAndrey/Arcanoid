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
    }
    
    bool HitPunch(int damage, string playerName)
    {
        int currentLife = lifes - damage;

        // пробития нет
        if(currentLife > 0)
        {
            lifes = currentLife;
            _instantiater.GiveScoreToPlayer(playerName, lifes - damage);
            return false;
        }
        // пробило
        else
        {
            _instantiater.GiveScoreToPlayer(playerName, damage - currentLife + score);
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
            if(!HitPunch(ball.damage, ball.playerName))
                coll2D.isTrigger = false;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == BDNames.Ball)
        {
            coll2D.isTrigger = true;
        }
    }
}
