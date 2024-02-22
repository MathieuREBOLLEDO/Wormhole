using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    Menu,
    Game,
    Pause,
}

public class S_GameManager : MonoBehaviour
{
    [SerializeField] S_GetGameManager gManager;
    private void Awake()
    {
        gManager.gameManager = this;
    }
}
