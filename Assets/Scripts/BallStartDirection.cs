using UnityEngine;

public class BallStartDirection : MonoBehaviour
{
    int minX, maxX, minY, maxY;

    int x, y;
    bool upX, upY;

    Vector3 target;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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

        if (coord >= max || coord <= min) isUp = !isUp;
    }
}
