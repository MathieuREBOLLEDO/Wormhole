using UnityEngine;
using UnityEngine.InputSystem;
//using UnityEngine.InputSystem.EnhancedTouch;


public class S_CursorController : MonoBehaviour
{
    [SerializeField] private S_GetPortalManager portalManager;
    [SerializeField] private GameObject portal;
    [SerializeField] public float scrollSpeed = 50f;

    private PlayerInput playerInput;


    private InputAction touchPressAction;

    [Header ("Camera")]
    private Vector2 screenBounds;
    [SerializeField] private S_CameraBoundaries cameraBoundaries;



    private void Awake()
    {

        playerInput = GetComponent<PlayerInput>();
        touchPressAction = playerInput.actions["PlacePortal"];

    }

    private void Start()
    {
        screenBounds = cameraBoundaries.GetCameraBorder(-0.15f);
    }


    //  void Update()
    //  {
    //   //   Vector3 mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,Input.mousePosition.y, 10f));
    //   //   transform.position = mousePos;
    //   //   
    //   //   if (Input.GetMouseButtonDown(0) )
    //   //   {
    //   //       GameObject tmpPortal = GameObject.Instantiate(portal, transform.position, transform.rotation,portalManager.myPortalManager.transform);
    //   //       portalManager.myPortalManager.AddPortalToList(tmpPortal);
    //   //       //ChangeRenderer(false);
    //   //   }
    //   //   //else
    //   //       //ChangeRenderer(true);
    //   //   
    //   //   transform.rotation = Quaternion.AngleAxis(Input.GetAxis("Mouse ScrollWheel") * scrollSpeed, Vector3.forward)* transform.rotation;
    //  }

    //  private void OnEnable()
    // {
    //     touchPressAction.Enable();
    //     touchPressAction.performed += TouchPressed;
    //     Debug.Log("Listenning");
    // }
    // 
    // private void OnDisable()
    // {
    //     touchPressAction.Disable();
    //     touchPressAction.performed -= TouchPressed;
    //      //touchPressAction.started -= TouchPressed;
    //     Debug.Log("Mute");
    // }

    private void OnEnable()
    {
        touchPressAction.performed += PlacePortalTest;
    }
    private void OnDisable()
    {
        touchPressAction.performed -= PlacePortalTest;
    }



    public void PlacePortalTest(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            Vector2 readInput = touchPressAction.ReadValue<Vector2>();            

            Vector3 touchPosition = Camera.main.ScreenToWorldPoint(readInput); // convert screen pos to world pos
            touchPosition.z = 0;

            touchPosition.x = Mathf.Clamp(touchPosition.x, -screenBounds.x, screenBounds.x);
            touchPosition.y = Mathf.Clamp(touchPosition.y, -screenBounds.y, screenBounds.y);

            PlacePortal(touchPosition, transform.rotation);
        }      
    }

    void PlacePortal(Vector3 position, Quaternion rotation)
    {
        GameObject tmpPortal = GameObject.Instantiate(portal, position, rotation, portalManager.myPortalManager.transform);
        portalManager.myPortalManager.AddPortalToList(tmpPortal);
    }

    void ChangeRenderer(bool isActivate)
    {
        //foreach (var rend in thisRenderer)
        //{
        //    rend.color = new Color(
        //        rend.color.r,
        //        rend.color.g,
        //        rend.color.b,
        //        (isActivate ?  0.3f  : 0f)); 
        //}
    }

    private void OnDrawGizmos()
    {
        cameraBoundaries.DisplayGizmos(screenBounds, Color.blue);
    }
}