using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_ButtonPauseManager : MonoBehaviour
{
    [SerializeField] S_GetGameManager gM;
    [SerializeField] GameObject screenPause;
    //[SerializeField] GameObject buttonPause;


    public void OnPressButton()
    {
        gM.gameManager.isInMenu = true;
        screenPause.SetActive(true);
        gM.gameManager.PauseGame(true);
    }
}
