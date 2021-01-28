using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] PlayerMove playerPlatform;
    [SerializeField, Min(1)] float startedSpeed;

    Rigidbody2D _rb;
    bool _isStarted;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    public void Reset()
    {
        _isStarted = false;
    }
    
    private void Update()
    {
        switch (_isStarted)
        {
            case true:

                break;

            case false:
                PasteToPlayerPlatform();
                break;
        }
    }

    void PasteToPlayerPlatform()
    {
        transform.position = new Vector2(playerPlatform.transform.position.x, playerPlatform.transform.position.y + playerPlatform.transform.localScale.y);

        if (Input.GetMouseButtonUp(0))
        {
            GetRandomStartedForce();
            _isStarted = true;
        }
    }

    void GetRandomStartedForce()
    {
        Vector2 force = new Vector2(Random.Range(-100, 101), Random.Range(20, 101)).normalized;
        _rb.AddForce(force * startedSpeed);
    }
}
