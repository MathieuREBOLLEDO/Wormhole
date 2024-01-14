using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        border.DisplayGizmos(screenBounds, Color.green);
    }

    private void SpawFood()
    {
        GameObject food = Instantiate(myFood);
    }
}
