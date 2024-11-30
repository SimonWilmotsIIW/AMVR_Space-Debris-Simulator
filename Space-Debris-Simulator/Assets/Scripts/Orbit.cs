using UnityEngine;

public class Orbit : MonoBehaviour
{
    public Transform orbitCenter;
    public float orbitRadius = 10f; 
    public float orbitSpeed = 30f; 
    public float inclination = 0f; 
    public LineRenderer orbitVisualizer;

    private float currentAngle; 

    private void Start()
    {
        if (orbitCenter == null)
        {
            Debug.LogError("Orbit Center is not assigned!");
            return;
        }

        UpdatePosition();
    }

    private void Update()
    {
        if (orbitCenter == null) return;

        currentAngle += orbitSpeed * Time.deltaTime;
        currentAngle %= 360; // Keep the angle within 0-360 degrees.

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
    }
}
