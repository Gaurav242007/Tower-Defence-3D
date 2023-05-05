using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour
{
    public Color hoverColor;
    public Color notEnoughMoneyMat;
    public Vector3 positionOffset;

    [Header("Optional")]
    public GameObject turret;
    private Renderer rend;
    private Color startColor;

    BuildManager buildManager;

    void Start()
    {
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;

        buildManager = BuildManager.instance;
    }

    // return type Vector3 --> x, y, z offset
    public Vector3 GetBuildPosition()
    {
        return transform.position + positionOffset;
    }

    void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;
        // check if the turret we want to build is selected 
        if (!buildManager.CanBuild)
            return;
        // if already have build turret to this node
        if (turret != null)
        {
            Debug.Log("Can't build there! TODO: Display on Screen");
            return;
        }
        // passing the function all properties of 
        // the current GameObject
        buildManager.BuildTurretOn(this);
    }

    void OnMouseEnter()
    {
        // if the turret icon overlap node 
        // then make sure we select the turret
        // and not instantiate any
        if (EventSystem.current.IsPointerOverGameObject())
            return;
        if (!buildManager.CanBuild)
            return;
        if (!buildManager.hasEnoughMoneyToBuild)
        {
            rend.material.color = notEnoughMoneyMat;
            return;
        }
        rend.material.color = hoverColor;
    }

    void OnMouseExit()
    {
        rend.material.color = startColor;
    }
}
