using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_CameraBorder : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    public Vector2 GetCameraBorder(float offset)
    {
        float aspectRatio = mainCamera.aspect;
        float fov = mainCamera.fieldOfView;

        float halfFov = fov * 0.5f;
        float halfHeight = Mathf.Tan(Mathf.Deg2Rad * halfFov) * 10 + offset;//nearClipPlane;
        float halfWidth = halfHeight * aspectRatio + 0.5f;

       // screenBounds = new Vector2(halfWidth, halfHeight);

        return new Vector2(halfWidth, halfHeight); 
    }

    public void DisplayGizmos(Vector2 screenBoundaries, Color color)
    {
        float nearClipPlane = mainCamera.nearClipPlane;
        float farClipPlane = mainCamera.farClipPlane;

        // Get camera position and forward direction
        Vector3 cameraPosition = mainCamera.transform.position;
        Vector3 cameraForward = mainCamera.transform.forward;

        // Calculate frustum corners in world space
        Vector3 topLeft = cameraPosition + cameraForward * nearClipPlane - mainCamera.transform.right * screenBoundaries.x + mainCamera.transform.up * screenBoundaries.y;
        Vector3 topRight = cameraPosition + cameraForward * nearClipPlane + mainCamera.transform.right * screenBoundaries.x + mainCamera.transform.up * screenBoundaries.y;
        Vector3 bottomLeft = cameraPosition + cameraForward * nearClipPlane - mainCamera.transform.right * screenBoundaries.x - mainCamera.transform.up * screenBoundaries.y;
        Vector3 bottomRight = cameraPosition + cameraForward * nearClipPlane + mainCamera.transform.right * screenBoundaries.x - mainCamera.transform.up * screenBoundaries.y;

        // Draw lines to represent frustum borders
        Debug.DrawLine(topLeft, topRight, color);
        Debug.DrawLine(topRight, bottomRight, color);
        Debug.DrawLine(bottomRight, bottomLeft, color);
        Debug.DrawLine(bottomLeft, topLeft, color);
    }
}
