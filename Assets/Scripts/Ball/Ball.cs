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

    #region Awake Start
    private void Awake()
    {
        player = gameManager.players[0];
    }

    private void Start()
    {
        SetDamage();
        if (!player.TrySetMagnetteBall()) move.GetRandomForce(0.5f);
    }
    #endregion

    #region Взаимодейстивия с зоной падения мяча BallTriger
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
    #endregion

    #region Взаимодейстивия с игроком
    void SetDamage()
    {
        stats.Damage = player.stats.GetDamage();
        move.speed = move.startSpeed + stats.Damage;
    }

    void CheckMagnette(Vector2 contactPoint)
    {
        if (player.TrySetMagnetteBall())
        {
            player.HaveMagnetteBall(true);
            move.NewMagnettePoint(contactPoint);
        }
    }
    #endregion

    #region Дейстивия из-за манипуляций игрока
    public void Duplicate()
    {
        Ball ball = Instantiate(this);
        ball.move.SetVelocity(move.GetVelosity());
        ball.move.GetRandomForce(0.5f);
        gameManager.balls.Add(ball);
    }
    #endregion

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
                CheckMagnette(transform.position - player.move.transform.position);
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
