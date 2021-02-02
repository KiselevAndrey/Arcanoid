using UnityEngine;

enum MovingType { Keyboard, Mouse, Ball }

public class PlayerMove : MonoBehaviour
{
    [SerializeField, Min(1)] int moveSpeed;
    [SerializeField, Min(1)] float borderX;
    [SerializeField] MovingType movingType;

    GameObject _ball;
    Rigidbody2D _rigidbody;
    int _currentSpeed;
    float _startPosY;

    // Start is called before the first frame update
    void Start()
    {
        _currentSpeed = moveSpeed;
        _startPosY = transform.position.y;

        _rigidbody = GetComponent<Rigidbody2D>();
        _ball = GameObject.FindGameObjectWithTag(BDNames.Ball);
    }

    // Update is called once per frame
    void Update()
    {
        MoveHorizontal();
    }

    void MoveHorizontal()
    {
        switch (movingType)
        {
            case MovingType.Keyboard:
                Vector2 moveInput = new Vector2(Input.GetAxisRaw(BDNames.Horizontal), 0);
                _rigidbody.MovePosition(_rigidbody.position + moveInput.normalized * moveSpeed * Time.deltaTime);
                break;

            case MovingType.Mouse:
                Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                mouseWorldPos.y = _startPosY;
                transform.position = mouseWorldPos;
                break;

            case MovingType.Ball:
                Vector2 ballPos = _ball.transform.position;
                ballPos.y = transform.position.y;
                transform.position = ballPos;
                break;

            default:
                break;
        }


        if (transform.position.x > borderX)
            transform.position = new Vector2(borderX, transform.position.y);
        else if(transform.position.x < -borderX)
            transform.position = new Vector2(-borderX, transform.position.y);
    }
}
