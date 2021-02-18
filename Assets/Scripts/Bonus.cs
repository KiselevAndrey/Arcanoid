using UnityEngine;


public class Bonus : MonoBehaviour
{
    [SerializeField] SpriteRenderer spriteRenderer;
    [HideInInspector] public BonusSO bonusSO;
    [SerializeField, Min(0)] float speed;

    [Header("Цвета бонусов")]
    [SerializeField] Color goodBonus;
    [SerializeField] Color badBonus;


    Rigidbody2D _rb;
    BonusName _bonusName;
    bool _isPositive;

    #region Start Update
    private void Start() => _rb = GetComponent<Rigidbody2D>();

    private void Update() => UpdateVelocity(speed);
    #endregion

    #region Create Bonus
    public void NewBonus(BonusSO bonus)
    {
        bonusSO = bonus;
        _bonusName = bonusSO.bonusName;
        spriteRenderer.sprite = bonus.sprite;
    }
    
    public void IsPositive(bool value)
    {
        switch (_bonusName)
        {
            case BonusName.Random:
            case BonusName.DublicateBall:
                break;
            default:
                _isPositive = value;
                spriteRenderer.color = _isPositive ? goodBonus : badBonus;
                break;
        }
    }

    public void SetForce(int difficult) => bonusSO.Force = Random.Range(1, difficult + 1) * bonusSO.multiply * (_isPositive ? 1 : -1);
    #endregion

    void UpdateVelocity(float speedMultiply) => _rb.velocity = _rb.velocity.normalized * speedMultiply;

    #region From Collisions Detected
    /// <summary>
    /// Определение рандомного бонуса на не рандомный
    /// </summary>
    void BonusIsNotRandom()
    {
        BonusName[] temp = (BonusName[])System.Enum.GetValues(typeof(BonusName));
        _bonusName = temp[Random.Range(0, temp.Length)];
    }

    /// <summary>
    /// раздвоение первого мяча принадлежавшего игроку
    /// </summary>
    void DoublerBall(Collision2D collision)
    {
        Player player = collision.gameObject.GetComponentInParent<Player>();

        // пробегаемся по всем мячам в игре
        for (int i = 0; i < player.gameManager.balls.Count; i++)
        {
            // ищем мяч, кот относится к нужному игроку
            if (player.gameManager.balls[i].player == player)
            {
                player.gameManager.balls[i].Duplicate();
                return;
            }
        }

        // если нет у игрока мячей, то получить бонус
        player.score.AddScore((int)(bonusSO.Force * player.gameManager.balls.Count));
    }
    #endregion

    #region Collision Enter
    private void OnCollisionEnter2D(Collision2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case TagsNames.Respawn:
                Destroy(gameObject);
                break;

            case TagsNames.Player:
                BonusForPlayer(collision);
                Destroy(gameObject);
                break;
        }
    }

    void BonusForPlayer(Collision2D collision)
    {
        switch (_bonusName)
        {
            case BonusName.Damage:
                collision.gameObject.GetComponent<PlayerStats>().AddDamage((int)bonusSO.Force);
                break;

            case BonusName.SpeedPlatform:
                collision.gameObject.GetComponent<PlayerMove>().AddSpeed(bonusSO.Force);
                break;

            case BonusName.Score:
                collision.gameObject.GetComponent<PlayerScore>().AddScore((int)(bonusSO.Force * Random.Range(1, 6)));
                break;

            case BonusName.Random:
                BonusIsNotRandom();
                BonusForPlayer(collision);
                break;

            case BonusName.DublicateBall:
                DoublerBall(collision);
                break;
        }

        // бонус за вредность
        if (!_isPositive)
            collision.gameObject.GetComponent<PlayerScore>().AddScore((int)(bonusSO.Force * 2));
    }
    #endregion;
}
