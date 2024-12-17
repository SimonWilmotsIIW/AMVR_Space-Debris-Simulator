using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CelestialObject : MonoBehaviour, ISelectable
{
    public string objectName;
    public string metadata;
    public float sizeMultiplier = 1.0f;
    public float rotationSpeed = 1.0f;

    public Renderer objectRenderer;
    private Color originalColor;

    private void Start()
    {
        transform.localScale = Vector3.one * sizeMultiplier;
        if (objectRenderer == null) objectRenderer = GetComponent<Renderer>();
        originalColor = objectRenderer.material.color;
    }

    public void Update() 
    {
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime, Space.Self);
    }

    public void OnSelect()
    {
        objectRenderer.material.color = Color.red;
        Debug.Log($"Selected: {objectName}");
    }

    public void OnDeselect()
    {
        objectRenderer.material.color = originalColor;
        Debug.Log($"Deselected: {objectName}");
    }
}
