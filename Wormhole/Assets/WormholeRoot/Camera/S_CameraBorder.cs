using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_CameraBorder : MonoBehaviour
{
    private Camera mainCamera;
    private Vector2 screenBounds;

    private void Start()
    {
        
    }

    private void OnDrawGizmos()
    {
        mainCamera = Camera.main;
        CalculateCameraBorders();
        DisplayGizmos();
    }

    private void CalculateCameraBorders()
    {
        float aspectRatio = mainCamera.aspect;
        float fov = mainCamera.fieldOfView;

        float halfFov = fov * 0.5f;
        float halfHeight = Mathf.Tan(Mathf.Deg2Rad * halfFov) * 10;//nearClipPlane;
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
