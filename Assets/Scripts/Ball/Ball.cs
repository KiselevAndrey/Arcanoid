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

    [HideInInspector] public Player player;

    private void Start()
    {
        player = gameManager.players[indexInGame];
        SetDamage();
    }

    void SetDamage()
    {
        stats.Damage = player.stats.GetDamage();
        move.speed = move.startSpeed + stats.Damage;
    }

    /// <summary>
    /// Метод, кот покажет надо ли уничтожать мяч или перенести его на платформу к игроку
    /// </summary>
    public void CheckBallDestroy()
    {

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
                SetDamage();
                break;
        }

        move.ZeroingTouches();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case TagsNames.Player:
                player = collision.gameObject.GetComponentInParent<Player>();
                SetDamage();
                break;
        }
    }
    #endregion
}
