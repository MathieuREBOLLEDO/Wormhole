using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SO_AnimationData", menuName = "Snake/AnimationData", order = 3)]

public class S_AnimationDatas : ScriptableObject
{
    public float rotationValues;

    public float amplitude = 1.0f;
    public float frequency = 1.0f;

}
