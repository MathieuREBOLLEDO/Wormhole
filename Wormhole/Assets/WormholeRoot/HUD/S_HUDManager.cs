using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class S_HUDManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textFrameRate;
    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 120;
    }

    // Update is called once per frame
    void Update()
    {
        textFrameRate.SetText(((int)(1f/Time.smoothDeltaTime)).ToString());
    }
}
