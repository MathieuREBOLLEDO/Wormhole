using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class S_FoodManager : MonoBehaviour
{
    private Vector2 screenBounds;
    [SerializeField] private S_CameraBoundaries cameraBoundaries;

    [SerializeField] private GameObject myFood;

    [Range(-1.5f, 0f)]
    [SerializeField] float distanceFromBorder;

    void Start()
    {
        screenBounds = cameraBoundaries.GetCameraBorder(distanceFromBorder);
        StartCoroutine(CallSpawnRandomly());
    }


    private void OnDrawGizmos()
    {
        cameraBoundaries.DisplayGizmos(screenBounds, Color.black);
    }

    private IEnumerator CallSpawnRandomly()
    {
        while (true)
        {
            SpawFood();
            float randomTime = Random.Range(0.5f, 2.5f); // Change the range as needed
            yield return new WaitForSeconds(randomTime); // Wait for a random time
        }
    }

    private void SpawFood()
    {
        Vector3 posToAppear = new Vector3(
            Random.Range(-screenBounds.x, screenBounds.x),
            Random.Range(-screenBounds.y, screenBounds.y),
            0);
        
        GameObject food = Instantiate(myFood, posToAppear,Quaternion.identity,transform);

        //food.GetComponent<S_FoodBehavior>().SetFood((FoodType)Random.Range(0,3));       
    }    
}
