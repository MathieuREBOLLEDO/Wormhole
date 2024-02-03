using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
//using UnityEngine.InputSystem.EnhancedTouch;

public class S_CursorController : MonoBehaviour
{
    private PlayerControls playerControls;
    //private PlayerInput playerInput;
    //private InputAction touchPressAction;*

    [SerializeField]
    private float minimunDistance = 0.2f;
    [SerializeField]
    private float maximunTime = 1.0f;

    private Vector2 startPosition;
    private Vector2 endPosition;
    private float startTime;
    private float endTime;

    [Header("Camera")]
    [SerializeField]
    [Range(0f, 1f)]
    private float cameraBorderOffset = 0.15f;

    private Camera mainCamera;

    private Vector2 screenBounds;
    [SerializeField]
    private S_CameraBoundaries cameraBoundaries;

    [Header("Portal")]
    [SerializeField]
    private S_GetPortalManager portalManager;
    [SerializeField]
    private GameObject portalPrefab;
    private GameObject portalPlaced;

    [Header("Trail")]
    [SerializeField]
    private GameObject trail;

    [Header("Debug")]
    [SerializeField]
    private bool isDebug = false;
    int indexDebug = 1;

    #region Init
    private void Awake()
    {
        mainCamera = Camera.main;
        Debug.Log(mainCamera.name);
        playerControls = new PlayerControls();
    }

    private void Start()
    {
        screenBounds = cameraBoundaries.GetCameraBorder(-cameraBorderOffset);
    }

    private void OnEnable()
    {
        playerControls.Enable();
       // playerControls.Touch.PrimaryContact.started += StartTouchPrimary;
       // playerControls.Touch.PrimaryContact.started += EndTouchPrimary;
    }
    private void OnDisable()
    {
        playerControls.Disable();
       // playerControls.Touch.PrimaryContact.started -= StartTouchPrimary;
       // playerControls.Touch.PrimaryContact.started -= EndTouchPrimary;
    }
    #endregion

    public void StartTouchPrimary(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
        {
            Vector2 readValue = playerControls.Touch.PrimaryPosition.ReadValue<Vector2>();
            Debug.Log(readValue);
            Vector3 position = ConvertScreenToWorld(readValue);
            Debug.Log(position);

            //position.x = Mathf.Clamp(position.x, -screenBounds.x, screenBounds.x);
            //position.y = Mathf.Clamp(position.y, -screenBounds.y, screenBounds.y);

            InitPortal(position, transform.rotation);

            SwipeStar(position, (float)ctx.startTime);
        }
    }

    public void EndTouchPrimary(InputAction.CallbackContext ctx)
    {
        if (ctx.canceled)
        {
            Vector3 position = ConvertScreenToWorld(playerControls.Touch.PrimaryPosition.ReadValue<Vector2>());
            SwipeEnd(position, (float)ctx.time);
        }
    }

    private void SwipeStar(Vector2 position, float time)
    {
        startPosition = position;
        startTime = time;

        trail.SetActive(true);
        trail.transform.position = position;
        StartCoroutine(Trail());

        if (isDebug)
        {
            Debug.Log("<color=green> StartPosition : " + position + " || start time  : " + time + "</color>");
        }
    }

    private IEnumerator Trail()
    {
         while (true)
         {
             trail.transform.position = ConvertScreenToWorld( playerControls.Touch.PrimaryPosition.ReadValue<Vector2>());
             yield return null;
         }        
    }

    private void SwipeEnd(Vector2 position, float time)
    {
        endPosition = position;
        endTime = time;

        trail.SetActive(false);
        StopCoroutine(Trail());

        if (isDebug)
        {
            Debug.Log("<color=red> EndPosition : " + position + " || end time  : " + time + "</color>");
        }

        DetectSwipe();
    }

    private void DetectSwipe()
    {
        if (Vector3.Distance(startPosition, endPosition) >= minimunDistance
            && (endTime - startTime) <= maximunTime)
        {
            if (isDebug)
            {
                Debug.Log("Swipe Detected");
                Debug.DrawLine(startPosition, endPosition, Color.red, 5f);
            }

            Vector3 direction = endPosition - startPosition;
            Vector2 direction2D = new Vector2(direction.x, direction.y).normalized;
        }
    }

    void InitPortal(Vector3 position, Quaternion rotation)
    {

        GameObject tmpPortal = GameObject.Instantiate(portalPrefab, position, rotation, portalManager.myPortalManager.transform);
        portalManager.myPortalManager.AddPortalToList(tmpPortal);

        if (isDebug)
        {
            Debug.Log("<color=purple>Place a Portal n°" + indexDebug + "  || Id " + portalManager.myPortalManager.GetPortalId(tmpPortal) + "</Color>");
            indexDebug++;
        }

        portalPlaced = tmpPortal;
    }



    #region Utils

    Vector3 ConvertScreenToWorld(Vector2 input)
    {
        Vector3 worldPos = mainCamera.ScreenToWorldPoint(input); // convert screen pos to world pos
        worldPos.z = 0;

        return worldPos;
    }

    private void OnDrawGizmos()
    {
        cameraBoundaries.DisplayGizmos(screenBounds, Color.blue);
    }
    #endregion
}