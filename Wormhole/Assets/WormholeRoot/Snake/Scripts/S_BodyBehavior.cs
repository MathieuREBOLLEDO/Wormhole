using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_BodyBehavior : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Portal"))
        {
            GetComponentInParent<S_SnakeBehavior>().portalManager.myPortalManager.DestroyMultiplePortals(collision.gameObject);
        }
    }
}

