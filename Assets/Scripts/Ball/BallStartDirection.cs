using UnityEngine;

public class BallStartDirection : MonoBehaviour
{
    public LineRenderer startDirectionLine;

    [SerializeField, Min(1)] int diffX;
    [SerializeField, Min(1)] int speed;
    

    int x, y;
    float z;
    bool upX;

    Vector3 target;

    void Start()
    {
        x = -diffX;
        z = transform.position.z;
        target = new Vector3(x, y, z).normalized + transform.position;
    }

    void Update()
    {
        if (Time.timeScale != 0)
        {
            ChangeCoordinate();
            target = new Vector3(x, y, z).normalized + transform.position;

            startDirectionLine.SetPosition(0, new Vector3(0, 0, z + 1));
            startDirectionLine.SetPosition(1, new Vector3(x, y, z + 1).normalized);
        }
       
    }

   void ChangeCoordinate()
    {
        switch (upX)
        {
            case true:
                x += speed;
                break;
            case false:
                x -= speed;
                break;
        }
        if (x > diffX || x < -diffX) upX = !upX;

        y = Mathf.Abs(Mathf.Abs(x) - diffX);
    }

    public Vector3 GetForseDirection() => target;
}
