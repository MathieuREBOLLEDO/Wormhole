using MoreMountains.Feedbacks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_SnakeBodyAnimation : MonoBehaviour
{
    [SerializeField] GameObject bodyRenderer;
    [SerializeField] private S_SnakeAnimationDatas datasAnim;

    [SerializeField] public MMF_Player deathFeedback;

    private float idSnake;

    private float startTime;

    private bool isRandom = true;

    private void Start()
    {
        if( isRandom)
            startTime = Time.time + Random.Range(0f,1);    
    }

    public void AnimateBody()
    {
        float yPos;
        if (isRandom)
            yPos = Mathf.Sin((Time.time - startTime) * datasAnim.frequency) * datasAnim.amplitude*Time.deltaTime;
        else
            yPos = Mathf.Sin((Time.time - (Time.time)+idSnake)*datasAnim.frequency)*datasAnim.amplitude*Time.deltaTime;

        bodyRenderer.transform.localPosition = new Vector3(bodyRenderer.transform.localPosition.x, yPos, bodyRenderer.transform.localPosition.z);
    }

    public void SetIdSnake(int id, int snakeSize)
    {
        if(snakeSize!=0)
            idSnake = id/snakeSize;
    }

    public void SetBodyColor(Color newColor)
    {
        bodyRenderer.GetComponent<MeshRenderer>().material.color = newColor;
    }
}
