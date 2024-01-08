using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class S_SnakeBehavior : MonoBehaviour
{ 
    public S_SnakeData snakeData;
    public S_SnakeColorPalette colorPalette;

    [Header("")]
    [SerializeField] private S_GetPortalManager portalManager;

    private Vector3 direction;

    private List <GameObject> bodyParts = new List <GameObject>();
    private List <Vector3> positionHistory = new List <Vector3>();
    

    void Awake()
    {
        //GameObject head = GameObject.Instantiate(snakeData.snakeHead,this.transform);
        GameObject head = GetComponentInChildren<Transform>().gameObject;
        bodyParts.Add(head);
    }

    void Start()
    {
        direction = transform.right;

        for (int i = 0;i <snakeData.initSize;i++) 
            GrowSnake();        
    }

    void Update()
    {
        transform.position += direction * snakeData.snakeSpeed;

        positionHistory.Insert(0, transform.position);

        int index = 0;
        foreach (var body in bodyParts) 
        {
            Vector3 point = positionHistory[Mathf.Min(index*snakeData.snakeBodyGap,positionHistory.Count-1)];
            body.transform.position = point;
            index++;
        }

        AddSegment();

    }

    private void OnCollisionEnter(Collision collision)
    {
   
        if (collision.gameObject.CompareTag("Portal"))
        {
            TeleportSnake(collision.gameObject);
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
        portalManager.myPortalManager.getPortal(portal).GetComponent<Collider>().enabled = false;
        transform.position = portalManager.myPortalManager.getPortal(portal).transform.position;
        transform.rotation = portalManager.myPortalManager.getPortal(portal).transform.rotation;
        direction = transform.right;
    }

    private void GrowSnake()
    {
        GameObject body = GameObject.Instantiate(snakeData.snakeBody, this.transform);
        bodyParts.Add(body);

        int index = 0;
        foreach(var part in bodyParts)
        {
            if(index!=0)
                part.GetComponent<MeshRenderer>().material.color = new Color(0, 1, 1);
            index++;
        }


        if (bodyParts[bodyParts.Count - 2].GetComponent<S_BodyBehavior>() != null)
        {
            Debug.Log("Test");
            Destroy(bodyParts[bodyParts.Count - 2].GetComponent<S_BodyBehavior>());
        }

        bodyParts[bodyParts.Count - 1].AddComponent<S_BodyBehavior>();
        
    }

    public void DestroyPoral(GameObject portal)
    {
        Destroy(portalManager.myPortalManager.getPortal(portal));
        Destroy(portal);
    }
}
