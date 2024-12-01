using UnityEngine;

public class Orbit : MonoBehaviour
{
    public Transform orbitCenter;
    public float orbitRadius = 10f; 
    public float orbitSpeed = 30f; 
    public float inclination = 0f; 
    private LineRenderer orbitVisualizer;

    private float currentAngle; 
    private bool isVisible = false;

    private void Start()
    {
        if (orbitCenter == null)
        {
            Debug.LogError("Orbit Center is not assigned!");
            return;
        }

        UpdatePosition();

        if (orbitVisualizer == null)
        {
            orbitVisualizer = gameObject.AddComponent<LineRenderer>();
            orbitVisualizer.enabled = isVisible;
            orbitVisualizer.startWidth = 0.1f;
            orbitVisualizer.endWidth = 0.1f;
            orbitVisualizer.material = new Material(Shader.Find("Sprites/Default"));
            orbitVisualizer.startColor = Color.green;
            orbitVisualizer.endColor = Color.green;
            orbitVisualizer.positionCount = 2;
            DrawOrbitPath();
        }
    }

    private void Update()
    {
        if (orbitCenter == null) return;

        currentAngle += orbitSpeed * Time.deltaTime;
        currentAngle %= 360; //0-360 degrees.

        UpdatePosition();
    }

    private void UpdatePosition()
    {
        float radians = Mathf.Deg2Rad * currentAngle;
        float x = orbitRadius * Mathf.Cos(radians);
        float z = orbitRadius * Mathf.Sin(radians);

        Quaternion inclinationRotation = Quaternion.Euler(inclination, 0, 0);

        Vector3 localPosition = new Vector3(x, 0, z);
        Vector3 inclinedPosition = inclinationRotation * localPosition;

        transform.position = orbitCenter.position + inclinedPosition;

        // if (orbitVisualizer != null && orbitVisualizer.positionCount > 1)
        // {
        //     Vector3 orbitCenterPosition = orbitCenter.position;
        //     orbitVisualizer.SetPosition(0, orbitCenterPosition);
        //     orbitVisualizer.SetPosition(1, orbitCenterPosition + inclinedPosition);
        // }
    }

    private void DrawOrbitPath()
    {
        int segments = 500;
        float angleStep = 360f / segments;
        
        orbitVisualizer.positionCount = segments + 1;

        for (int i = 0; i <= segments; i++)
        {
            float angle = i * angleStep;
            float radians = Mathf.Deg2Rad * angle;
            float x = orbitRadius * Mathf.Cos(radians);
            float z = orbitRadius * Mathf.Sin(radians);

            Quaternion inclinationRotation = Quaternion.Euler(inclination, 0, 0);
            Vector3 localPosition = new Vector3(x, 0, z);
            Vector3 inclinedPosition = inclinationRotation * localPosition;

            orbitVisualizer.SetPosition(i, orbitCenter.position + inclinedPosition);
        }
    }

    public void ToggleOrbitVisibility(bool toggle)
    {
        if (orbitVisualizer != null)
        {
            orbitVisualizer.enabled = toggle;
        }
    }

    public bool IsOrbitVisible() { return isVisible; }
    public LineRenderer GetOrbitRenderer() { return orbitVisualizer; }
}
