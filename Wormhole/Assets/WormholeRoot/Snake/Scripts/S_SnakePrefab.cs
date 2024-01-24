using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SO_Snake Prefab", menuName = "Snake/Snake Prefab", order = 1)]
public class S_SnakePrefab : ScriptableObject
{
    [Header("Prefab")]
    public GameObject snakeHead;
    public GameObject snakeBody;
}
