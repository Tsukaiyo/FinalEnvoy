using UnityEngine;

public class WayPointGizmo : MonoBehaviour
{
    public float radius = 1f;        // Radius of the cylinder
    public float height = 2f;        // Height of the cylinder
    public int segments = 5;        // Number of segments (more = smoother)

    private Color color = Color.green;

    // Draw the wireframe cylinder using Gizmos
    void OnDrawGizmos()
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
            topCircle[i] = new Vector3(Mathf.Cos(angle) * radius, height, Mathf.Sin(angle) * radius) + transform.position;
            bottomCircle[i] = new Vector3(Mathf.Cos(angle) * radius, 0, Mathf.Sin(angle) * radius) + transform.position;

            Gizmos.DrawLine(topCircle[i], bottomCircle[i]);

        }
    }
}
