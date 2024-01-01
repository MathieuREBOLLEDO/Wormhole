using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[CreateAssetMenu(fileName = "SO_Snake", menuName = "Snake/SnakeData", order = 1)]
public class S_SnakeData : ScriptableObject
{
    [Header("Singleton")]
    //public SnakeHead snake;

    [Header("Datas")]
    public Quaternion snakeRotation;

    public int snakeSize;
    public int snakeSpeed;

    [Header ("")]    
    public Color min;
    public Color max;

}
