using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_CheckBounds : MonoBehaviour
{
    private Vector2 screenBounds;   
    [SerializeField] private S_CameraBoundaries cameraBounds;
    

    void Start()
    {    
        screenBounds = cameraBounds.GetCameraBorder(0.15f);
    }
/*
    void Update()
    {
        CheckForBounce();        
    }
*/
    private void OnDrawGizmos()
    {
        cameraBounds.DisplayGizmos(screenBounds, Color.red);
    }


    public void CheckForBounds()
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
