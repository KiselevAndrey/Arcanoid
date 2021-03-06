﻿using UnityEngine;

public class Block : MonoBehaviour
{
    [Header("Характеристики блока")]
    [SerializeField] int lifes;
    [SerializeField] public int score;
    [SerializeField] bool isInvisible;
    [SerializeField] public bool isImmortal;

    [Header("Доп связи")]
    [SerializeField] Number health;

    BlockCounter _counter;
    SpriteRenderer _spriteRenderer;
    Collider2D _coll2D;
    bool _isDie;

    #region Awake Start
    private void Awake()
    {
        _coll2D = GetComponent<Collider2D>();
        _counter = FindObjectOfType<BlockCounter>();

        if (isInvisible)
            _spriteRenderer = GetComponent<SpriteRenderer>();

        if (isImmortal)
            health.gameObject.SetActive(false);
    }

    private void Start()
    {
        if(isInvisible)
            SetVisibility(false);

        if(lifes > 0)
            UpdateHealth();
    }
    #endregion

    /// <summary>
    /// Проверка на урон больше жизни и реакция на полученный урон
    /// </summary>
    /// <returns></returns>
    bool HitPunch(int damage, Player player)
    {
        if (isInvisible)
        {
            SetVisibility(true);
            isInvisible = false;
            return false;
        }

        if (isImmortal) return false;

        int currentLife = lifes - damage;

        // пробития нет
        if (currentLife > 0)
        {
            SetLifes(currentLife);
            player.score.AddScore(damage);
            return false;
        }
        // пробило
        else
        {
            player.score.AddScore(damage - currentLife + score);
            BlockDied();
            return true;
        }        
    }

    void BlockDied()
    {
        if (_isDie) return;

        _isDie = true;
        _counter.BlockDied(transform.position);
        Destroy(gameObject);
    }

    void SetVisibility(bool value)
    {
        _spriteRenderer.enabled = value;
        health.gameObject.SetActive(value);
    }

    #region Life
    void UpdateHealth() => health.SetNumber(lifes);

    public void SetLifes(int value)
    {
        lifes = value;
        UpdateHealth();
    }

    public int GetLifes() => lifes;
    #endregion

    #region OnEnter OnExit
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == TagsNames.Ball)
        {
            Ball ball = collision.GetComponent<Ball>();
            ball.move.ZeroingTouches();

            if(!HitPunch(ball.stats.Damage, ball.player))
                _coll2D.isTrigger = false;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == TagsNames.Ball)
            _coll2D.isTrigger = true;
    }
    #endregion
}
