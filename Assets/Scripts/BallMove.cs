using UnityEngine;

public class BallMove : MonoBehaviour
{
    [Header("Связывающие данные")]
    [SerializeField] Ball ball;

    [SerializeField, Range(1, 10)] float speed;

    Rigidbody2D _rb;
    Collider2D _collider;
    TrailRenderer _trail;

    bool _isStarted;
    int _touchWallCount;

    #region Awake Update
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _collider = GetComponent<Collider2D>();
        _trail = GetComponentInChildren<TrailRenderer>();
        //_trail.colorGradient.colorKeys[0].color
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
    #endregion
    
    #region Zeroing
    public void Zeroing()
    {
        UpdateVelocity(0);
        ZeroingTouches();
        StartBall(false);

        _isStarted = false;
    }

    public void ZeroingTouches() => _touchWallCount = 0;
    #endregion

    #region Move
    void UpdateVelocity(float speedMultiply) => _rb.velocity = _rb.velocity.normalized * speedMultiply;

    void PasteToPlayerPlatform()
    {
        transform.position = new Vector2(ball.player.move.transform.position.x, ball.player.move.transform.position.y + ball.player.move.transform.localScale.y);

        if (Input.GetMouseButtonUp(0))
        {
            GetRandomStartedForce();
            StartBall(true);
        }
    }
    #endregion

    void StartBall(bool value)
    {
        _trail.gameObject.SetActive(value);
        _collider.isTrigger = !value;
    }

    void GetRandomStartedForce()
    {
        _rb.velocity = new Vector2(Random.Range(-100, 101), Random.Range(-100, 101)).normalized;
        UpdateVelocity(speed);

        _isStarted = true;
    }

    /// <summary>
    /// Удар об стену
    /// </summary>
    public void HitWall()
    {
        _touchWallCount++;
        if (_touchWallCount > 5)
        {
            GetRandomStartedForce();
            ZeroingTouches();
        }
    }
}
