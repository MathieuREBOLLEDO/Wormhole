using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum PortalStates
{
    Hover,
    Unactivate,
    Activate,
    Destroy
}
public class S_CursorController : MonoBehaviour
{
    public GameObject portal;
    public float scrollSpeed = 50f;

    [SerializeField] private SpriteRenderer [] renderer;

    void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,Input.mousePosition.y, 10f));
        transform.position = mousePos;

        if (Input.GetMouseButtonDown(0))
        {
            GameObject.Instantiate(portal, transform.position, transform.rotation);
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