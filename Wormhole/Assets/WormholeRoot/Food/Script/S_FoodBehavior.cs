using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_FoodBehavior : MonoBehaviour, IEatable
{
    [SerializeField] private S_FoodColorPalette colorPalette;
    [SerializeField] private S_FoodPoints listeOfPoints;

    [Header("Datas")]
    [SerializeField] private FoodType type;
    private int thisPoint;

    

    private Vector3 rotationAxis;
    [SerializeField]
    [Range(0, 20f)] private float amplitude = 15;
    [SerializeField]
    [Range(0f, 10f)] private float frequency = 5;
    private float angle;
    private float startingAngle;

    [SerializeField] private MeshRenderer thisRenderer;    


    void Start()
    {
        rotationAxis = transform.forward;
        startingAngle = Random.Range(-amplitude, amplitude)-90;
        transform.rotation = Quaternion.AngleAxis(startingAngle, rotationAxis);
        StartCoroutine(OscillateRotation());
    }

    private IEnumerator OscillateRotation()
    {
        while(true) 
        {            
            angle = Mathf.Sin((Time.time * frequency)+ Mathf.Deg2Rad*startingAngle ) * amplitude;

            transform.rotation *= Quaternion.AngleAxis( angle, rotationAxis);
            
            yield return null;
        }
    }

    void Update ()
    {
       // transform.rotation *= Quaternion.AngleAxis(rotationSpeed*Time.deltaTime,rotationAxis);
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

    public void Eat()
    {
        Destroy(gameObject);
    }

    public int GetPoint()
    {
        return thisPoint;
    }
}
