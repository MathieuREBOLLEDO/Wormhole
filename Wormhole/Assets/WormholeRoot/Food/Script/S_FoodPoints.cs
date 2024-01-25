using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName ="SO_FoodPoints",menuName ="Level/Food Points", order =0)]
public class S_FoodPoints : ScriptableObject
{
    [SerializeField] public int [] points;
}
