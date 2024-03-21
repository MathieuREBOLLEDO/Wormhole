using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
//using UnityEngine.InputSystem.EnhancedTouch;

public class S_InputManager : MonoBehaviour
{
    [SerializeField] S_GetGameManager gameManager;
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
    private Vector2 screenInputLimits;
    [SerializeField]
    private S_CameraBoundaries cameraBoundaries;

    [Header("Portal")]
    [SerializeField]
    private S_GetPortalManager portalManager;
    
    private GameObject portalPlaced;
    private Vector3 portalPosition;

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
        playerControls = new PlayerControls();
    }

    private void Start()
    {
        screenBounds = cameraBoundaries.GetCameraBorder(-cameraBorderOffset);
        screenInputLimits = cameraBoundaries.GetCameraBorder(cameraBorderOffset);
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }
    private void OnDisable()
    {
        playerControls.Disable();
    }
    #endregion

    #region Touch
    public void StartTouchPrimary(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
        {
            Vector2 readValue = playerControls.Touch.PrimaryPosition.ReadValue<Vector2>();
            StartCoroutine(checkPos(ctx, readValue));
        }
    }

    public void EndTouchPrimary(InputAction.CallbackContext ctx)
    {
        if (ctx.canceled)
        {
            Vector3 position = ConvertScreenToWorld(playerControls.Touch.PrimaryPosition.ReadValue<Vector2>());
            SwipeEnd(position, (float)ctx.time);
            StopCoroutine(checkPos(ctx, new Vector2()));
        }
    }

    #endregion

    #region Coroutine
    private IEnumerator checkPos(InputAction.CallbackContext ctx, Vector2 touchInput)
    {
        if (touchInput == new Vector2())
        {
            yield return new WaitForEndOfFrame();
            touchInput = playerControls.Touch.PrimaryPosition.ReadValue<Vector2>();
        }

        portalPosition = ConvertScreenToWorld(touchInput);

        if (!gameManager.gameManager.isInMenu)
        {
            if (portalPosition.x >= -screenInputLimits.x &&
                portalPosition.x <= screenInputLimits.x &&
                portalPosition.y >= -screenInputLimits.y &&
                portalPosition.y <= screenInputLimits.y)

            {
                portalPosition.x = Mathf.Clamp(portalPosition.x, -screenBounds.x, screenBounds.x);
                portalPosition.y = Mathf.Clamp(portalPosition.y, -screenBounds.y, screenBounds.y);


                if (gameManager.gameManager.GetFirstInput())
                {
                    S_SnakeBehavior snake = FindObjectOfType<S_SnakeBehavior>();
                    Vector3 frontOfSnake = snake.transform.position + Vector3.up * 1f;
                    Debug.Log(frontOfSnake);
                    InitPortal(frontOfSnake, Quaternion.identity);
                    gameManager.gameManager.HideTips();
                }

                InitPortal(portalPosition, transform.rotation);

                SwipeStar(portalPosition, (float)ctx.startTime);
                yield return null;
            }
            else
            {
                Debug.Log("InputNotInBoard");
                yield return null;
            }
        }
    }

    private IEnumerator lookAtDirection()
    {
        while (true)
        {
            Vector2 currentPos = ConvertScreenToWorld(playerControls.Touch.PrimaryPosition.ReadValue<Vector2>());

            Vector3 direction = currentPos - startPosition;            

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);

            if (portalPlaced != null)
                portalPlaced.transform.rotation = rotation;

            yield return null;
        }
    }

    private IEnumerator Trail()
    {
        while (true)
        {
            trail.transform.position = ConvertScreenToWorld(playerControls.Touch.PrimaryPosition.ReadValue<Vector2>());
            yield return null;
        }
    }
    #endregion

    #region Swipe
    private void SwipeStar(Vector2 position, float time)
    {
        startPosition = position;
        startTime = time;

        //trail.SetActive(true);
        //trail.transform.position = position;
        //StartCoroutine(Trail());

        StartCoroutine(lookAtDirection());

        if (isDebug)
        {
            Debug.Log("<color=green> StartPosition : " + position + " || start time  : " + time + "</color>");
        }
    }

    private void SwipeEnd(Vector2 position, float time)
    {
        endPosition = position;
        endTime = time;

        //trail.SetActive(false);
        //StopCoroutine(Trail());

        StopCoroutine(lookAtDirection());

        if (isDebug)
        {
            Debug.Log("<color=blue> EndPosition : " + position + " || end time  : " + time + "</color>");
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

    #endregion

    void InitPortal(Vector3 position, Quaternion rotation)
    {
        GameObject tmpPortal = portalManager.myPortalManager.PlacePortal(position,rotation);

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