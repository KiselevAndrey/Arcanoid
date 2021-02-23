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
    public BallSound sound;

    [Header("Доп элементы")]
    [SerializeField] GameObject hitParticle;

    [HideInInspector] public Player player;

    #region Awake Start
    private void Awake()
    {
        player = gameManager.players[0];
    }

    private void Start()
    {
        SetDamage();
        //if (!player.CanSetMagnetteBall())
        //{
        //    move.StartBall(true);
        //}
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
        if (player.magnette.CanSetMagnetteBall())
        {
            player.magnette.HaveMagnetteBall(true);
            move.NewMagnettePoint(contactPoint*.9f);
            move.UpdateVelocity(0);
        }
    }
    #endregion

    #region Дейстивия из-за манипуляций игрока
    public void Duplicate()
    {
        Ball newBall = Instantiate(this);
        BeforeInstantiate(newBall);
        gameManager.balls.Add(newBall);
        //gameManager.Paused(); 
    }

    void BeforeInstantiate(Ball newBall)
    {
        newBall.move.SetVelocity(move.GetVelosity());
        newBall.move.BallIsStarted(true);
        newBall.move.GetRandomForce(0.5f);
    }
    #endregion

    #region OnEnter2D
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Instantiate(hitParticle, collision.contacts[0].point, Quaternion.identity);

        switch (collision.gameObject.tag)
        {
            case TagsNames.Wall:
                move.HitWall();
                sound.PlayOneShot(BallStatus.hit);
                return;

            case TagsNames.Respawn:
                player = collision.gameObject.GetComponentInParent<Player>();
                TryDeleteBall();
                sound.PlayOneShot(BallStatus.dead);
                break;

            case TagsNames.Player:
                player = collision.gameObject.GetComponentInParent<Player>();
                SetDamage();
                CheckMagnette(transform.position - player.move.transform.position);
                sound.PlayOneShot(BallStatus.hit);
                break;

            case TagsNames.Savior:
            case TagsNames.Block:
            case TagsNames.Bonus:
                sound.PlayOneShot(BallStatus.hit);
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

    private void OnCollisionExit2D(Collision2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case TagsNames.Block:
                sound.PlayOneShot(BallStatus.hit);
                Instantiate(hitParticle, collision.contacts[0].point, Quaternion.identity);
                break;
        }

    }
    #endregion
}
