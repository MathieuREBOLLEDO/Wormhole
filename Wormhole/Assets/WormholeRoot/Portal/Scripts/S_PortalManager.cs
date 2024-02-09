using MoreMountains.Feedbacks;
using System.Collections.Generic;
using UnityEngine;

public class S_PortalManager : MonoBehaviour
{
    [SerializeField] private S_GetPortalManager instancePortalManager;
    [SerializeField] private S_PortalColorPalette colorPalette;
    [SerializeField] private GameObject portal;

    [SerializeField] private MMF_Player fb_PlacePortal;

    private int idColorToUse = -1;

    protected List<GameObject> portalList = new List<GameObject>();

    private void Awake()
    {
        instancePortalManager.myPortalManager = this;
    }

    public GameObject PlacePortal(Vector3 position, Quaternion rotation)
    {
        GameObject portalPlaced = GameObject.Instantiate(portal, position, rotation, transform);
        AddPortalToList(portalPlaced);

        SetColor(portalPlaced);
        fb_PlacePortal?.PlayFeedbacks();
        return portalPlaced;
    }

    public GameObject GetPortal(GameObject portalTofind)
    {
        int id = portalList.IndexOf(portalTofind);
        return (id % 2 != 0) ? portalList[id - 1] : portalList[id + 1];
    }

    public int GetPortalId(GameObject portalTofind)
    {
        return portalList.IndexOf(portalTofind);
    }

    public bool CheckForLink(GameObject portalTofind)
    {
        int id = portalList.IndexOf(portalTofind);
        bool checkIsTrue = true;

        if (id % 2 == 0 || id == 0)
        {
            if (portalList.Count - 1 == id)
                checkIsTrue = false;
            else checkIsTrue = true;
        }
        return checkIsTrue;
    }

    private void AddPortalToList(GameObject portal)
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

    private void SetColor(GameObject portal)
    {
        if (GetPortalId(portal) % 2 == 0)
        {
            idColorToUse++;
            if (idColorToUse >= colorPalette.colors.Length)
                idColorToUse = 0;
        }

        if (colorPalette.colors.Length != 0)
        {
            Color newColor = colorPalette.colors[idColorToUse];

            S_PortalElements portalRenderers = portal.GetComponent<S_PortalElements>();
            portalRenderers.portalRenderer.material.SetColor("_TwirlColor", newColor);
            //portalRenderers.arrowRenderer.color = newColor;
        }
    }
}
