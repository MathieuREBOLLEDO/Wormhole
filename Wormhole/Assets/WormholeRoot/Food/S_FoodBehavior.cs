using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_FoodBehavior :MonoBehaviour, IEatable
{
    [SerializeField]
    private float rotationSpeed;
    private Vector3 rotationAxis;

    public void Eat()
    {
        Destroy(gameObject);
    }

    void Start()
    {
        rotationAxis = transform.up;
    }

    void Update ()
    {
        transform.rotation *= Quaternion.AngleAxis(rotationSpeed,rotationAxis);
    }

    public void SetFood()
    {

    }
    private void SetColor()
    {

    }

    private void SetPoints()
    {

    }
}
