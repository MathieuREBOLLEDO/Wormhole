using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_BaseSnakeMovement : MonoBehaviour
{
    private Vector3 direction;
    public float speed = 5f;
    void Update()
    {
        direction = Vector3.right * speed;
        transform.position += direction;   
    }
}
