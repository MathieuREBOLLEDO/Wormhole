using MoreMountains.Feedbacks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_SnakeBodyBehavior : MonoBehaviour
{
    private GameObject portalCollide;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
       DestroyPortals(collision.gameObject);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (portalCollide == null)
        {
            DestroyPortals(collision.gameObject);
            portalCollide = null;
        }
    }

    private void DestroyPortals(GameObject portal) 
    {
        if(portal.CompareTag("Portal"))
        {
            portalCollide = portal;
            GetComponentInParent<S_SnakeBehavior>().CallDestroyPortals(portal);
        }
    }
}

