using UnityEngine;

enum MovingType { Keyboard, Mouse, Ball }

public class PlayerMove : MonoBehaviour
{
    [SerializeField] MovingType movingType;
    [SerializeField, Min(1)] int moveSpeed;

    public float leftBoard;
    public float rightBoard;

    GameObject _ball;
    Rigidbody2D _rigidbody;
    float _startPosY;

    // Start is called before the first frame update
    void Start()
    {
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
        Vector2 newPosition = Vector2.zero;

        switch (movingType)
        {
            case MovingType.Keyboard:
                Vector2 moveInput = new Vector2(Input.GetAxisRaw(BDNames.Horizontal), 0);
                newPosition = _rigidbody.position + moveInput;
                break;

            case MovingType.Mouse:
                Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                mouseWorldPos.y = _startPosY;
                newPosition = mouseWorldPos;
                break;

            case MovingType.Ball:
                Vector2 ballPos = _ball.transform.position;
                ballPos.y = _startPosY;
                newPosition = ballPos;
                break;
        }

        newPosition.x = Mathf.Clamp(newPosition.x, leftBoard, rightBoard);

        transform.position = Vector2.Lerp(transform.position, newPosition, moveSpeed * Time.deltaTime);
    }
}
