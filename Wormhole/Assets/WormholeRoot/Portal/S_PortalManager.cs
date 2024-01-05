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
    /*
    void Start()
    {
        portalList.Add(GameObject.FindGameObjectWithTag("Portal"));
    }
    */


    public GameObject getPortal(GameObject portalTofind)
    {
        int id = portalList.IndexOf(portalTofind);
        return (id%2 != 0)?portalList[id-1] : portalList[id+1];
    }

    public void AddPortalToList(GameObject portal)
    {
        portalList.Add(portal);
    }

}
