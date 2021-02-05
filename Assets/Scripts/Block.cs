using UnityEngine;

public class Block : MonoBehaviour
{
    [Header("Характеристики блока")]
    [SerializeField] public int score;
    [SerializeField] bool isInvisible;
    [SerializeField] bool isImmortal;

    [Header("Доп связи")]
    [SerializeField] Number health;

   // PlayerManager _player;
    BlockInstantiater _instantiater;
    SpriteRenderer _spriteRenderer;
    Collider2D _coll2D;
    int _lifes = 0;

    private void Start()
    {
        _instantiater = GameObject.FindGameObjectWithTag(BDNames.BlockInstantiater).GetComponent<BlockInstantiater>();
        _coll2D = GetComponent<Collider2D>();

        if(isInvisible)
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _spriteRenderer.enabled = false;
            health.gameObject.SetActive(false);
        }
    }

    bool HitPunch(int damage, PlayerManager player)
    {
        if (isInvisible)
        {
            _spriteRenderer.enabled = true;
            health.gameObject.SetActive(true);
            isInvisible = false;
            return false;
        }

        if (isImmortal) return false;

        int currentLife = _lifes - damage;

        // пробития нет
        if (currentLife > 0)
        {
            SetLifes(currentLife);
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

    void UpdateHealth() => health.SetNumber(_lifes);

    public void SetLifes(int value)
    {
        print(value);
        _lifes = value;
        UpdateHealth();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == BDNames.Ball)
        {
            Ball ball = collision.GetComponent<Ball>();
            ball.ZeroingTouches();

            if(!HitPunch(ball.damage, ball.GetPlayer()))
                _coll2D.isTrigger = false;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == BDNames.Ball)
        {
            _coll2D.isTrigger = true;
        }
    }
}
