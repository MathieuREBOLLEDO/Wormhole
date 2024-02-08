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
    private float timeBeforeGoingToCommon;
    float initFlickeringTime = 0.5f;
    float flickeringTime;
    bool isFlickering = false;
    bool isVisible=true;

    [SerializeField] private MeshRenderer thisRenderer;    

    void Awake()
    {
        SetFood((FoodType)Random.Range(0,3));
        //SetFood(type);
    }

    void Start()
    {
        rotationAxis = transform.forward;
        startingAngle = Random.Range(-amplitude, amplitude)-90;
        transform.rotation = Quaternion.AngleAxis(startingAngle, rotationAxis);
        StartCoroutine(OscillateRotation());

        if (type != FoodType.Common)
        {
            StartCoroutine( SetTimerBeforeReset());
        }
    }

    private IEnumerator SetTimerBeforeReset()
    {
        float tmpTime = timeBeforeGoingToCommon - 3f;
        yield return new WaitForSeconds(tmpTime);
        StartCoroutine(VisibilityToggleTimer());


        yield return new WaitForSeconds(2f);
        flickeringTime = 0.15f;


        yield return new WaitForSeconds(1f);
        //type = FoodType.Common;
        SetFood(FoodType.Common);
        isFlickering = false;        
        
    }

    private IEnumerator VisibilityToggleTimer()
    {
        isFlickering = true;
        flickeringTime = initFlickeringTime;
        while (isFlickering)
        {
            isVisible = !isVisible;
            //SetFoodTransparency(isVisible);
            thisRenderer.enabled = isVisible;
            yield return new WaitForSeconds(flickeringTime);
        }
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

    public void SetFood( FoodType newType)
    {
        StopCoroutine(VisibilityToggleTimer());
        StopCoroutine(SetTimerBeforeReset());
        
        type = newType;
        SetColor();
        SetPoints();
        isVisible = true;
        thisRenderer.enabled = isVisible;
        //SetFoodTransparency(isVisible);

        if (type != FoodType.Common)
            SetTime();
        
    }

    private void SetTime()
    {
        switch(type)
        {
            case FoodType.Rare:
                timeBeforeGoingToCommon = 12f;
                break;

            case FoodType.Legendary:
                timeBeforeGoingToCommon = 7f;
                break;
        }
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

    private void SetFoodTransparency(bool disable)
    {
        int index = 0;
        foreach( Material mat in thisRenderer.materials )
        {
            Color objectColor = thisRenderer.materials[index].color;
            thisRenderer.materials[index].color = disable ?
                new Color(objectColor.r, objectColor.g, objectColor.b, 255) :
                new Color(objectColor.r, objectColor.g, objectColor.b, 125);
            index++;
        }
    }

}
