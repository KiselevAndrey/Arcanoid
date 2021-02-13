using UnityEngine;

public class Ball : MonoBehaviour
{
    [Header("Связывающие данные")]
    public GameManager gameManager;
    [HideInInspector] public int indexInGame;

    [Header("Скрипты связанные с мячом")]
    public BallMove move;

    public int damage = 10;

    BallMove _ballMove;
    Player player;
    public Player GetPlayer() => player;

    private void Start()
    {
        _ballMove = GetComponent<BallMove>();
        _ballMove.Zeroing();
        //player = GameObject.FindGameObjectWithTag(BDNames.Player).GetComponent<Player>();
        //damage = player.stats.GetDamage();
    }

    public void Zeroing() => _ballMove.Zeroing();

    public void ZeroingTouches() => _ballMove.ZeroingTouches();

    private void OnCollisionEnter2D(Collision2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case TagsNames.Respawn:
                //player.Hit();
                Zeroing();
                break;

            case TagsNames.Player:
                //player = collision.gameObject.GetComponent<Player>();
                //damage = player.stats.GetDamage();
                break;
        }
    }
}
