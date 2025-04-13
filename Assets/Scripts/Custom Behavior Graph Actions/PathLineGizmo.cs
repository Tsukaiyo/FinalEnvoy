using System.Drawing;
using Unity.AppUI.UI;
using UnityEngine;
using static UnityEngine.Rendering.HableCurve;
using Color = UnityEngine.Color;

public class PathLineGizmo : MonoBehaviour
{
    public float radius = 1f;        // Radius of the cylinder
    public float height = 2f;        // Height of the cylinder
    public int segments = 5;        // Number of segments (more = smoother)
    private int waypointCount;
    private GameObject[] waypoints;
    void Start()
    {
        waypointCount = transform.childCount;
        GetWaypoints();
  
    }
    void OnDrawGizmos()
    {
        for (int i = 0; i < waypointCount; i++)
        {
            Color waypointColor;
            if (i == 0) waypointColor = Color.cyan;
            else if (i > 0 && i == waypointCount - 1) waypointColor = Color.magenta;
            else
            {
                waypointColor = Color.grey;
            }
            WayPointGizmo waypointGizmo = waypoints[i].GetComponentInChildren<WayPointGizmo>();
            Destroy(waypointGizmo);
            DrawWaypointGizmo(waypointColor, waypoints[i].transform.position);
        }
        if(waypointCount > 1) DrawLineBetweenWayPoints();
    }
    void GetWaypoints()
    {
        waypoints = new GameObject[waypointCount];
        for (int i = 0; i < waypointCount; i++)
        {
            waypoints[i] = transform.GetChild(i).gameObject;
        }
    }
    public void DrawWaypointGizmo(Color color, Vector3 waypointPos)
    {
        Gizmos.color = color; // Set Gizmo color

        // Calculate angle step for each segment
        float angleStep = 360f / segments;

        // Draw the top and bottom circles
        Vector3[] topCircle = new Vector3[segments];
        Vector3[] bottomCircle = new Vector3[segments];

        // Calculate points on the top and bottom circles
        for (int i = 0; i < segments; i++)
        {
            float angle = i * angleStep * Mathf.Deg2Rad;

            // Adjusting to have the origin at the center of the bottom face
            topCircle[i] = new Vector3(Mathf.Cos(angle) * radius, height, Mathf.Sin(angle) * radius) + waypointPos;
            bottomCircle[i] = new Vector3(Mathf.Cos(angle) * radius, 0, Mathf.Sin(angle) * radius) + waypointPos;

            Gizmos.DrawLine(topCircle[i], bottomCircle[i]);

        }
    }
    public void DrawLineBetweenWayPoints()
    {
        for (int i = 0; i < waypointCount; i++)
        {
            Gizmos.color = Color.yellow;
            if(i>0) Gizmos.DrawLine(waypoints[i - 1].transform.position, waypoints[i].transform.position);

            //Connect Start point to End point
            if(i==waypointCount-1) Gizmos.DrawLine(waypoints[waypointCount-1].transform.position, waypoints[0].transform.position);
        }
    }
}
