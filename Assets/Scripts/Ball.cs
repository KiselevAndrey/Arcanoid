using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] PlayerMove playerPlatform;
    [SerializeField, Min(1)] float startedSpeed;
    [SerializeField] PlayerManager player;

    Rigidbody2D _rb;
    bool _isStarted;
    int _touchWallCount, _touchPlayerCount;
    float _minVelocity, _maxVelocity;
    float _currentMinVelocity, _currentMaxVelocity;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _minVelocity = 10;
        _maxVelocity = 15;
    }

    public void Zeroing()
    {
        _rb.velocity *= 0f;
        _isStarted = false;
        ZeroingTouches();
    }

    void ZeroingTouches()
    {
        _touchWallCount = 0;
        _touchPlayerCount = 0;
    }
    
    private void Update()
    {
        switch (_isStarted)
        {
            case true:

                UpdateVelocity();

                break;

            case false:

                PasteToPlayerPlatform();

                break;
        }
    }

    void UpdateVelocity()
    {
        if (_rb.velocity.magnitude < _currentMinVelocity) _rb.velocity *= 1.2f;
        else if (_rb.velocity.magnitude > _currentMaxVelocity) _rb.velocity *= 0.8f;
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
                ZeroingTouches();
            }
        }

        else if (collision.gameObject.tag == BDNames.Respawn)
        {
            player.Hit();
            Zeroing();
        }

        else if(collision.gameObject.tag == BDNames.Player)
        {
            _touchPlayerCount++;
            if(_touchPlayerCount > 3)
            {
                GetRandomStartedForce();
                _touchPlayerCount = 0;
            }
            _touchWallCount = 0;
        }
        else
        {
            ZeroingTouches();
        }
    }

    public void ChangeVelocity(int difficultLvl)
    {
        _currentMinVelocity = _minVelocity / 2 + difficultLvl - 1;
        _currentMaxVelocity = _maxVelocity / 2 + difficultLvl - 1;

        if (_currentMinVelocity > _minVelocity) _currentMinVelocity = _minVelocity;
        if (_currentMaxVelocity > _maxVelocity) _currentMaxVelocity = _maxVelocity;
    }

}
