using UnityEngine;

public class BallMove : MonoBehaviour
{
    [Header("Связывающие данные")]
    [SerializeField] Ball ball;

    [SerializeField, Range(1, 10)] float speed;

    PlayerMove _playerPlatform;
    Rigidbody2D _rb;
    Collider2D _collider;
    TrailRenderer _trail;

    bool _isStarted;
    int _touchWallCount;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _collider = GetComponent<Collider2D>();
        _trail = GetComponentInChildren<TrailRenderer>();
    }

    private void Start()
    {
        _playerPlatform = ball.gameManager.players[ball.indexInGame].move;
    }

    private void FixedUpdate()
    {
        switch (_isStarted)
        {
            case true:

                UpdateVelocity(speed);

                break;

            case false:

                PasteToPlayerPlatform();

                break;
        }
    }

    void UpdateVelocity(float speedMultiply)
    {
        _rb.velocity = _rb.velocity.normalized * speedMultiply;
    }

    public void Zeroing()
    {
        UpdateVelocity(0);
        ZeroingTouches();
        _trail.gameObject.SetActive(false);
        _isStarted = false;
        _collider.isTrigger = true;
    }

    void PasteToPlayerPlatform()
    {
        transform.position = new Vector2(_playerPlatform.transform.position.x, _playerPlatform.transform.position.y + _playerPlatform.transform.localScale.y);

        if (Input.GetMouseButtonUp(0))
        {
            GetRandomStartedForce();
            _trail.gameObject.SetActive(true);
            _collider.isTrigger = false;
        }
    }

    void GetRandomStartedForce()
    {
        _rb.velocity = new Vector2(Random.Range(-100, 101), Random.Range(-100, 101)).normalized;
        UpdateVelocity(speed);

        _isStarted = true;
    }

    public void ZeroingTouches()
    {
        _touchWallCount = 0;
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == TagsNames.Wall)
        {
            _touchWallCount++;
            if (_touchWallCount > 5)
            {
                GetRandomStartedForce();
                ZeroingTouches();
            }
        }

        else
        {
            ZeroingTouches();
        }
    }
}
