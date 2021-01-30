using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] PlayerMove playerPlatform;
    [SerializeField, Min(1)] float startedSpeed;
    [SerializeField] PlayerManager player;

    Rigidbody2D _rb;
    bool _isStarted;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    public void Zeroing()
    {
        _isStarted = false;
    }
    
    private void Update()
    {
        switch (_isStarted)
        {
            case true:

                break;

            case false:
                PasteToPlayerPlatform();
                break;
        }
    }

    void PasteToPlayerPlatform()
    {
        transform.position = new Vector2(playerPlatform.transform.position.x, playerPlatform.transform.position.y + playerPlatform.transform.localScale.y);

        if (Input.GetMouseButtonUp(0))
        {
            GetRandomStartedForce();
            _isStarted = true;
        }
    }

    void GetRandomStartedForce()
    {
        Vector2 force = new Vector2(Random.Range(-100, 101), Random.Range(20, 101)).normalized;
        _rb.velocity = Vector2.zero;
        _rb.AddForce(force * startedSpeed);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == BDNames.Respawn)
        {
            player.Hit();
            _isStarted = false;
        }
    }
}
