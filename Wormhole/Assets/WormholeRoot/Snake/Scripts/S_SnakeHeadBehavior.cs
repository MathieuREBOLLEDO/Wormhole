using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_SnakeHeadBehavior : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {      
        if (collision.gameObject.layer == 8)
        {
            GetComponentInParent<S_SnakeBehavior>().CallDeath();
            Destroy(GetComponent<Collider>());
            Destroy(this);
        }        
    }
}
