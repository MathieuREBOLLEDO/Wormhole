using System.Collections;
using System.Collections.Generic;
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
        GameObject head = GameObject.Instantiate(snakeData.snakeHead,this.transform);
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

    }

    private void OnCollisionEnter(Collision collision)
    {
   
        if (collision.gameObject.CompareTag("Portal"))
        {
            Debug.Log("Collision with" + collision);
            transform.position = portalManager.myPortalManager.getPortal(0).transform.position;
            transform.rotation = portalManager.myPortalManager.getPortal(0).transform.rotation;
            direction = transform.right;
        }
    }

    void AddSegment()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            GrowSnake();
        }
    }

    void TeleportSnake()
    {

    }

    private void GrowSnake()
    {
        GameObject body = GameObject.Instantiate(snakeData.snakeBody, this.transform);
        bodyParts.Add(body);

        int index = 0;
        foreach(var part in bodyParts)
        {
            part.GetComponent<MeshRenderer>().material.color = new Color(0, 1, 1);
            index++;
        }
    }
}
