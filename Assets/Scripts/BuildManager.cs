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
    public GameObject sellEffect;

    private TurretBluePrint turretToBuild;
    private Node selectedNode;

    // need to do this whenever using property of non static methods/scripts
    public NodeUI nodeUI;
    // will return true if turretToBuild is not null
    // else will return false
    // instead of a function using this 
    public bool CanBuild { get { return turretToBuild != null; } }
    public bool HasMoney { get { return PlayerStats.Money >= turretToBuild.cost; } }

    public void SelectNode(Node node)
    {
        FindObjectOfType<AudioController>().GetComponent<AudioController>().PlayButtonClickSFX();
        if (selectedNode == node)
        {
            DeselectNode();
            return;
        }
        selectedNode = node;
        turretToBuild = null;

        nodeUI.SetTarget(node);

    }

    public void DeselectNode()
    {
        FindObjectOfType<AudioController>().GetComponent<AudioController>().PlayButtonClickSFX();
        selectedNode = null;
        nodeUI.Hide();
    }

    public void SelectTurretToBuild(TurretBluePrint turret)
    {
        turretToBuild = turret;
        FindObjectOfType<AudioController>().GetComponent<AudioController>().PlaySelectTurretSFX();
        DeselectNode();
    }

    public TurretBluePrint GetTurretToBuild()
    {
        return turretToBuild;
    }
}
