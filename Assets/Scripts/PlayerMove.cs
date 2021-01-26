using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField, Min(0)] int moveSpeed;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MoveHorizontal();
    }

    void MoveHorizontal()
    {
        transform.position += new Vector3(Input.GetAxis(BDNames.Horizontal) * moveSpeed * Time.deltaTime, 0);
    }
}
