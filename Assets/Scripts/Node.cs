using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour
{
    public Color hoverColor;
    public Color notEnoughMoneyColor;
    public Vector3 positionOffset;

    [HideInInspector]
    public GameObject turret;
    [HideInInspector]
    public TurretBluePrint turretBluePrint;
    [HideInInspector]
    public bool isUpgraded = false;

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
        // if already have build turret to this node
        if (turret != null)
        {
            buildManager.SelectNode(this);
            return;
        }
        // check if the turret we want to build is selected 
        if (!buildManager.CanBuild)
            return;
        // passing the function all properties of 
        // the current GameObject
        BuildTurret(buildManager.GetTurretToBuild());
    }

    void BuildTurret(TurretBluePrint bluePrint)
    {
        if (GameManager.turretsCount >= GameManager.MaxTurrets)
            return;
        // if the Money is less than the turret cost
        if (PlayerStats.Money < bluePrint.cost)
        {
            Debug.Log("Not enough money build that!");
            return;
        }
        // if have that much money as turret cost
        // then subtract the money
        PlayerStats.Money -= bluePrint.cost;

        // Build a Turret
        GameObject _turret = (GameObject)Instantiate(bluePrint.prefab, GetBuildPosition(), Quaternion.identity);
        turret = _turret;
        turretBluePrint = bluePrint;

        // Playing Audio
        FindObjectOfType<AudioController>().GetComponent<AudioController>().PlayInstantiateTurretSFX();

        GameObject effect = (GameObject)Instantiate(buildManager.buildEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 5f);

        GameManager.turretsCount++;
    }

    public void UpgradeTurret()
    {
        // if the Money is less than the turret upgradeCost
        if (PlayerStats.Money < turretBluePrint.upgradeCost)
        {
            Debug.Log("Not enough money upgrade turret!");
            return;
        }
        // if have that much money as turret upgradeCost
        // then subtract the money
        PlayerStats.Money -= turretBluePrint.upgradeCost;

        // Get rid of the old turret
        Destroy(turret);

        // Build a new Upgraded Turret
        GameObject _turret = (GameObject)Instantiate(turretBluePrint.upgradedPrefab, GetBuildPosition(), Quaternion.identity);
        turret = _turret;

        // Playing Audio
        FindObjectOfType<AudioController>().GetComponent<AudioController>().PlayUpgradeTurretSFX();

        GameObject effect = (GameObject)Instantiate(buildManager.buildEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 5f);

        isUpgraded = true;

        Debug.Log("Turret Upgraded!");
    }

    public void SellTurret()
    {
        PlayerStats.Money += turretBluePrint.GetSellAmount();

        FindObjectOfType<AudioController>().GetComponent<AudioController>().PlaySellSFX();

        GameObject effect = (GameObject)Instantiate(buildManager.sellEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 5f);

        // Destroying the turrets
        Destroy(turret);
        turretBluePrint = null;

        GameManager.turretsCount--;
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

        if (buildManager.HasMoney)
        {
            rend.material.color = hoverColor;
        }
        else
        {
            rend.material.color = notEnoughMoneyColor;
        }
    }

    void OnMouseExit()
    {
        rend.material.color = startColor;
    }
}
