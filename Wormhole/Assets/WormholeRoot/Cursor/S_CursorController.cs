using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class S_CursorController : MonoBehaviour
{
    [SerializeField] private S_GetPortalManager portalManager;
    public GameObject portal;
    [SerializeField] public float scrollSpeed = 50f;

    [SerializeField] private SpriteRenderer [] renderer;

    void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,Input.mousePosition.y, 10f));
        transform.position = mousePos;

        if (Input.GetMouseButtonDown(0))
        {
            GameObject tmpPortal = GameObject.Instantiate(portal, transform.position, transform.rotation);
            portalManager.myPortalManager.AddPortalToList(tmpPortal);
            ChangeRenderer(false);
        }
        else
            ChangeRenderer(true);

        transform.rotation = Quaternion.AngleAxis(Input.GetAxis("Mouse ScrollWheel") * scrollSpeed, Vector3.forward)* transform.rotation;
    }

    void ChangeRenderer(bool isActivate)
    {
        foreach (var rend in renderer)
        {
            rend.color = new Color(
                rend.color.r,
                rend.color.g,
                rend.color.b,
                (isActivate ?  0.3f  : 0f)); 
        }
    }
}