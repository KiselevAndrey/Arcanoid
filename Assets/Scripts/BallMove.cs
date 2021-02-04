using UnityEngine;

public class BallMove : MonoBehaviour
{
    [SerializeField, Range(1, 10)] float speed;

    Rigidbody2D _rb;
    PlayerMove _playerPlatform;
    TrailRenderer _trail;
    bool _isStarted;
    int _touchWallCount;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _playerPlatform = GameObject.FindGameObjectWithTag(BDNames.Player).GetComponent<PlayerMove>();
        _trail = GetComponentInChildren<TrailRenderer>();
    }

    private void Update()
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
    }

    void PasteToPlayerPlatform()
    {
        transform.position = new Vector2(_playerPlatform.transform.position.x, _playerPlatform.transform.position.y + (_playerPlatform.transform.localScale.y + transform.localScale.y)/2);

        if (Input.GetMouseButtonUp(0))
        {
            GetRandomStartedForce();
            _trail.gameObject.SetActive(true);
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
        if (collision.gameObject.tag == BDNames.Wall)
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
