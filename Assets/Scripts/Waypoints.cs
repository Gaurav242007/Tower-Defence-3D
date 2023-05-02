using UnityEngine;

public class Waypoints : MonoBehaviour
{
    // Create spaces for the element in child 
    // if 13 child element then 
    // initializing the points array length to be ==> 13
    public static Transform[] points;

    void Awake()
    {
        points = new Transform[transform.childCount];
        for (int i = 0; i < points.Length; i++)
        {
            // Looping through all the elements
            // getting all the child data
            points[i] = transform.GetChild(i);
        }
    }
}
