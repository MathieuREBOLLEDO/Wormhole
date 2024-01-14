using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_BodyBehavior : MonoBehaviour
{
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Portal"))
        {
            GetComponentInParent<S_SnakeBehavior>().portalManager.myPortalManager.DestroyMultiplePortals(collision.gameObject);
        }
    }
    /*
    private void OnCollisionEnter(Collision collision)
    {        
        if (collision.gameObject.CompareTag("Portal"))
        {
            GetComponentInParent<S_SnakeBehavior>().portalManager.myPortalManager.DestroyMultiplePortals(collision.gameObject);
        }        
    }*/
}

