using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_CursorController : MonoBehaviour
{
    void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,Input.mousePosition.y, 10f));
        transform.position = mousePos;
    }
}