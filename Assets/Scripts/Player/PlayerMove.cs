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

    float _startPosY;

    #region Start Update
    void Start()
    {
        _startPosY = transform.position.y;
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
                Ball nearestBall = player.gameManager.balls[0];
                float minDistance = ((Vector2)(nearestBall.transform.position - transform.position)).magnitude;

                for (int i = 1; i < player.gameManager.balls.Count; i++)
                {
                    float tempDistance = ((Vector2)(player.gameManager.balls[i].transform.position - transform.position)).magnitude;
                    if(tempDistance < minDistance)
                    {
                        nearestBall = player.gameManager.balls[i];
                        minDistance = tempDistance;
                    }
                }

                Vector2 ballPos = nearestBall.transform.position;
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
