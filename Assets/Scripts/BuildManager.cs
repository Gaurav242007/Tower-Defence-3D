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

    public GameObject standardTurretPrefab;

    void Start()
    {
        turretToBuild = standardTurretPrefab;
    }

    private GameObject turretToBuild;

    public GameObject GetTurretToBuild()
    {
        return turretToBuild;
    }
}
