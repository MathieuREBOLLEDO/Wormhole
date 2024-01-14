using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_CheckBounds : MonoBehaviour
{
    private Vector2 screenBounds;
    [SerializeField]private S_CameraBorder border;
    

    void Start()
    {    
        screenBounds = border.GetCameraBorder(0.5f);
    }

    void Update()
    {
        CheckForBounce();        
    }

    private void OnDrawGizmos()
    {
       border.DisplayGizmos(screenBounds, Color.red);
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
