using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField, Min(0)] int moveSpeed;

    int _currentSpeed;


    // Start is called before the first frame update
    void Start()
    {
        _currentSpeed = moveSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        MoveHorizontal();
    }

    void MoveHorizontal()
    {
        transform.position += new Vector3(Input.GetAxis(BDNames.Horizontal) * _currentSpeed * Time.deltaTime, 0);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == BDNames.Ball)
        {
            print(collision.gameObject.tag);
        }
        if (collision.gameObject.tag == BDNames.Wall)
        {
            print(collision.gameObject.tag);
            _currentSpeed = -moveSpeed;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == BDNames.Wall)
        {
            _currentSpeed = moveSpeed;
        }
    }
}
