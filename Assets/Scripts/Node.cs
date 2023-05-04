using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour
{
    public Color hoverColor;
    public Vector3 positionOffset;
    private GameObject turret;
    private Renderer rend;
    private Color startColor;

    BuildManager buildManager;

    void Start()
    {
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;

        buildManager = BuildManager.instance;
    }

    void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;
        // check if the turret we want to build is selected 
        if (buildManager.GetTurretToBuild() == null)
            return;
        // if already have build turret to this node
        if (turret != null)
        {
            Debug.Log("Can't build there! TODO: Display on Screen");
            return;
        }
        // Build a Turret
        GameObject turretToBuild = BuildManager.instance.GetTurretToBuild();
        turret = (GameObject)Instantiate(turretToBuild, transform.position + positionOffset, transform.rotation);
    }

    void OnMouseEnter()
    {
        // if the turret icon overlap node 
        // then make sure we select the turret
        // and not instantiate any
        if (EventSystem.current.IsPointerOverGameObject())
            return;
        if (buildManager.GetTurretToBuild() == null)
            return;
        rend.material.color = hoverColor;
    }

    void OnMouseExit()
    {
        rend.material.color = startColor;
    }
}
