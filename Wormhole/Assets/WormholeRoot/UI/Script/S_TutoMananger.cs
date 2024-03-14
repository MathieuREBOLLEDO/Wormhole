using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class S_TutoMananger : MonoBehaviour
{
    [SerializeField] S_GetGameManager gM;
    int idText = 0;

    [SerializeField] string[] textToDisplay = new string[] { "Place A Portal", "Lorem Ipsum", "Sit Dolor"};
    [SerializeField] TextMeshProUGUI displayText;

    [SerializeField] GameObject boutonPrevious;
    [SerializeField] GameObject boutonNext;
    [SerializeField] GameObject boutonClose;

    private void Start()
    {
        gM.gameManager.isInMenu = true;
        boutonPrevious.SetActive(false);
        SetText();
        boutonClose.SetActive(false);
    }

    public void CallNext()
    {
        idText++;
        SetText();
        if( idText == textToDisplay.Length-1)
        {
            boutonNext.SetActive(false);
            boutonClose.SetActive(true);
        }
        boutonPrevious.SetActive(true);
    }

    public void CallPrev() 
    {
        idText--;
        SetText();
        if(idText==0)
        {
            boutonPrevious.SetActive(false);
        }
        boutonNext.SetActive(true);
        boutonClose.SetActive(false);
    }

    public void CallClose()
    {
        gM.gameManager.isInMenu=false;
        Destroy(this.gameObject);
    }

    private string SetText()
    {
        return displayText.text = textToDisplay[idText];
    }
}
