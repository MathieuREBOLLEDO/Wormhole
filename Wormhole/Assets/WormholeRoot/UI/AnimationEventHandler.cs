using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEventHandler: MonoBehaviour
{

    [SerializeField] S_SceneLoader sceneLoader;
    

    public void OnAnimationEnd()
    {
        Debug.Log("Animation has ended!");
        // Add any additional logic you want to execute when the animation ends.
        StartCoroutine(sceneLoader.LoadSceneAsync());
    }
}
