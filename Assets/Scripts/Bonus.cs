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

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        UpdateVelocity(speed);
    }

    public void NewBonus(BonusSO bonus)
    {
        bonusSO = bonus;
        spriteRenderer.sprite = bonus.sprite;

        if(bonus.bonusName == BonusName.Random)
        {
            var temp = System.Enum.GetValues(typeof(BonusName));
            bonusSO = (BonusSO)temp.GetValue(1);
        }
    }

    public void SetForce(int difficult)
    {
        bonusSO.SetForce(Random.Range(1, difficult + 1) * bonusSO.multiply);
    }

    public void IsPositive(bool value)
    {
        bonusSO.isPositive = value;

        if (bonusSO.bonusName == BonusName.Random) return;

        spriteRenderer.color = bonusSO.isPositive ? goodBonus : badBonus;
    }

    void UpdateVelocity(float speedMultiply)
    {
        _rb.velocity = _rb.velocity.normalized * speedMultiply;
    }

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
        switch (bonusSO.bonusName)
        {
            case BonusName.Damage:
                collision.gameObject.GetComponent<Player>().stats.AddDamage((int)bonusSO.Force);
                break;

            case BonusName.SpeedPlatform:
                collision.gameObject.GetComponent<PlayerMove>().AddSpeed(bonusSO.Force);
                break;

            case BonusName.Score:
                collision.gameObject.GetComponent<Player>().score.AddScore((int)bonusSO.Force);
                break;
        }
    }

}
