using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class S_HUDManager : MonoBehaviour
{
    #region Variables
    [SerializeField] S_GetHUDManager thisManager;
    [SerializeField] S_HUDPaletteColor colorPalette;

    [Header("Background Color")]
    [SerializeField] Image bannerTopImage;
    [SerializeField] Camera backgroundCamera;
    [SerializeField] Image bannerBotImage;

    [Header("Score")]
    [SerializeField] TextMeshProUGUI textScore;
    [SerializeField] Image comboBar;
    [SerializeField] TextMeshProUGUI textCombo;


    [SerializeField] private int score;
    [SerializeField] private int comboCounter;
    [SerializeField] private float comboTimer;
    [SerializeField] private float comboDuration = 4f;
    [SerializeField]
    [Range(0f, 1f)] private float comboAmount = 0;
    #endregion

    private void Start()
    {
        thisManager.hudManager = this;
        SetScore(0);
        ResetCombo();
        backgroundCamera = GameObject.Find("Camera_UI").GetComponent<Camera>();
        ResetCombo();
    }



    public void UpdateCombo(int points)
    {
        SetCombo();
        int tmpPoints = (comboCounter != 0) ? points * comboCounter : points;
        SetScore(tmpPoints);
        comboTimer = comboDuration;
    }

    private void ResetCombo()
    {
        comboBar.fillAmount = 0;
        comboCounter = 0;
        textCombo.gameObject.SetActive(false);
        StopCoroutine(UpdateComboBar());

    }

    private void SetScore(int points)
    {
        score += points;
        textScore.text = score.ToString();
    }

    private void SetCombo()
    {
        comboCounter++;
        if (comboCounter >= 2)
        {
            textCombo.gameObject.SetActive(true);
            textCombo.text = "x" + comboCounter.ToString();
            StartCoroutine(UpdateComboBar());
        }
    }

    private void SetComboBar()
    {
        if (comboTimer != 0)
        {
            comboAmount = comboTimer / comboDuration;
            comboBar.fillAmount = comboAmount;
        }
    }

    private IEnumerator UpdateComboBar()    {   

        while (true)
        {            
            comboTimer -= 0.01f;
            SetComboBar();

            if (comboTimer <= 0)
            {
                ResetCombo();
            }
            yield return null;
        }

    }
}
