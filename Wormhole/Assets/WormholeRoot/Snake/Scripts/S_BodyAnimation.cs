using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_BodyAnimation : MonoBehaviour
{
    [SerializeField] GameObject bodyRenderer;
    [SerializeField] private S_AnimationDatas datasAnim;

    private float startTime;

    private void Start()
    {
        startTime = Time.time + Random.Range(0f,1);    
    }

    public void AnimateBody()
    {
        float yPos = Mathf.Sin((Time.time - startTime) * datasAnim.frequency) * datasAnim.amplitude;

        bodyRenderer.transform.localPosition = new Vector3(bodyRenderer.transform.localPosition.x, yPos, bodyRenderer.transform.localPosition.z);
    }
}
