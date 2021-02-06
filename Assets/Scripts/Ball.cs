using UnityEngine;

public class Ball : MonoBehaviour
{
    public int damage = 10;

    BallMove _ballMove;
    PlayerManager player;
    public PlayerManager GetPlayer() => player;

    private void Awake()
    {
        _ballMove = GetComponent<BallMove>();
        player = GameObject.FindGameObjectWithTag(BDNames.Player).GetComponent<PlayerManager>();
        damage = player.GetDamage();
    }

    public void Zeroing() => _ballMove.Zeroing();

    public void ZeroingTouches() => _ballMove.ZeroingTouches();

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == BDNames.Respawn)
        {
            player.Hit();
            Zeroing();
        }
        else if (collision.gameObject.tag == BDNames.Player)
        {
            player = collision.gameObject.GetComponent<PlayerManager>();
            damage = player.GetDamage();
        }
    }
}
