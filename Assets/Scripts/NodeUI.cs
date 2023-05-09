using UnityEngine;

public class NodeUI : MonoBehaviour
{
    public GameObject ui;
    private Node target;
    public Vector3 offset;

    public void SetTarget(Node _target)
    {
        target = _target;
        transform.position = target.GetBuildPosition() + offset;

        ui.SetActive(true);
    }

    public void Hide()
    {
        ui.SetActive(false);
    }
}
