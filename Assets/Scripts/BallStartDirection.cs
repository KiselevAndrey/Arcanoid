using UnityEngine;

public class BallStartDirection : MonoBehaviour
{
    [SerializeField] int minX, maxX, y;

    int x;
    float z;
    bool upX, upY;

    Vector3 target;

    // Start is called before the first frame update
    void Start()
    {
        x = minX;
        z = transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        ChangeCoordinate(ref x, ref upX, minX, maxX);
        target = new Vector3(x, y, z).normalized + transform.position;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, target);
    }

    void ChangeCoordinate(ref int coord, ref bool isUp, int min, int max)
    {
        switch (isUp)
        {
            case true:
                coord++;
                break;
            case false:
                coord--;
                break;
        }

        if (coord > max || coord < min) isUp = !isUp;
    }
}
