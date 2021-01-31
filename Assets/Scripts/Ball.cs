using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] PlayerMove playerPlatform;
    [SerializeField, Min(1)] float startedSpeed;
    [SerializeField] PlayerManager player;

    Rigidbody2D _rb;
    bool _isStarted;
    int _touchWallCount;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    public void Zeroing()
    {
        _rb.velocity = Vector2.zero;
        _isStarted = false;
        _touchWallCount = 0;
    }
    
    private void Update()
    {
        switch (_isStarted)
        {
            case false:
                PasteToPlayerPlatform();
                break;
        }

        UpdateVelocity();
    }

    void UpdateVelocity()
    {
        if (_rb.velocity.magnitude < 10) _rb.velocity *= 1.2f;
        else if (_rb.velocity.magnitude > 20) _rb.velocity *= 0.8f;
    }

    void PasteToPlayerPlatform()
    {
        transform.position = new Vector2(playerPlatform.transform.position.x, playerPlatform.transform.position.y + playerPlatform.transform.localScale.y);

        if (Input.GetMouseButtonUp(0))
        {
            GetRandomStartedForce();
        }
    }

    void GetRandomStartedForce()
    {
        Vector2 force = new Vector2(Random.Range(-100, 101), Random.Range(20, 101)).normalized;
        _rb.AddForce(force * startedSpeed);
        _isStarted = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == BDNames.Wall)
        {
            _touchWallCount++;
            if (_touchWallCount > 5)
            {
                GetRandomStartedForce();
                _touchWallCount = 0;
            }
        }

        else if (collision.gameObject.tag == BDNames.Respawn)
        {
            player.Hit();
            Zeroing();
        }
        else
        {
            _touchWallCount = 0;
        }
    }


}
