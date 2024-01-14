using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FoodType
{
    Base,
    Rare,
    Legendary,
}

public class S_FoodManager : MonoBehaviour
{
    private Vector2 screenBounds;
    [SerializeField] private S_CameraBorder border;

    [SerializeField] private GameObject myFood;

    // Start is called before the first frame update
    void Start()
    {
        screenBounds = border.GetCameraBorder(-1.5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDrawGizmos()
    {
        border.DisplayGizmos(screenBounds, Color.black);
    }

    private void SpawFood()
    {
        Vector3 posToAppear = new Vector3(Random.Range(-screenBounds.x, screenBounds.x), Random.Range(-screenBounds.y, screenBounds.y), 0); ;
        
        GameObject food = Instantiate(myFood, posToAppear,Quaternion.AngleAxis(Random.Range(0,360f),Vector3.up));
        food.GetComponent<S_FoodBehavior>().SetFood();

    }

    
}
