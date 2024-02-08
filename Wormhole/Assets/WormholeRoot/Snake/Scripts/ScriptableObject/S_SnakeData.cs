using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[CreateAssetMenu(fileName = "SO_Snake Datas", menuName = "Snake/Datas", order = 0)]
public class S_SnakeData : ScriptableObject
{   
    [Header("Datas")]
    //public Quaternion snakeRotation;

    public int initSize = 2;
    [Range(1f , 5f)]
    public float snakeSpeed = 0.2f;
    [Range(10 , 70)]
    public int snakeBodyGap = 10;

}
