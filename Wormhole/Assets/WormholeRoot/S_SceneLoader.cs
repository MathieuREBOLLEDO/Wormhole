using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class S_SceneLoader : MonoBehaviour
{

    [SerializeField] Slider slider;

    public IEnumerator LoadSceneAsync()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(1);

        while (!asyncLoad.isDone)
        {
            slider.value = Mathf.Clamp01(asyncLoad.progress / 0.9f);
            yield return null;
        }
    }
}
