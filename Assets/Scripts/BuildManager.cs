using UnityEngine;

public class BuildManager : MonoBehaviour
{
    // static variable can be access from anywhere
    public static BuildManager instance;

    // awake method is call right before start
    void Awake()
    {
        if (instance != null)
        {
            Debug.Log("More than one BuildManager");
            return;
        }
        instance = this;
    }

    public GameObject buildEffect;

    private TurretBluePrint turretToBuild;
    // will return true if turretToBuild is not null
    // else will return false
    // instead of a function using this 
    public bool CanBuild { get { return turretToBuild != null; } }
    public bool HasMoney { get { return PlayerStats.Money >= turretToBuild.cost; } }

    public void BuildTurretOn(Node node)
    {
        // if the Money is less than the turret cost
        if (PlayerStats.Money < turretToBuild.cost)
        {
            Debug.Log("Not enough money build that!");
            return;
        }
        // if have that much money as turret cost
        // then subtract the money
        PlayerStats.Money -= turretToBuild.cost;

        // Build a Turret
        GameObject turret = (GameObject)Instantiate(turretToBuild.prefab, node.GetBuildPosition(), Quaternion.identity);
        node.turret = turret;

        GameObject effect = (GameObject)Instantiate(buildEffect, node.GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 5f);

        Debug.Log("Turret build! Money Left: " + PlayerStats.Money);

    }

    public void SelectTurretToBuild(TurretBluePrint turret)
    {
        turretToBuild = turret;
    }
}
