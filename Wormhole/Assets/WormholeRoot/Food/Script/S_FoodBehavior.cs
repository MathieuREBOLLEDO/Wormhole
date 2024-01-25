using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_FoodBehavior :MonoBehaviour, IEatable
{
    [SerializeField] private S_FoodColorPalette colorPalette;
    [SerializeField] private S_FoodPoints listeOfPoints;

    [Header("Datas")]
    [SerializeField] private FoodType type;
    public int thisPoint;

    [SerializeField]
    [Range(0f, 360f)]
    private float rotationSpeed;
    private Vector3 rotationAxis;

    [SerializeField] private MeshRenderer thisRenderer;    



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
        SetColor();
        SetPoints();
    }
    private void SetColor()
    {
        thisRenderer.material.color = colorPalette.colors[(int)type];
    }

    private void SetPoints()
    {
        thisPoint = listeOfPoints.points[(int)type];
    }
}
