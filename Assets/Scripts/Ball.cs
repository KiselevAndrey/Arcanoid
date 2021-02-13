using UnityEngine;

public class Ball : MonoBehaviour
{
    [Header("Связывающие данные")]
    public GameManager gameManager;
    [HideInInspector] public int indexInGame;

    [Header("Скрипты связанные с мячом")]
    public BallMove move;
    public BallStats stats;
    public BallStartDirection startDirection;

    public Player player;

    private void Start()
    {
        player = gameManager.players[indexInGame];
        stats.Damage = player.stats.GetDamage();
    }

    #region OnEnter2D
    private void OnCollisionEnter2D(Collision2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case TagsNames.Wall:
                move.HitWall();
                return;

            case TagsNames.Respawn:
                player.Hit();
                move.Zeroing();
                break;

            case TagsNames.Player:
                player = collision.gameObject.GetComponentInParent<Player>();
                stats.Damage = player.stats.GetDamage();
                break;
        }

        move.ZeroingTouches();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case TagsNames.Player:
                player = collision.gameObject.GetComponent<Player>();
                stats.Damage = player.stats.GetDamage();
                break;
        }
    }
    #endregion
}
