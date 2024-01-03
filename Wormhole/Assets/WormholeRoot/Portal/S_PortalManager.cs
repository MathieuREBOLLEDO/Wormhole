using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_PortalManager : MonoBehaviour
{
    [SerializeField] private S_GetPortalManager instancePortalManager;

    protected List<GameObject> portalList = new List<GameObject>();

    private void Awake()
    {
        instancePortalManager.myPortalManager = this;
    }

    void Start()
    {
        portalList.Add(GameObject.FindGameObjectWithTag("Portal"));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject getPortal(int id)
    {
        return portalList[0];
    }

}
