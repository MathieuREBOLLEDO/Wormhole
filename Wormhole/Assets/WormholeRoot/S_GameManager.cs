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

    public bool isFirstInput = true;

    public bool isInMenu = true;

    [SerializeField] GameObject canvasTips;

    [SerializeField] GameObject soundManager;

    //[SerializeField] 

    private void Awake()
    {
        gManager.gameManager = this;
    }

    public void HideTips()
    {
        canvasTips.SetActive(false);
        isFirstInput = false;
    }

    public bool GetFirstInput()   
    {
        return isFirstInput;
    }


}
