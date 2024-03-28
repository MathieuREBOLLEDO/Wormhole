using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject content;
    [SerializeField]

    private void Awake()
    {
        content.SetActive(false);
    }

    private void Start()
    {
        S_GameManager.PauseOrUnPauseGame += ShowMenu;

    }

    private void OnDestroy()
    {
        S_GameManager.PauseOrUnPauseGame -= ShowMenu;
    }

    private void ShowMenu(bool newPauseState)
    {
        content.SetActive(newPauseState);
    }

    public void CloseMenu()
    {
        S_GameManager.TriggerPause(false);
    }
}
