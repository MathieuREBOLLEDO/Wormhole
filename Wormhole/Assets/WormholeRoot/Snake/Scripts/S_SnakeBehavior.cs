using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_SnakeBehavior : MonoBehaviour
{ 
    public S_SnakeData snakeData;
    private Vector3 direction;

    private List <GameObject> bodyParts = new List <GameObject>();
    private List <Vector3> positionHistory = new List <Vector3>();
    

    void Awake()
    {
        GameObject head = GameObject.Instantiate(snakeData.snakeHead);
        bodyParts.Add(head);
    }

    void Start()
    {
        direction = Vector3.right;
        GrowSnake();
        GrowSnake();
        GrowSnake();
    }

    void Update()
    {
       // direction = 
        transform.position += direction * snakeData.snakeSpeed;

        positionHistory.Insert(0, transform.position);

        int index = 0;
        foreach (var body in bodyParts) 
        {
            Vector3 point = positionHistory[Mathf.Min(index*snakeData.snakeBodyGap,positionHistory.Count-1)];
            body.transform.position = point;
            index++;
        }

    }

    void AddSegment()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            GrowSnake();
        }
    }



    private void GrowSnake()
    {
        GameObject body = GameObject.Instantiate(snakeData.snakeBody);
        bodyParts.Add(body);
    }
}
