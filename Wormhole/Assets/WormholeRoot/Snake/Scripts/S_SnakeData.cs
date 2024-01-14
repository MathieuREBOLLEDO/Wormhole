using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[CreateAssetMenu(fileName = "SO_Snake", menuName = "Snake/SnakeData", order = 1)]
public class S_SnakeData : ScriptableObject
{
    [Header("Prefab")]
    public GameObject snakeHead;
    public GameObject snakeBody;

    [Header("Datas")]
    public Quaternion snakeRotation;

    public int initSize = 2;
    public float snakeSpeed = 0.2f;
    public int snakeBodyGap = 10;

}
