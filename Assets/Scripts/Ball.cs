using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    Vector3 _startPosition;
    Rigidbody2D _rigidbody;

    private void Start()
    {
        _startPosition = transform.position;
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    public void Reset()
    {
        _rigidbody.velocity = Vector3.zero;
        transform.position = _startPosition;
    }

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    print(collision.gameObject.tag);
    //    if (collision.gameObject.tag == BDNames.Wall)
    //        print(collision.gameObject.tag);
    //}
}
