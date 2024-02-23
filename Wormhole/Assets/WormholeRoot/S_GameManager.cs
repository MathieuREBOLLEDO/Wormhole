using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState
{
    Menu,
    Game,
    Pause,
}

public class S_GameManager : MonoBehaviour
{
    [SerializeField] S_GetGameManager gManager;

    [SerializeField] GameObject soundManager;

    //[SerializeField] 

    private void Awake()
    {
        gManager.gameManager = this;
        //SceneManager.UnloadSceneAsync(0);
    }


}