using MoreMountains.Feedbacks;
using System.Collections;
using System.Collections.Generic;
//using System.Drawing;
using UnityEngine;
//using Color = Unity.Color;

public class S_SnakeBehavior : MonoBehaviour
{
    [SerializeField] Transform bodysParent;
    [Header("Datas")]
    public S_SnakeData snakeData;
    public S_SnakePrefab snakePrefab;
    public S_SnakeColorPalette colorPalette;

    [Header("Feedbacks")]
    [SerializeField] MMF_Player fEatFood;
    [SerializeField] MMF_Player fTeleport;
    [SerializeField] MMF_Player fEatPortal;

    //public S_SnakeFeedbacks snakeFeedbacks;

    [Header("PortalManager")]
    [SerializeField] public S_GetPortalManager portalManager;   

    private Vector3 direction;

    #region List
    private List <GameObject> bodyParts = new List <GameObject>();
    private List <S_SnakeBodyAnimation> bodyAnimations = new List <S_SnakeBodyAnimation>();

    private List <Vector3> positionHistory = new List <Vector3>();
    
    private List <float> listOfAlpha = new List<float>();
    #endregion

    
    bool isDead = false;
    

    void Awake()
    {
        //GameObject head = GameObject.Instantiate(snakePrefab.snakeHead,bodysParent);
        S_SnakeBodyAnimation head = GetComponentInChildren<S_SnakeBodyAnimation>();
        bodyParts.Add(head.gameObject);
        bodyAnimations.Add(head);
        listOfAlpha.Add(0f);        
    }

    void Start()
    {
        direction = transform.right;
        StartCoroutine(InitSnake(0.5f));

        //GameObject parentFeeback = GameObject.Instantiate(new GameObject("Feebacks"), transform);
        //fEatFood = GameObject.Instantiate(snakeFeedbacks.eatFB, parentFeeback.transform);
        //fTeleport = GameObject.Instantiate(snakeFeedbacks.teleportFB, parentFeeback.transform);
        //fEatFood = GameObject.Instantiate(snakeFeedbacks.eatPortalFB, parentFeeback.transform);

    }

    void Update()
    {
        if (!isDead)
        {
            AddSegment();

            transform.position += direction * snakeData.snakeSpeed * Time.deltaTime;

            positionHistory.Insert(0, transform.position);

            int index = 0;
            foreach (var body in bodyParts)
            {
                Vector3 point = positionHistory[Mathf.Min(index * snakeData.snakeBodyGap, positionHistory.Count - 1)];
                body.transform.position = point;
                bodyAnimations[index].AnimateBody();
                index++;
                
            }            
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject objecCollide = collision.gameObject;
        if (objecCollide.CompareTag("Portal"))
        {
            if (portalManager.myPortalManager.CheckForLink(objecCollide))
            {
                TeleportSnake(objecCollide);
            }
            else
            {
                portalManager.myPortalManager.DestroyPortal(objecCollide);
            }

        }

        if (objecCollide.GetComponent<MonoBehaviour>() as IEatable != null)
        {
            objecCollide.GetComponent<IEatable>().Eat();
            fEatFood?.PlayFeedbacks();
            GrowSnake();
        }
    }  

    void AddSegment()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            GrowSnake();
        }

        /*
        if( Input.GetMouseButtonDown(0))
        {
            GrowSnake();
        }*/

        if (Input.GetKeyDown(KeyCode.Space)) { CallDeath(); }
    }

    void TeleportSnake(GameObject portal)
    {
        portalManager.myPortalManager.GetPortal(portal).GetComponent<Collider2D>().enabled = false;
        transform.position = portalManager.myPortalManager.GetPortal(portal).transform.position;
        transform.rotation = portalManager.myPortalManager.GetPortal(portal).transform.rotation;
        direction = transform.right;
    }

    private void GrowSnake()
    {
        GameObject body = GameObject.Instantiate(snakePrefab.snakeBody, bodysParent);
        bodyParts.Add(body);
        bodyAnimations.Add(body.GetComponent<S_SnakeBodyAnimation>());
        listOfAlpha.Add(0f);

        int index = 0;

        foreach(var part in bodyParts)
        {            
            if (index!=0)
            {
                part.GetComponentInChildren<MeshRenderer>().material.color = ColorUpdater(index);               
            }                
            index++;
        }

        MoveBodyBehavior();
    }

    private Color ColorUpdater(int id)
    {
        Color colorToReturn;
        listOfAlpha[id] =  id / (float)(bodyParts.Count-1);
        float lerpAlpha = Mathf.Lerp(0, 1, listOfAlpha[id]);
        
        if (lerpAlpha<0.5f)
        {
            lerpAlpha = lerpAlpha * 2;
            colorToReturn = Color.Lerp(colorPalette.minColor,colorPalette.middleColor, lerpAlpha);
        }
        else
        {
            lerpAlpha = 2 * lerpAlpha - 1;
            colorToReturn = Color.Lerp(colorPalette.middleColor, colorPalette.maxColor, lerpAlpha);
        }
        return colorToReturn;
    }

    private void MoveBodyBehavior()
    {
        if (bodyParts[bodyParts.Count - 2].GetComponent<S_SnakeBodyBehavior>() != null)
        {
            Destroy(bodyParts[bodyParts.Count - 2].GetComponent<S_SnakeBodyBehavior>());
        }

        bodyParts[bodyParts.Count - 1].AddComponent<S_SnakeBodyBehavior>();
    }

    public void CallDeath()
    {
        Debug.Log("<color=red>DEATH!!!</color>");
        isDead = true;
        bodyParts[0].GetComponent<S_SnakeHeadBehavior>().deathFeedback?.PlayFeedbacks();

        StartCoroutine(DestroySnake());
    }


    IEnumerator DestroySnake()
    {
        for (int i = bodyParts.Count - 1 ; i >= 0; i--)
        {            
            bodyParts[i].GetComponent<S_SnakeBodyAnimation>().deathFeedback?.PlayFeedbacks();
            yield return new WaitForSeconds(0.15f);
        }   

    }

    IEnumerator InitSnake(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);

        for (int i = 0; i < snakeData.initSize; i++)
            GrowSnake();
    }
}
