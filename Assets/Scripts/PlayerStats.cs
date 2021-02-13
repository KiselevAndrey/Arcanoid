using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [Header("Связывающие данные")]
    [SerializeField] Player player;

    [Header("Картинки цифр")]
    [SerializeField] Number life;
    [SerializeField] Number damage;

    int _life;
    int _damage;

    public int GetDamage() => _damage;
    public void AddDamage(int value)
    {
        _damage += value;
        if (_damage < 1) _damage = 1;
        UpdateDamageNumberPict();
    }

    private void Awake()
    {
        _life = player.playerSO.startLifes;
        _damage = player.playerSO.startDamage;
    }

    private void Start()
    {
        UpdateLifeNumberPict();
        UpdateDamageNumberPict();
    }

    public void Hit()
    {
        _life--;
        UpdateLifeNumberPict();

        if (_life <= 0)
        {
            print("Game Over");
            Destroy(gameObject);
        }
    }

    void UpdateLifeNumberPict()
    {
        life.SetNumber(_life);
    }

    void UpdateDamageNumberPict() => damage.SetNumber(_damage);
}
