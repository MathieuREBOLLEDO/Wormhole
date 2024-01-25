using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class S_CursorController : MonoBehaviour
{
    [SerializeField] private S_GetPortalManager portalManager;
    [SerializeField] private GameObject portal;
    [SerializeField] public float scrollSpeed = 50f;

    private InputAction touchPressAction;

    private Vector2 screenBounds;
    [SerializeField] private S_CameraBoundaries cameraBoundaries;

    [SerializeField] private SpriteRenderer [] thisRenderer;

    void Start()
    {
        screenBounds = cameraBoundaries.GetCameraBorder(-0.5f);
    }

    void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,Input.mousePosition.y, 10f));
        transform.position = mousePos;
        
        if (Input.GetMouseButtonDown(0) )
        {
            GameObject tmpPortal = GameObject.Instantiate(portal, transform.position, transform.rotation,portalManager.myPortalManager.transform);
            portalManager.myPortalManager.AddPortalToList(tmpPortal);
            //ChangeRenderer(false);
        }
        //else
            //ChangeRenderer(true);
        
        transform.rotation = Quaternion.AngleAxis(Input.GetAxis("Mouse ScrollWheel") * scrollSpeed, Vector3.forward)* transform.rotation;
    }

    private void OnEnable()
    {
        touchPressAction.performed += TouchPressed; 
    }

    private void OnDisable()
    {
        
    }

    private void TouchPressed(InputAction.CallbackContext context) 
    { }

    void ChangeRenderer(bool isActivate)
    {
        foreach (var rend in thisRenderer)
        {
            rend.color = new Color(
                rend.color.r,
                rend.color.g,
                rend.color.b,
                (isActivate ?  0.3f  : 0f)); 
        }
    }

    private void OnDrawGizmos()
    {
        cameraBoundaries.DisplayGizmos(screenBounds, Color.blue);
    }
}