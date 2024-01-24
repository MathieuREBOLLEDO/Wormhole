using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_FoodBehavior :MonoBehaviour, IEatable
{
    private FoodType type;

    [SerializeField]
    [Range(0f, 360f)]
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
        transform.rotation *= Quaternion.AngleAxis(rotationSpeed*Time.deltaTime,rotationAxis);
    }

    public void SetFood( FoodType newType)
    {
        type = newType;
    }
    private void SetColor()
    {
        switch (type)
        {
            case FoodType.Common:
                
                break;

            case FoodType.Rare:

                break;

            case FoodType.Legendary: 
                
                break;
        }
    }

    private void SetPoints()
    {
        switch (type)
        {
            case FoodType.Common:

                break;

            case FoodType.Rare:

                break;

            case FoodType.Legendary:

                break;
        }
    }
}
