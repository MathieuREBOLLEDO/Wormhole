using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_HeadBehavior : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer == 8)
        {
            GetComponentInParent<S_SnakeBehavior>().CallDeath();
            Destroy(GetComponent<Collider>());
            Destroy(this);
        }
    }
}
