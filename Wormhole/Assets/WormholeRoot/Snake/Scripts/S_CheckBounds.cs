using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_CheckBounds : MonoBehaviour
{
    private Camera mainCamera;
    private Vector2 screenBounds;

    void Start()
    {
        mainCamera = Camera.main;
        CalculateCameraBorders();
        DisplayGizmos();
    }

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

    private void CalculateCameraBorders()
    {
        float aspectRatio = mainCamera.aspect;
        float fov = mainCamera.fieldOfView;

        float halfFov = fov * 0.5f;
        float halfHeight = Mathf.Tan(Mathf.Deg2Rad * halfFov) * 10 + 0.5f;//nearClipPlane;
        float halfWidth = halfHeight * aspectRatio + 0.5f;

        screenBounds = new Vector2(halfWidth, halfHeight);
    }

    void DisplayGizmos()
    {     
        float nearClipPlane = mainCamera.nearClipPlane;
        float farClipPlane = mainCamera.farClipPlane;

        // Get camera position and forward direction
        Vector3 cameraPosition = mainCamera.transform.position;
        Vector3 cameraForward = mainCamera.transform.forward;

        // Calculate frustum corners in world space
        Vector3 topLeft = cameraPosition + cameraForward * nearClipPlane - mainCamera.transform.right * screenBounds.x + mainCamera.transform.up * screenBounds.y;
        Vector3 topRight = cameraPosition + cameraForward * nearClipPlane + mainCamera.transform.right * screenBounds.x + mainCamera.transform.up * screenBounds.y;
        Vector3 bottomLeft = cameraPosition + cameraForward * nearClipPlane - mainCamera.transform.right * screenBounds.x - mainCamera.transform.up * screenBounds.y;
        Vector3 bottomRight = cameraPosition + cameraForward * nearClipPlane + mainCamera.transform.right * screenBounds.x - mainCamera.transform.up * screenBounds.y;

        // Draw lines to represent frustum borders
        Debug.DrawLine(topLeft, topRight, Color.red);
        Debug.DrawLine(topRight, bottomRight, Color.red);
        Debug.DrawLine(bottomRight, bottomLeft, Color.red);
        Debug.DrawLine(bottomLeft, topLeft, Color.red);

    }
}
