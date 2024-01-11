using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_SetBackgroundDatas : MonoBehaviour
{
    [SerializeField] private S_BackgroundData data;

    private void Start()
    {
        GetComponent<MeshRenderer>().material.SetColor("_TileColor", data.tileColor);
        GetComponent<MeshRenderer>().material.SetColor("_BorderColor", data.borderColor);
        Debug.Log("Set Color background");
    }
}
