using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class S_SnakeBehavior : MonoBehaviour
{ 
    public S_SnakeData snakeData;
    public S_SnakeColorPalette colorPalette;

    [Header("")]
    [SerializeField] public S_GetPortalManager portalManager;

    private Vector3 direction;

    private List <GameObject> bodyParts = new List <GameObject>();
    private List <Vector3> positionHistory = new List <Vector3>();

    public List <float> listOfAlpha = new List<float>();

    bool isDead = false;
    

    void Awake()
    {
        //GameObject head = GameObject.Instantiate(snakeData.snakeHead,this.transform);
        GameObject head = GetComponentInChildren<Transform>().gameObject;
        bodyParts.Add(head);
        listOfAlpha.Add(0f);
    }

    void Start()
    {
        direction = transform.right;

        StartCoroutine(InitSnake(0.5f));
    }

    void Update()
    {
        if (!isDead)
        {
            transform.position += direction * snakeData.snakeSpeed;

            positionHistory.Insert(0, transform.position);

            int index = 0;
            foreach (var body in bodyParts)
            {
                Vector3 point = positionHistory[Mathf.Min(index * snakeData.snakeBodyGap, positionHistory.Count - 1)];
                body.transform.position = point;

                index++;
            }

            AddSegment();
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
   
        if (collision.gameObject.CompareTag("Portal"))
        {
            if(portalManager.myPortalManager.CheckForLink(collision.gameObject))
            {
                TeleportSnake(collision.gameObject);
            }
            else
            {
                portalManager.myPortalManager.DestroyPortal(collision.gameObject);
            }
            
        }
    }

    void AddSegment()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            GrowSnake();
        }
    }

    void TeleportSnake(GameObject portal)
    {
        portalManager.myPortalManager.GetPortal(portal).GetComponent<Collider>().enabled = false;
        transform.position = portalManager.myPortalManager.GetPortal(portal).transform.position;
        transform.rotation = portalManager.myPortalManager.GetPortal(portal).transform.rotation;
        direction = transform.right;
    }

    private void GrowSnake()
    {
        GameObject body = GameObject.Instantiate(snakeData.snakeBody, this.transform);
        bodyParts.Add(body);
        listOfAlpha.Add(0f);

        int index = 0;

        foreach(var part in bodyParts)
        {
            if(index!=0)
            {
                part.GetComponent<MeshRenderer>().material.color = ColorUpdater(index); ;         
            }                
            index++;
        }

        MoveBodyBehaviorScript();
    }

    private Color ColorUpdater(int id)
    {
        Color color;

        listOfAlpha[id] =  id / (float)(bodyParts.Count-1);

        float lerpAlpha = Mathf.Lerp(0, 1, listOfAlpha[id]);

        if (lerpAlpha<0.5f)
        {
            color = Color.Lerp(colorPalette.minColor,colorPalette.middleColor, lerpAlpha);
        }
        else
        {         
            color = Color.Lerp(colorPalette.middleColor, colorPalette.maxColor, lerpAlpha);
        }
        //Debug.Log("$<color=" + color + ">" + lerpAlpha + "</color>");
        return color;
    }

    private void MoveBodyBehaviorScript()
    {
        if (bodyParts[bodyParts.Count - 2].GetComponent<S_BodyBehavior>() != null)
        {
            Destroy(bodyParts[bodyParts.Count - 2].GetComponent<S_BodyBehavior>());
        }

        bodyParts[bodyParts.Count - 1].AddComponent<S_BodyBehavior>();
    }

    public void CallDeath()
    {
        Debug.Log("$<color=red>DEATH!!!</color");
        isDead = true;
    }

    IEnumerator InitSnake(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);

        for (int i = 0; i < snakeData.initSize; i++)
            GrowSnake();
    }
}
