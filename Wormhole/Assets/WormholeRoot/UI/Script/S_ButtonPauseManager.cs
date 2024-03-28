using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_ButtonPauseManager : MonoBehaviour
{
    public void OnPressButton()
    {
        S_GameManager.TriggerPause(true);
    }
}
