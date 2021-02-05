using UnityEngine;

enum MovingType { Keyboard, Mouse, Ball }

public class PlayerMove : MonoBehaviour
{
    [SerializeField] MovingType movingType;
    [SerializeField, Range(1, 10)] float moveSpeed;

    public float leftBoard;
    public float rightBoard;

    GameObject _ball;
    float _startPosY;

    // Start is called before the first frame update
    void Start()
    {
        _startPosY = transform.position.y;

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
                newPosition = new Vector2(transform.position.x, transform.position.y) + moveInput;
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

        transform.position = Vector2.MoveTowards(transform.position, newPosition, moveSpeed * Time.deltaTime);
        
    }
}
