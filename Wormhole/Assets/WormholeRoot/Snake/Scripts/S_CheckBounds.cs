using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_CheckBounds : MonoBehaviour
{

    private Vector2 screenBounds;

    void Start()
    {
        float camHeight = Camera.main.orthographicSize;
        float camWidth = camHeight * Camera.main.aspect;
        screenBounds = new Vector2(camWidth, camHeight);
    }

    // Update is called once per frame
    void Update()
    {
        CheckForBounce();
    }

    private void CheckForBounce()
    {
        Vector3 newPosition = transform.position;

        if (newPosition.x < -screenBounds.x)
            newPosition.x = screenBounds.x;

        if(newPosition.x > screenBounds.x)
            newPosition.x = -screenBounds.x;

        if (newPosition.y < -screenBounds.y )
            newPosition.y = screenBounds.y;

        if( newPosition.y > screenBounds.y)
            newPosition.y = -screenBounds.y;

        transform.position = newPosition;
    }
}
