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
        player = gameManager.players[0];
        SetDamage();
    }

    void SetDamage()
    {
        stats.Damage = player.stats.GetDamage();
        move.speed = move.startSpeed + stats.Damage;
    }

    public void TryDeleteBall()
    {
        if (gameManager.balls.Count > gameManager.players.Count)
        {
            gameManager.balls.Remove(this);
            Destroy(gameObject);
        }
        else
        {
            player.Hit();
            move.Zeroing();
        }
    }

    public void Duplicate()
    {
        Ball ball = Instantiate(this);
        ball.move.SetVelocity(move.GetVelosity());
        ball.move.GetRandomForce(0.5f);
        gameManager.balls.Add(ball);
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
                player = collision.gameObject.GetComponentInParent<Player>();
                TryDeleteBall();
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
