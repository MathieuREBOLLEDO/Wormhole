using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class S_MenuManager : MonoBehaviour
{
    public int sceneId = 1;
    public void StartGame()
    {
        StartCoroutine(LoadSceneAsync());
    }

    IEnumerator LoadSceneAsync()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneId);
        while(!operation.isDone) 
        {
            yield return null;
        }
        StopCoroutine(LoadSceneAsync());
        SceneManager.UnloadSceneAsync(0);
    }
}
