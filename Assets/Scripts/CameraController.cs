using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float panSpeed = 30f;
    public float panBorderThickness = 10f;
    public float scrollSpeed = 5f;
    public float minY = 10f;
    public float maxY = 80f;

    // Update is called once per frame
    void Update()
    {
        if (GameManager.GameIsOver)
        {
            // DISABLING THE SCRIPT COMPONENT
            // equivalent to this.enabled
            enabled = false;
            return;
        }

        if (Input.GetKey("w") || Input.mousePosition.y >= Screen.height - panBorderThickness)
        {
            // Vector3.forward ==> new Vector3(0f, 0f, 1f);
            // Writing Space.World ignores the current rotation of the camera
            // and hence will move forward according to the world space
            transform.Translate(Vector3.forward * panSpeed * Time.deltaTime, Space.World);
        }

        if (Input.GetKey("s") || Input.mousePosition.y <= panBorderThickness)
        {
            transform.Translate(Vector3.back * panSpeed * Time.deltaTime, Space.World);
        }

        if (Input.GetKey("d") || Input.mousePosition.x >= Screen.width - panBorderThickness)
        {
            transform.Translate(Vector3.right * panSpeed * Time.deltaTime, Space.World);
        }

        if (Input.GetKey("a") || Input.mousePosition.x <= panBorderThickness)
        {
            transform.Translate(Vector3.left * panSpeed * Time.deltaTime, Space.World);
        }

        float scroll = Input.GetAxis("Mouse ScrollWheel");

        Vector3 pos = transform.position;
        pos.y -= scroll * scrollSpeed * Time.deltaTime * 1000;
        // restrict pos.y value between minY and maxY
        pos.y = Mathf.Clamp(pos.y, minY, maxY);

        transform.position = pos;
    }
}
