using MoreMountains.Feedbacks;
using System.Collections;
using System.Collections.Generic;
using TMPro;
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

    #region Feebacks    
    [Header("Feedbacks")]
    [SerializeField] GameObject floatingText;
    [SerializeField] MMF_Player fEatFood;
    [SerializeField] MMF_Player fTeleport;
    [SerializeField] MMF_Player fEatPortal;
    [SerializeField] MMF_Player fDestroyPortal;
    #endregion

    [Header("Other Manager")]
    [SerializeField] private S_GetPortalManager portalManager;
    [SerializeField] private S_GetHUDManager hud;

    #region VarPrivate
    [SerializeField] bool isInvulnerable;
    bool firstUseOnePortal = false;
    bool isInitialized = false;
    bool isDead = false;
    private Vector3 direction;
    private S_CheckBounds checkBounds;

    #region List
    private List <GameObject> bodyParts = new List <GameObject>();
    private List <S_SnakeBodyAnimation> bodyAnimations = new List <S_SnakeBodyAnimation>();

    private List <Vector3> positionHistory = new List <Vector3>();
    
    private List <float> listOfAlpha = new List<float>();
    #endregion
    #endregion

    #region Init
    void Awake()
    {
        Application.targetFrameRate = 30;
        //GameObject head = GameObject.Instantiate(snakePrefab.snakeHead,bodysParent);
        S_SnakeBodyAnimation head = GetComponentInChildren<S_SnakeBodyAnimation>();
        head.GetComponent<Collider2D>().enabled = false;
        bodyParts.Add(head.gameObject);
        bodyAnimations.Add(head);
        listOfAlpha.Add(0f);      
        
        checkBounds = GetComponent<S_CheckBounds>();
    }

    void Start()
    {
        direction = transform.right;
        Invoke("InitSnake",0.15f);
    }

    private void InitSnake()
    {   
        for (int i = 0; i < snakeData.initSize; i++)
            GrowSnake();
        bodyParts[0].GetComponent<Collider2D>().enabled = true;    
    
    }
    public void EnableCollision()
    {
        for (int i = 0; i < snakeData.initSize; i++)
        {
            if (i != snakeData.initSize - 1)
            {
                bodyParts[i + 1].GetComponent<Collider2D>().enabled = false; // deactivate collider 2d for the 2 element following the head
            }
        }
        isInitialized = true;
    }
    #endregion

    void Update()
    {
        if(isInitialized)
            checkBounds.CheckForBounds();

        DebugInput();

        if (!isDead)
        {          
            transform.position += direction * snakeData.snakeSpeed * Time.deltaTime;

            positionHistory.Insert(0, transform.position);

            int index = 0;
            foreach (var body in bodyParts)
            {
                Vector3 point = positionHistory[Mathf.Min(index * (snakeData.snakeBodyGap/10), positionHistory.Count - 1)];
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
            EatFood(objecCollide);            
        }
    }

    #region Behavior
    private void MoveBodyBehavior()
    {
        if (bodyParts[bodyParts.Count - 2].GetComponent<S_SnakeBodyBehavior>() != null)
        {
            Destroy(bodyParts[bodyParts.Count - 2].GetComponent<S_SnakeBodyBehavior>());
        }

        bodyParts[bodyParts.Count - 1].AddComponent<S_SnakeBodyBehavior>();
    }

    void TeleportSnake(GameObject portal)
    {
        portalManager.myPortalManager.GetPortal(portal).GetComponent<Collider2D>().enabled = false;

        if (!isInitialized)
            EnableCollision();

        transform.position = portalManager.myPortalManager.GetPortal(portal).transform.position;
        transform.rotation = portalManager.myPortalManager.GetPortal(portal).transform.rotation;
        direction = transform.right;
        fTeleport?.PlayFeedbacks();
    }

    void EatFood(GameObject other)
    {
        other.GetComponent<IEatable>().Eat();

        int pointsToDisplay = other.GetComponent<IEatable>().GetPoint();
        DisplayFloatingText(pointsToDisplay, bodyParts[0].transform.position);

        fEatFood?.PlayFeedbacks();
        //hud.hudManager.UpdateCombo(other.GetComponent<IEatable>().GetPoint());

        GrowSnake();
    }

    private void GrowSnake()
    {
        int newId = bodyParts.Count;
        Vector3 tmpPosition = positionHistory[Mathf.Min(newId * (snakeData.snakeBodyGap / 10), positionHistory.Count - 1)];
        GameObject body = GameObject.Instantiate(snakePrefab.snakeBody, tmpPosition,Quaternion.identity, bodysParent);
        bodyParts.Add(body);
        bodyAnimations.Add(body.GetComponent<S_SnakeBodyAnimation>());
        listOfAlpha.Add(0f);

        int index = 0;

        foreach(var part in bodyParts)
        {            
            part.GetComponent<S_SnakeBodyAnimation>().SetBodyColor(ColorUpdater(index));
            part.GetComponent<S_SnakeBodyAnimation>().SetIdSnake(index,bodyParts.Count-1);
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

    public void CallDeath()
    {
        if (isInitialized && !isInvulnerable)
        {
            Debug.Log("<color=red>DEATH!!!</color>");
            isDead = true;
            bodyParts[0].GetComponent<S_SnakeHeadBehavior>().deathFeedback?.PlayFeedbacks();
            StartCoroutine(DestroySnake());
        }
    }
    #endregion

    void DisplayFloatingText(int points, Vector3 position)
    {
        GameObject tmpText = GameObject.Instantiate(floatingText, position, Quaternion.identity, bodysParent);
        //tmpText.GetComponentInChildren<TextMesh>().text = points.ToString();
        tmpText.GetComponentInChildren<TextMeshPro>().text = points.ToString();
        Destroy(tmpText,1f);
    }

    public void CallDestroyPortals(GameObject portal)
    {
        portalManager.myPortalManager.DestroyMultiplePortals(portal);
        fDestroyPortal?.PlayFeedbacks();
    }

    IEnumerator DestroySnake()
    {
        for (int i = bodyParts.Count - 1 ; i >= 0; i--)
        {
            int points = 50;
            bodyParts[i].GetComponent<S_SnakeBodyAnimation>().deathFeedback?.PlayFeedbacks();
            DisplayFloatingText(points, bodyParts[i].transform.position);
            yield return new WaitForSeconds(0.15f);
        } 
        StopCoroutine(DestroySnake());
    }    

    void DebugInput()
    {
        if (Input.GetKey(KeyCode.A)) 
        {
            CallDeath();
        }
    }
}
