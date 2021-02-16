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
    BonusName bonusName;

    #region Start Update
    private void Start() => _rb = GetComponent<Rigidbody2D>();

    private void Update() => UpdateVelocity(speed);
    #endregion

    #region Create Bonus
    public void NewBonus(BonusSO bonus)
    {
        bonusSO = bonus;
        bonusName = bonusSO.bonusName;
        spriteRenderer.sprite = bonus.sprite;
    }
    
    public void IsPositive(bool value)
    {
        bonusSO.isPositive = value;

        if (bonusName == BonusName.Random) return;

        spriteRenderer.color = bonusSO.isPositive ? goodBonus : badBonus;
    }

    public void SetForce(int difficult) => bonusSO.SetForce(Random.Range(1, difficult + 1) * bonusSO.multiply);
    #endregion

    void UpdateVelocity(float speedMultiply) => _rb.velocity = _rb.velocity.normalized * speedMultiply;

    public void BonusIsNotRandom()
    {
        BonusName[] temp = (BonusName[])System.Enum.GetValues(typeof(BonusName));
        bonusName = temp[Random.Range(0, temp.Length)];
    }

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
        switch (bonusName)
        {
            case BonusName.Damage:
                collision.gameObject.GetComponentInParent<Player>().stats.AddDamage((int)bonusSO.Force);
                break;

            case BonusName.SpeedPlatform:
                collision.gameObject.GetComponentInParent<PlayerMove>().AddSpeed(bonusSO.Force);
                break;

            case BonusName.Score:
                collision.gameObject.GetComponentInParent<Player>().score.AddScore((int)bonusSO.Force);
                break;

            case BonusName.Random:
                BonusIsNotRandom();
                BonusForPlayer(collision);
                break;
        }
    }
    #endregion;
}
