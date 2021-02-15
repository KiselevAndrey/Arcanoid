using UnityEngine;

public class BallStartDirection : MonoBehaviour
{
    [SerializeField, Min(1)] int diffX;
    [SerializeField, Min(1)] int speed;
    [HideInInspector] public bool drawGizmo = true;

    int x, y;
    float z;
    bool upX;

    Vector3 target;

    void Start()
    {
        x = -diffX;
        z = transform.position.z;
    }

    void Update()
    {
        if (drawGizmo)
        {
            ChangeCoordinate();
            target = new Vector3(x, y, z).normalized + transform.position;
        }
        else
            target = transform.position;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, target);
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
