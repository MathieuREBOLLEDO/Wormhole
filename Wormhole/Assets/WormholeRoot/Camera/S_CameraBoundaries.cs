using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CameraType
{
    Orthographic,
    Perspective,
}


[CreateAssetMenu(fileName ="SO_CameraBoundaries", menuName = "Level/CameraBoundaries", order = 1)]
public class S_CameraBoundaries : ScriptableObject
{
    [SerializeField] private CameraType cameraType;
    [SerializeField] private Camera mainCamera;
    public Vector2 GetCameraBorder(float offset)
    {
        float halfHeight, halfWidth;

        if( cameraType == CameraType.Orthographic)
        {
             halfHeight = Camera.main.orthographicSize + offset;
             halfWidth = halfHeight * Camera.main.aspect + (offset/2);
        }
        else
        {
            float aspectRatio = mainCamera.aspect;
            float fov = mainCamera.fieldOfView;

            float halfFov = fov * 0.5f;
            halfHeight = Mathf.Tan(Mathf.Deg2Rad * halfFov) * 10 + offset;//nearClipPlane;
            halfWidth = halfHeight * aspectRatio + 0.5f;
        }        

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

