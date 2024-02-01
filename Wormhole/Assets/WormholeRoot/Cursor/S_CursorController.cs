using UnityEngine;
using UnityEngine.InputSystem;
//using UnityEngine.InputSystem.EnhancedTouch;


public class S_CursorController : MonoBehaviour
{
    [Header ("Input")]

    private PlayerInput playerInput;
    private InputAction touchPressAction;

    [Header("Camera")]
    [SerializeField] [Range (0f,1f)] private float cameraBorderOffset;
    private Vector2 screenBounds;
    [SerializeField] private S_CameraBoundaries cameraBoundaries;

    [Header ("Portal")]
    [SerializeField] private S_GetPortalManager portalManager;
    [SerializeField] private GameObject portal;



    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        touchPressAction = playerInput.actions["PlacePortal"];
    }

    private void Start()
    {
        screenBounds = cameraBoundaries.GetCameraBorder(-cameraBorderOffset);
    }   

    private void OnEnable()
    {
        touchPressAction.performed += PlacePortal;
    }
    private void OnDisable()
    {
        touchPressAction.performed -= PlacePortal;
    }



    public void PlacePortal(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            Vector2 readInput = touchPressAction.ReadValue<Vector2>();            

            Vector3 touchPosition = Camera.main.ScreenToWorldPoint(readInput); // convert screen pos to world pos
            touchPosition.z = 0;

            touchPosition.x = Mathf.Clamp(touchPosition.x, -screenBounds.x, screenBounds.x);
            touchPosition.y = Mathf.Clamp(touchPosition.y, -screenBounds.y, screenBounds.y);

            InitPortal(touchPosition, transform.rotation);
        }      
    }

    void InitPortal(Vector3 position, Quaternion rotation)
    {
        GameObject tmpPortal = GameObject.Instantiate(portal, position, rotation, portalManager.myPortalManager.transform);
        portalManager.myPortalManager.AddPortalToList(tmpPortal);
    }

    private void OnDrawGizmos()
    {
        cameraBoundaries.DisplayGizmos(screenBounds, Color.blue);
    }
}