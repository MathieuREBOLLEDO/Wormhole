using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class S_GameManager : MonoBehaviour
{
    [SerializeField] S_GetGameManager gManager;

    public bool isFirstTime;

    public bool isFirstInput = true;

    public bool isInMenu = true;

    private bool isPause = true;

    [SerializeField] GameObject canvasTips;
    [SerializeField] GameObject canvasTuto;

    [SerializeField] GameObject soundManager;

    //[SerializeField] 

    #region Pause event

    public delegate void PauseEvent(bool newPauseState);

    public static event PauseEvent PauseOrUnPauseGame;

    public static void TriggerPause(bool newPauseState)
    {
        if(PauseOrUnPauseGame != null)
        {
            PauseOrUnPauseGame(newPauseState);
        }
    }
    #endregion

    #region Score event

    public delegate void ScoreEvent(int points);
    public static event ScoreEvent UpdateScore;

    public static void TriggerIncrementScore(int points)
    {
        if (UpdateScore != null)
        {
            UpdateScore(points);
        }
    }
    #endregion

    private void Start()
    {
        PauseOrUnPauseGame += SetIsPause;

    }

    private void OnDestroy()
    {
        PauseOrUnPauseGame -= SetIsPause;
    }


    private void Awake()
    {
        gManager.gameManager = this;

        if (isFirstTime)
        {
            canvasTuto.SetActive(true);
        }
        canvasTips.SetActive(true);
    }

    public void HideTips()
    {
        //SetPauseGame();
        canvasTips.SetActive(false);
        isFirstInput = false;
    }

    public bool GetFirstInput()   
    {
        return isFirstInput;
    }

    private void SetIsPause(bool newPauseState)
    {
        isPause = newPauseState;
        isInMenu = newPauseState;
        SetTimeScale();
    }

    public void SetTimeScale()
    {
        Time.timeScale = (isPause) ? 0 : 1;
    }


}
