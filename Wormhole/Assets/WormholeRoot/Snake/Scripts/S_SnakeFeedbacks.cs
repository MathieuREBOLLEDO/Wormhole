using MoreMountains.Feedbacks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="SO_Snake Feedbacks",menuName = "Snake/Feedbacks", order =-1)]
public class S_SnakeFeedbacks : ScriptableObject 
{
    public MMF_Player eatFB;
    public MMF_Player teleportFB;
    public MMF_Player eatPortalFB;
}
