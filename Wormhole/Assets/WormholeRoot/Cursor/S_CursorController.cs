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
    private GameObject portal;


    int indexDebug = 0;
    // #region Events
    // public delegate void StartTouch(Vector2 position, float time);
    // public event StartTouch OnStartTouch;
    //
    // public delegate void EndTouch(Vector2 position, float time);
    // public event StartTouch OnEndTouch;
    // #endregion

    private void Awake()
    {
        mainCamera = Camera.main;
        playerControls = new PlayerControls();
        //playerInput = GetComponent<PlayerInput>();
        //touchPressAction = playerInput.actions["PlacePortal"];
    }

    private void Start()
    {
        screenBounds = cameraBoundaries.GetCameraBorder(-cameraBorderOffset);

        //playerControls.Touch.PrimaryContact.started += ctx => StartTouchPrimary(ctx);
        //playerControls.Touch.PrimaryContact.started += ctx => EndTouchPrimary(ctx);
    }

    private void OnEnable()
    {
        //playerControls.Enable();

        playerControls.Touch.PrimaryContact.performed += StartTouchPrimary;
        playerControls.Touch.PrimaryContact.performed += EndTouchPrimary;
        //touchPressAction.performed += PlacePortal;
    }
    private void OnDisable()
    {
        //playerControls.Disable();

        playerControls.Touch.PrimaryContact.started -= StartTouchPrimary;
        playerControls.Touch.PrimaryContact.started -= EndTouchPrimary;
        //touchPressAction.performed -= PlacePortal;
    }


    public void StartTouchPrimary(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
        {
            Vector3 position = ConvertScreenToWorld(playerControls.Touch.PrimaryPosition.ReadValue<Vector2>());

            position.x = Mathf.Clamp(position.x, -screenBounds.x, screenBounds.x);
            position.y = Mathf.Clamp(position.y, -screenBounds.y, screenBounds.y);

            InitPortal(position, transform.rotation);

            Debug.Log("Place a Portal" + indexDebug);
            indexDebug++;

            SwipeStar(position, (float)ctx.startTime);

        }

        if (ctx.performed)
        {
            Debug.Log("Call");
        }
    }

    public void EndTouchPrimary(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
        {
            Vector3 position = ConvertScreenToWorld(playerControls.Touch.PrimaryPosition.ReadValue<Vector2>());
            SwipeEnd(position, (float)ctx.time);
        }
    }

    private void SwipeStar(Vector2 position, float time)
    {
        startPosition = position;
        startTime = time;
    }

    private void SwipeEnd(Vector2 position, float time)
    {
        endPosition = position;
        endTime = time;
        DetectSwipe();
    }

    private void DetectSwipe()
    {
        if (Vector3.Distance(startPosition, endPosition) >= minimunDistance
            && (endTime - startTime) <= maximunTime)
        {
            Debug.Log("Swipe Detected");
            Debug.DrawLine(startPosition, endPosition, Color.red, 5f);

            Vector3 direction = endPosition - startPosition;
            Vector2 direction2D = new Vector2(direction.x, direction.y).normalized;
        }
    }
    //  private void PlacePortal(InputAction.CallbackContext context)
    //  {
    //      if (context.started)
    //      {
    //          Vector2 readInput = touchPressAction.ReadValue<Vector2>();
    //
    //          Vector3 touchPosition = ConvertScreenToWorld(readInput);
    //
    //          touchPosition.x = Mathf.Clamp(touchPosition.x, -screenBounds.x, screenBounds.x);
    //          touchPosition.y = Mathf.Clamp(touchPosition.y, -screenBounds.y, screenBounds.y);
    //
    //          InitPortal(touchPosition, transform.rotation);
    //      }      
    //  }

    void InitPortal(Vector3 position, Quaternion rotation)
    {
        GameObject tmpPortal = GameObject.Instantiate(portal, position, rotation, portalManager.myPortalManager.transform);
        portalManager.myPortalManager.AddPortalToList(tmpPortal);
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