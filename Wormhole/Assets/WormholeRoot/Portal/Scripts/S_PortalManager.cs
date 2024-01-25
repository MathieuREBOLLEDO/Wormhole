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


    public GameObject GetPortal(GameObject portalTofind)
    {
        int id = portalList.IndexOf(portalTofind);
        return (id % 2 != 0) ? portalList[id - 1] : portalList[id + 1];
        
    }

    public bool CheckForLink(GameObject portalTofind) 
    {
        int id = portalList.IndexOf(portalTofind);
        bool checkIsTrue = true;

        if(id % 2 == 0 || id == 0  )
        {            
            if (portalList.Count-1 == id)
                checkIsTrue = false;
            else checkIsTrue = true;
        }
        return checkIsTrue;
    }

    public void AddPortalToList(GameObject portal)
    {
        portalList.Add(portal);
    }

    public void DestroyMultiplePortals(GameObject portal)
    {
        DestroyPortal(GetPortal(portal));
        DestroyPortal(portal);
    }

    public void DestroyPortal(GameObject portal)
    {
        portalList.Remove(portal);
        Destroy(portal);
    }

}
