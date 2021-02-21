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

    #region Awake-Start
    private void Awake()
    {
        //_life = player.playerSO.startLifes;
        //_damage = player.playerSO.startDamage;
    }

    private void Start()
    {
        UpdateLifeNumberPict();
        UpdateDamageNumberPict();
    }
    #endregion

    #region Life
    public void SetLife(int value)
    {
        _life = value;
        UpdateLifeNumberPict();
    }

    public void Hit()
    {
        _life--;
        UpdateLifeNumberPict();

        if (_life <= 0)
        {
            player.gameManager.GameOver(false);
        }
    }

    void UpdateLifeNumberPict()
    {
        life.SetNumber(_life);
    }
    #endregion

    #region Damage
    public int GetDamage() => _damage;

    public void SetDamage(int value)
    {
        _damage = value;
        UpdateDamageNumberPict();
    }

    public void AddDamage(int value)
    {
        _damage += value;
        if (_damage < 1) _damage = 1;
        UpdateDamageNumberPict();
    }

    void UpdateDamageNumberPict() => damage.SetNumber(_damage);
    #endregion
}
