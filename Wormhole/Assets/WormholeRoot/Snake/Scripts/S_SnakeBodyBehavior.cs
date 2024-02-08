using MoreMountains.Feedbacks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_SnakeBodyBehavior : MonoBehaviour
{
    GameObject snakeCore;
    //private GameObject portalCollide;

    private void Start()
    {
        snakeCore = transform.parent.transform.parent.gameObject;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
       DestroyPortals(collision.gameObject);
    }

    private void DestroyPortals(GameObject portal) 
    {
        if(portal.CompareTag("PortalDetection"))
        {
            portal = portal.transform.parent.gameObject;            
            snakeCore.GetComponentInParent<S_SnakeBehavior>().CallDestroyPortals(portal);
        }
    }
}

