using UnityEngine;

public enum MovingType { Keyboard, Mouse, Ball }

public class PlayerMove : MonoBehaviour
{
    [Header("Связывающие данные")]
    [SerializeField] Player player;

    [Header("Данные по скрипту")]
    [SerializeField] public MovingType movingType;
    [SerializeField, Range(1, 10)] float moveSpeed;
    [SerializeField, Range(1, 10)] float minSpeed;

    public float leftBoard;
    public float rightBoard;

    Ball _ball;
    float _startPosY;

    #region Start Update
    void Start()
    {
        _startPosY = transform.position.y;

        if (movingType == MovingType.Ball)
            _ball = player.gameManager.balls[player.indexInGame];
    }

    void Update()
    {
        MoveHorizontal();
    }
    #endregion

    void MoveHorizontal()
    {
        Vector2 newPosition = Vector2.zero;

        switch (movingType)
        {
            case MovingType.Keyboard:
                Vector2 moveInput = new Vector2(Input.GetAxisRaw(AxesNames.Horizontal), 0);
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

    #region Speed
    /// <summary>
    /// Изменение скорости для бонуса
    /// </summary>
    /// <param name="value">На сколько изменится скорость</param>
    public void AddSpeed(float value)
    {
        SetSpeed(moveSpeed + value * moveSpeed / 10f);
    }

    public void SetSpeed(float value)
    {
        moveSpeed = value;
        if (moveSpeed < minSpeed) moveSpeed = minSpeed;
    }
    #endregion
}
